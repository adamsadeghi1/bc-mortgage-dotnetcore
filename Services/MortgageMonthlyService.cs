using System;
using System.Net.Mail;
using mortgage_calculator_dotNetCore.Abstracts;
using mortgage_calculator_dotNetCore.Dtos;
using mortgage_calculator_dotNetCore.Enums;
using mortgage_calculator_dotNetCore.Interfaces;
using mortgage_calculator_dotNetCore.Constant;

namespace mortgage_calculator_dotNetCore.Services
{
	public class MortgageMonthlyService : MortgageAbstract , IMortgageCalculate
    {
        public MortgagePayment runCalculation(MortgageDto mortgage)
        {
            if (!ValidateDownPayment(mortgage))
                throw new Exception(String.Format( "Minimum Payment is not enough!! % 5 for lessEqual 500_000, $25000 + (mortgageprice - 500_000) * % 10 for between 500_000 and 1_000_000, % 20 for more than 1M.In your case: {0} is required.",
                                        GetMinDownPaymentRequired(mortgage.PropertyPrice)
                                        ));

            double payPerPeriod = GetMortgagePaymentPerPeriod(mortgage);
            var mortgagePayment = new MortgagePayment {
                Type= PaymentScheduleType.MONTHLY.ToString(),
                Mortgage= $"${GetMortgagePrinciple(mortgage)}",
                Payment= $"${ payPerPeriod:F2}",
                PaymentNumber= GetPaymentNumber(mortgage.Period),
                SchedulPayments= GetSchedulePayment(mortgage, payPerPeriod),
            };

            return mortgagePayment;
        }

        public override int GetPaymentNumber(int period)
        {
            return period * Constants.MONTH_IN_YEAR;
        }

        public override double GetRate(double annualInterestRate)
        {
            return annualInterestRate / Constants.MONTH_IN_YEAR / Constants.PERCENT;
        }

    }
}

