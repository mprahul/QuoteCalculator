using System;

namespace TestApplication.Exceptions
{
    public class InvalidCsvLendersFileFormatException : Exception
    {
        public InvalidCsvLendersFileFormatException(string row)
            : base("Invalid CSV line: " + row)
        {
            
        }
    }
}