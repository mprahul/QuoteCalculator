namespace TestApplication.Quotes
{
    public class Quote
    {
        private readonly IQuoteCalculator _quoteCalculator;
        private readonly decimal _rate;
        private readonly decimal _requestedAmount;
        private readonly int _termInMonths;
        private decimal? _monthlyRepayment = null;


        private Quote(decimal rate, decimal requestedAmount, int termInMonths, IQuoteCalculator quoteCalculator)
        {
            _rate = rate;
            _requestedAmount = requestedAmount;
            _termInMonths = termInMonths;
            _quoteCalculator = quoteCalculator;
        }

        public decimal MonthlyRepayment
        {
            get
            {
                if (_monthlyRepayment == null) _monthlyRepayment = CalculateMonthlyRepayment();
                return _monthlyRepayment.Value;
            }
        }

        public decimal TotalRepayment { get { return MonthlyRepayment*TermInMonths; } }
        public decimal Rate { get { return _rate; } }
        public decimal RequestedAmount { get { return _requestedAmount; } }
        public int TermInMonths { get { return _termInMonths; } }

        private decimal CalculateMonthlyRepayment()
        {
           var monthlyRepayment= _quoteCalculator.CalculateMonthlyRepayment(this.Rate,this.RequestedAmount,this.TermInMonths);
            return monthlyRepayment;
        }

        public static Result<Quote> TryCreate(QuoteRequest request, IQuoteCalculator quoteCalculator)
        {
            var rateResult = request.TryGetRate();
            if (!rateResult.IsValid) return Result<Quote>.Error(rateResult.Message);
            return Result<Quote>.Ok(new Quote(rateResult.Value, request.RequestedAmount, request.TermInMonths, quoteCalculator));
        }
    }
}
