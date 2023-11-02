using System;
namespace mortgage_calculator_dotNetCore.Dtos
{
	public class SchedulePayment
	{
		public string? PayPerPeriod { get; set; }
		public int? PaymentNumber { get; set; }
		public string? RemainingBalance { get; set; }
		public string? PaidSoFarFromPrinciple { get; set; }
	}
}

