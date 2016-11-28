using System;
using TestApplication.Quotes;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var requestResult = QuoteRequest.TryCreate(args);
            if (ExitIfError(requestResult)) return;

            var quoteResult = Quote.TryCreate(requestResult.Value, new QuoteCalculator());
            if (ExitIfError(quoteResult)) return;

            var _quote = quoteResult.Value;
            Console.WriteLine("Requested Amount: {0}", String.Format("{0:C}", _quote.RequestedAmount));
            Console.WriteLine("Rate: {0:0.0}%", Math.Truncate(_quote.Rate * 10) / 10);
            Console.WriteLine("Monthly Repayment: {0}", String.Format("{0:C}",Math.Truncate(_quote.MonthlyRepayment * 100) / 100));
            Console.WriteLine("Total Repayment: {0}", String.Format("{0:C}", Math.Truncate(_quote.TotalRepayment * 100) / 100));
            Console.ReadKey();
            
        }

        private static bool ExitIfError<T>(Result<T> result)
        {
            if (result.IsValid) return false;
            Console.WriteLine(result.Message);
            Console.ReadKey();
            return true;
        }
    }
}
