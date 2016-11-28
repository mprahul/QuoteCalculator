namespace TestApplication.Quotes
{
    /// <summary>
    /// Encapsulates a valid quote request
    /// </summary>
    public class QuoteRequest
    {
        private const int _termInMonths = 36;
        private readonly Lenders.Lenders _lenders;
        private readonly decimal _requestedAmount;

        private QuoteRequest( decimal requestedAmount, Lenders.Lenders lenders)
        {
            _requestedAmount = requestedAmount;
            _lenders = lenders;
        }

        public Lenders.Lenders Lenders1 { get { return _lenders; } }
        public decimal RequestedAmount { get { return _requestedAmount; } }
        public int TermInMonths { get { return _termInMonths; } }

        /// <summary>
        /// Factory method to create a request with the command line arguments.
        /// This method will validate the args and throw an "InvalidRequestParametersException" if the arguments aren't valid.
        /// </summary>
        /// <param name="args">Array with 2 strings: 
        /// 1 - CSV filename
        /// 2 - Loan amount
        /// </param>
        /// <returns>A valid quote request</returns>
        public static Result<QuoteRequest> TryCreate(string[] args)
        {
            if (args == null || args.Length != 2) return Result<QuoteRequest>.Error("Invalid parameters");

            var lendersResult = ValidCsv(args[0]);
            if (!lendersResult.IsValid) return Result<QuoteRequest>.Error(lendersResult.Message);

            var loanValueResult = ValidLoanValue(args[1]);
            if (!loanValueResult.IsValid) return Result<QuoteRequest>.Error(loanValueResult.Message);

            return Result<QuoteRequest>.Ok(new QuoteRequest(loanValueResult.Value, lendersResult.Value));
        }

        private static Result<decimal> ValidLoanValue(string arg)
        {
            decimal amount;
            if (!decimal.TryParse(arg, out amount)) return Result<decimal>.Error("Invalid amount format: " + arg);
            if ((amount % 100.0m != 0) || amount < 1000.0m || amount > 15000.0m) return Result<decimal>.Error("Invalid amount value: " + arg);
            return Result<decimal>.Ok(amount);
        }

        private static Result<Lenders.Lenders> ValidCsv(string filename)
        {
            return Lenders.Lenders.LoadLendersFromCsvFile(filename);
        }

        public Result<decimal> TryGetRate()
        {
            var lenderResult = _lenders.GetLenderByAmount(_requestedAmount);
            if (!lenderResult.IsValid) return Result<decimal>.Error(lenderResult.Message);
            return Result<decimal>.Ok(lenderResult.Value.Rate);
        }
    }
}
