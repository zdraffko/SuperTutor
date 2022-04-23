using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using SuperTutor.SharedLibraries.BuildingBlocks.Domain.Utility.IdentifierConversion.JsonConversion;
using System.Text.Json;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Api.ModelBinders;

internal class JsonQueryModelBinder : IModelBinder
{
    private readonly IObjectModelValidator modelValidator;
    private readonly ILogger<JsonQueryModelBinder> logger;

    public JsonQueryModelBinder(IObjectModelValidator modelValidator, ILogger<JsonQueryModelBinder> logger)
    {
        this.modelValidator = modelValidator;
        this.logger = logger;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        try
        {
            var rawQuery = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
            if (rawQuery is null)
            {
                return Task.CompletedTask;
            }

            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonSerializerOptions.Converters.Add(new IdentifierJsonConverterFactory());

            var query = JsonSerializer.Deserialize(rawQuery, bindingContext.ModelType, jsonSerializerOptions);
            bindingContext.Result = ModelBindingResult.Success(query);

            if (query is null)
            {
                return Task.CompletedTask;
            }

            modelValidator.Validate(
                bindingContext.ActionContext,
                validationState: bindingContext.ValidationState,
                prefix: string.Empty,
                model: query
            );
        }
        catch (JsonException jsonException)
        {
            logger.LogError(jsonException, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);

            bindingContext.ActionContext.ModelState.TryAddModelError(key: jsonException.Path, exception: jsonException, bindingContext.ModelMetadata);
        }
        catch (Exception exception) when (exception is FormatException or OverflowException)
        {
            logger.LogError(exception, "Failed to bind parameter '{FieldName}'", bindingContext.FieldName);

            bindingContext.ActionContext.ModelState.TryAddModelError(string.Empty, exception, bindingContext.ModelMetadata);
        }

        return Task.CompletedTask;
    }
}
