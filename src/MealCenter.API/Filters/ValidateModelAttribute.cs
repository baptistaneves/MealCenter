namespace MealCenter.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new {  success = false, data = context.ModelState.AsEnumerable() });
            }
        }
    }
}
