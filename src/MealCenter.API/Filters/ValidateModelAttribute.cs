namespace MealCenter.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private List<string> _errors = new List<string>();

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    _errors.Add(error.Value.Errors.Select(e=> e.ErrorMessage).FirstOrDefault());
                }

                context.Result = new BadRequestObjectResult(new { success = false, data = _errors });
            }
        }
    }
}
