using System;
namespace mortgage_calculator_dotNetCore.Dtos
{
	public class MortgagePayment
	{
		public string? Type { get; set; }
        public string? Mortgage { get; set; }
        public string? Payment { get; set; }
        public int? PaymentNumber { get; set; }
        public List<SchedulePayment>? SchedulPayments { get; set; }

    }
}

