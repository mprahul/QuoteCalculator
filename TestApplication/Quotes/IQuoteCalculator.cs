namespace TestApplication
{
    public interface IQuoteCalculator
    {
        decimal CalculateMonthlyRepayment(decimal rate, decimal amount, int termInMonths);
    }
}
