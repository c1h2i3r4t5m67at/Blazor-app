
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Exl.ResumeBuilder.Web.Services
{
    public static class EditContextCustomValidationExtensions
    {
        public static IDisposable EnableCustomValidation(this EditContext editContext, bool doFieldValidation, bool clearMessageStore)
            => new DataAnnotationsEventSubscriptions(editContext, doFieldValidation, clearMessageStore);

        private static event Action? OnClearCache;

        private static void ClearCache(Type[]? _)
            => OnClearCache?.Invoke();

        private sealed class DataAnnotationsEventSubscriptions : IDisposable
        {
            private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo?> _propertyInfoCache = new();

            private readonly EditContext _editContext;
            private readonly ValidationMessageStore _messages;
            private bool _doFieldValidation;
            private bool _clearMessageStore;

            public DataAnnotationsEventSubscriptions(EditContext editContext, bool doFieldValidation, bool clearMessageStore)
            {
                _doFieldValidation = doFieldValidation;
                _clearMessageStore = clearMessageStore;
                _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
                _messages = new ValidationMessageStore(_editContext);

                if (doFieldValidation)
                    _editContext.OnFieldChanged += OnFieldChanged;
                _editContext.OnValidationRequested += OnValidationRequested;

                if (MetadataUpdater.IsSupported)
                {
                    OnClearCache += ClearCache;
                }
            }
            private void OnFieldChanged(object? sender, FieldChangedEventArgs eventArgs)
            {
                var fieldIdentifier = eventArgs.FieldIdentifier;
                if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
                {
                    var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
                    var validationContext = new ValidationContext(fieldIdentifier.Model)
                    {
                        MemberName = propertyInfo.Name
                    };
                    var results = new List<ValidationResult>();

                    Validator.TryValidateProperty(propertyValue, validationContext, results);
                    _messages.Clear(fieldIdentifier);
                    foreach (var result in CollectionsMarshal.AsSpan(results))
                    {
                        _messages.Add(fieldIdentifier, result.ErrorMessage!);
                    }

                    // We have to notify even if there were no messages before and are still no messages now,
                    // because the "state" that changed might be the completion of some async validation task
                    _editContext.NotifyValidationStateChanged();
                }
            }

            private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
            {
                var validationContext = new ValidationContext(_editContext.Model);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(_editContext.Model, validationContext, validationResults, true);

                // Transfer results to the ValidationMessageStore
                _messages.Clear();
                foreach (var validationResult in validationResults)
                {
                    if (validationResult == null)
                    {
                        continue;
                    }

                    var hasMemberNames = false;
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        hasMemberNames = true;
                        _messages.Add(_editContext.Field(memberName), validationResult.ErrorMessage!);
                    }

                    if (!hasMemberNames)
                    {
                        _messages.Add(new FieldIdentifier(_editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage!);
                    }
                }

                _editContext.NotifyValidationStateChanged();
            }

            public void Dispose()
            {
                if (_clearMessageStore)
                    _messages.Clear();
                if (_doFieldValidation)
                    _editContext.OnFieldChanged -= OnFieldChanged;
                _editContext.OnValidationRequested -= OnValidationRequested;
                _editContext.NotifyValidationStateChanged();

                if (MetadataUpdater.IsSupported)
                {
                    OnClearCache -= ClearCache;
                }
            }

            private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
            {
                var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
                if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
                {
                    // DataAnnotations only validates public properties, so that's all we'll look for
                    // If we can't find it, cache 'null' so we don't have to try again next time
                    propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                    // No need to lock, because it doesn't matter if we write the same value twice
                    _propertyInfoCache[cacheKey] = propertyInfo;
                }

                return propertyInfo != null;
            }

            internal void ClearCache()
                => _propertyInfoCache.Clear();
        }
    }

}
