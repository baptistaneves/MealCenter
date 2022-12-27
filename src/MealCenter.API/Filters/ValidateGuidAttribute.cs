namespace MealCenter.API.Filters
{
    public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private readonly List<string> _keys;

        public ValidateGuidAttribute(params string[] keys)
        {
            _keys = keys.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasError = false;
            var errorMessage = "";
            _keys.ForEach(key =>
            {
                if (!context.ActionArguments.TryGetValue(key, out var value)) return;

                if (!Guid.TryParse(value?.ToString(), out var guid))
                {
                    hasError = true;
                    errorMessage = $"The identifier for {key} is not correct GUID format";
                }
            });

            if (hasError)
            {
                context.Result = new BadRequestObjectResult(new { success = false, data = errorMessage }); ;
            }
        }
    }
}
