using mortgage_calculator_dotNetCore.Dtos;

namespace mortgage_calculator_dotNetCore.Abstracts
{
	public abstract class MortgageAbstract
	{
		public abstract double GetRate(double annualInterestRate);

		public abstract int GetPaymentNumber(int period);

        protected double GetMortgagePaymentPerPeriod(MortgageDto mortgage)
        {
            return MortgageCalculator(
              GetMortgagePrinciple(mortgage),
              GetRate(mortgage.AnnualInterestRate),
              GetPaymentNumber(mortgage.Period)
        
            );
        }
        protected double GetMortgagePrinciple(MortgageDto mortgage)
        {
            double result = mortgage.PropertyPrice - mortgage.Downpayment;
            if (result <= 0) 
                    throw new Exception("Property price should be greater than downpayment");
            return mortgage.PropertyPrice - mortgage.Downpayment;
        }
        private double MortgageCalculator(double principal, double interestRate,int paymentNumberBasedOnPeriod)
        {
            return (
              (principal *
                (interestRate *
                  Math.Pow(1 + interestRate, paymentNumberBasedOnPeriod))) /
              (Math.Pow(1 + interestRate, paymentNumberBasedOnPeriod) - 1)
            );
        }

        protected double CalculateRemainingBalance(MortgageDto mortgage ,int numberOfPaymentPast )
        {
            return (
              (GetMortgagePrinciple(mortgage) *
                (Math.Pow(
                  1 + GetRate(mortgage.AnnualInterestRate),
                  GetPaymentNumber(mortgage.Period)) -
                  Math.Pow(
                    1 + GetRate(mortgage.AnnualInterestRate),
                    numberOfPaymentPast       
                  ))) /
              (Math.Pow(1 + GetRate(mortgage.AnnualInterestRate),
                GetPaymentNumber(mortgage.Period)) -1)
            );
        }


        protected List<SchedulePayment> GetSchedulePayment(MortgageDto mortgage,double  payPerPeriod)
        {
            List<SchedulePayment> schedulePayments = new List<SchedulePayment>();
            int paymentNumber = GetPaymentNumber(mortgage.Period);

            for (int i = 0; i < paymentNumber; i++)
            {
                double remainingBalence = CalculateRemainingBalance(mortgage, i + 1);
                SchedulePayment payment = new SchedulePayment
                {
                    PayPerPeriod = (i + 1 == paymentNumber)
                ? $"${CalculateRemainingBalance(mortgage, i):F2}"
                : $"${payPerPeriod:F2}",
                    PaymentNumber = i + 1,
                    RemainingBalance = $"${remainingBalence:F2}",
                    PaidSoFarFromPrinciple = $"${(GetMortgagePrinciple(mortgage) - remainingBalence):F2}"
                };
                schedulePayments.Add(payment);
            }

             return schedulePayments;
        }

        protected double GetMinDownPaymentRequired(double propertyPrice)
        {
            if (propertyPrice <= 500_000) return propertyPrice * 0.05;
            else if (propertyPrice > 500_000 && propertyPrice < 1_000_000)
                return 500_000 * 0.05 + (propertyPrice - 500_000) * 0.1;
            else return propertyPrice * 0.2;
        }


        protected bool ValidateDownPayment(MortgageDto mortgage )
        {
            if (
              mortgage.Downpayment <
              GetMinDownPaymentRequired(mortgage.PropertyPrice)
            )
                return false;
            return true;
        }


    }
}

