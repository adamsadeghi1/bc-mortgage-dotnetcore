using System;
using System.Security.AccessControl;
using mortgage_calculator_dotNetCore.Dtos;
using mortgage_calculator_dotNetCore.Interfaces;

namespace mortgage_calculator_dotNetCore.Services
{
	public class MortgageCalculateFactory : IMortgageCalculateFactory
    {
        private readonly IServiceProvider serviceProvider;

        public MortgageCalculateFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMortgageCalculate GetMortgageCalculator(MortgageDto mortgageDto)
        {
            if (Enum.TryParse(typeof(Enums.PaymentScheduleType), mortgageDto.PaymentSchedule.ToUpper(), out var paymentType))
            {

                Enums.PaymentScheduleType  desiredType =  (Enums.PaymentScheduleType)paymentType;

                switch (desiredType)
                {
                    case Enums.PaymentScheduleType.WEEKLY:
                        return new MortgageWeeklyService();
                    case Enums.PaymentScheduleType.BIWEEKLY:
                        return new MortgageBiWeeklyService();
                    case Enums.PaymentScheduleType.MONTHLY:
                        return new MortgageMonthlyService();
                    default:
                        throw new Exception(String.Format("Invalid payment schedule: {0}. paymentSchedule shoud be one of these options case-insensitive : weekly| biweekly | monthly ", mortgageDto.PaymentSchedule));
                }

        }
            else
            {
                throw new Exception(String.Format("Invalid payment schedule: {0}. paymentSchedule shoud be one of these options case-insensitive : weekly| biweekly | monthly ", mortgageDto.PaymentSchedule));
            }
}
    }
}

