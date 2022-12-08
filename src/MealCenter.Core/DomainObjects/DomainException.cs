namespace MealCenter.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException()
        {
            ValidationErrors = new List<string>();
        }

        public DomainException(string message) : base(message)
        {
            ValidationErrors = new List<string>();
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
            ValidationErrors = new List<string>();
        }

        public List<string> ValidationErrors { get; }
    }
}
