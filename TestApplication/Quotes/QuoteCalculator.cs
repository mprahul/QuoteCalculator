using System;

namespace TestApplication.Quotes
{
    public class QuoteCalculator:IQuoteCalculator
    {
        private const int _numOfMonthsInYear = 12;
        public decimal CalculateMonthlyRepayment(decimal rate, decimal amount, int termInMonths)
        {
            if (rate == 0 || amount == 0 || termInMonths == 0) return 0.0m;
           
            var monthlyRateOfinterest = ((rate / 100) / _numOfMonthsInYear);

            //Formula: amount[cmonthlyRateOfinterest(1 + monthlyRateOfinterest)^numberOfMonths]/[(1 + monthlyRateOfinterest)^numberOfMonths - 1]

            var monthlyRepayment = amount * (monthlyRateOfinterest * (decimal)Math.Pow((double)(1.0m + monthlyRateOfinterest), termInMonths) /
                              ((decimal)Math.Pow((double)(1m + monthlyRateOfinterest), termInMonths) - 1m));
            return monthlyRepayment;
        }
    }
}
