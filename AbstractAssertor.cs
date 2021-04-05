using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Arcaim.Assertor.Validators;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Arcaim.Assertor
{
    public abstract class AbstractValidator<T>
    {
        private T _model;
        public bool IsModelValid => _model is not null ? true : false;
        private List<Task> _validationTaskList = new List<Task>();

        public async Task SetModel(HttpContext context)
        {
            try
            {
                var request = context.Request;
                request.EnableBuffering();

                using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
                var json = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                var model = JsonConvert.DeserializeObject<T>(json);
                if (model is null || !IsJsonMatchesToModelType(json))
                {
                    return;
                }

                _model = model;
            }
            catch (Exception)
            {
                // Don't do anything ...the model type just doesn't fit 
            }
        }

        private bool IsJsonMatchesToModelType(string json)
        {
            string pattern = "\\\"(.*?)\\\":";
            var jsonType = JsonConvert.SerializeObject(System.Activator.CreateInstance(typeof(T)));

            var responseParameters = Regex.Matches(json, pattern);
            var typeParameters = Regex.Matches(jsonType, pattern);

            var matchesParametersCount = responseParameters
                .Where(b => typeParameters
                .Any(x => b.Value.Equals(x.Value, StringComparison.CurrentCultureIgnoreCase)))
                .ToList().Count;

            return matchesParametersCount == responseParameters.ToList().Count;
        }

        private void AddValidationTask(Action action)
            => _validationTaskList.Add(new Task(action));

        protected internal async Task ValidateAsync()
        {
            _validationTaskList.ForEach(task => task.Start());
            Task.WaitAll(_validationTaskList.ToArray());

            await Task.CompletedTask;
        }

        public void DigitExists(Func<T, string> expression)
            => AddValidationTask(
                () => DigitExistsValidator.Create(expression.Invoke(_model)).Validate()
            );

        public void Email(Func<T, string> expression)
            => AddValidationTask(
                () => EmailValidator.Create(expression.Invoke(_model)).Validate()
            );

        public void GreaterThanOrEqual(Func<T, decimal> expression, decimal valueToCompare)
            => AddValidationTask(
                () => GreaterThanOrEqualValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void GreaterThan(Func<T, decimal> expression, decimal valueToCompare)
            => AddValidationTask(
                () => GreaterThanValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void LessThanOrEqual(Func<T, decimal> expression, decimal valueToCompare)
            => AddValidationTask(
                () => LessThanOrEqualValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void LessThan(Func<T, decimal> expression, decimal valueToCompare)
            => AddValidationTask(
                () => LessThanValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void MaximumLength(Func<T, string> expression, int valueToCompare)
            => AddValidationTask(
                () => MaximumLengthValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void MinimumLength(Func<T, string> expression, int valueToCompare)
            => AddValidationTask(
                () => MinimumLengthValidator.Create(expression.Invoke(_model), valueToCompare).Validate()
            );

        public void SmallCharExists(Func<T, string> expression)
            => AddValidationTask(
                () => SmallCharExistsValidator.Create(expression.Invoke(_model)).Validate()
            );

        public void UpperCharExists(Func<T, string> expression)
            => AddValidationTask(
                () => UpperCharExistsValidator.Create(expression.Invoke(_model)).Validate()
            );
    }
}