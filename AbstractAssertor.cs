using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Arcaim.Assertor
{
    public abstract class AbstractAssertor<T> : AbstractValidator<T>
    {
        private T _model;
        public bool IsModelValid => _model is not null ? true : false;

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
                // Do not nothing ...type of model just doesn't fit 
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

        public async Task ValidateAsync() => await base.ValidateAsync(_model);
    }
}