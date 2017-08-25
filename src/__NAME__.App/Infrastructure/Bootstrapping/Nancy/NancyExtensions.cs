using System;
using System.Linq;
using System.Collections.Generic;
using FluentValidation.Results;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Newtonsoft.Json;

namespace __NAME__.App.Infrastructure.Bootstrapping.Nancy
{
    public static class NancyExtensions
    {
        public static bool IsHttpOptions(this NancyContext context)
        {
            return context.Request.Method.Equals("OPTIONS");
        }

        public static T BindAndValidateModel<T>(this NancyModule module) where T : class
        {
            return module.BindAndValidateModel<T>(null);
        }

        public static T BindAndValidateModel<T>(this NancyModule module, Action<T> hookToSetModel) where T : class
        {
            try {
                var input = module.Bind<T>();
                
                if (hookToSetModel != null) {
                    hookToSetModel(input);
                }
                
                module.ValidateModel(input);

                return input;
            }
            catch (JsonReaderException ex) {
                var errors = new Dictionary<string, string[]> { { ex.Path, new[] { ex.Message } } };
                throw new BadRequestException(errors);
            }
        }

        public static void ValidateModel<T>(this NancyModule module, T input) where T : class
        {
            var validation = module.Validate(input);
            if (validation.IsValid) return;
            
            throw new BadRequestException(validation.GetFlattenedErrors());
        }

        public static IDictionary<string, string[]> GetFlattenedErrors(this ModelValidationResult validationResult)
        {
            return validationResult.Errors.ToDictionary(errorItem => errorItem.Key, errorItem => errorItem.Value.Select(error => error.ErrorMessage).ToArray());
        }

        public static IDictionary<string, string[]> GetFlattenedErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.ToDictionary(errorItem => errorItem.PropertyName, errorItem => new [] {errorItem.ErrorMessage});
        }
    }
}
