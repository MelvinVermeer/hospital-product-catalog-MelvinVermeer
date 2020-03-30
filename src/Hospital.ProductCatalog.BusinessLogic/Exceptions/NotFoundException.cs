using System;

namespace Hospital.ProductCatalog.BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string objectType, int code)
           : base($"Object \"{objectType}\" with code {code} not found")
        { }
    }
}
