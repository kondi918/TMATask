namespace TMAWarehouseAPI.Exceptions
{
    public class ExistingItemException : Exception
    {
        public ExistingItemException(string message) : base(message) { }
    }
}
