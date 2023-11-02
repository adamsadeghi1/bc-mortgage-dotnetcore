using System;
using mortgage_calculator_dotNetCore.Abstracts;
using mortgage_calculator_dotNetCore.Constant;
using mortgage_calculator_dotNetCore.Dtos;
using mortgage_calculator_dotNetCore.Enums;
using mortgage_calculator_dotNetCore.Interfaces;

namespace mortgage_calculator_dotNetCore.Services
{
	public class MortgageBiWeeklyService : MortgageAbstract, IMortgageCalculate
    {
        public MortgagePayment runCalculation(MortgageDto mortgage)
        {
            if (!ValidateDownPayment(mortgage))
                throw new Exception(String.Format("Minimum Payment is not enough!! % 5 for lessEqual 500_000, $25000 + (mortgageprice - 500_000) * % 10 for between 500_000 and 1_000_000, % 20 for more than 1M.In your case: {0} is required.",
                                        GetMinDownPaymentRequired(mortgage.PropertyPrice)
                                        ));

            double payPerPeriod = GetMortgagePaymentPerPeriod(mortgage);
            var mortgagePayment = new MortgagePayment
            {
                Type = PaymentScheduleType.BIWEEKLY.ToString(),
                Mortgage = $"${GetMortgagePrinciple(mortgage)}",
                Payment = $"${payPerPeriod:F2}",
                PaymentNumber = GetPaymentNumber(mortgage.Period),
                SchedulPayments = GetSchedulePayment(mortgage, payPerPeriod),
            };

            return mortgagePayment;
        }

        public override int GetPaymentNumber(int period)
        {
            return period * Constants.BIWEEKLY_IN_YEAR;
        }

        public override double GetRate(double annualInterestRate)
        {
            return Math.Pow(1 + (double)annualInterestRate / Constants.PERCENT,(double) 1 / Constants.BIWEEKLY_IN_YEAR) - 1;
        }
    }
}

