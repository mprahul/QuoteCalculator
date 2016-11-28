using System;
using TestApplication.Exceptions;

namespace TestApplication.Lenders
{
    public class LenderDetails
    {
        private string _name;
        private decimal _rate;
        private decimal _amount;

        private LenderDetails(string name, decimal rate, decimal amount)
        {
            _name = name;
            _rate = rate;
            _amount = amount;
        }

        public string Name { get { return _name; } }
        public decimal Rate { get { return _rate; } }
        public decimal Amount { get { return _amount; } }

        public static LenderDetails Create(string row)
        {
            if (row == null) throw new ArgumentNullException("row");
            var args = row.Split(',');
            if (args.Length != 3) throw new InvalidCsvLendersFileFormatException(row);
            var name = args[0].Trim('"');
            decimal rate, amount;
            if (!decimal.TryParse(args[1], out rate)) throw new InvalidCsvLendersFileFormatException(row);
            if (!decimal.TryParse(args[2], out amount)) throw new InvalidCsvLendersFileFormatException(row);
            return new LenderDetails(name, rate, amount);
        }
    }
}