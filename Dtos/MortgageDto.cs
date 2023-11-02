using System;
using System.ComponentModel.DataAnnotations;
using mortgage_calculator_dotNetCore.Enums;

namespace mortgage_calculator_dotNetCore.Dtos
{
	public class MortgageDto
	{
        [Required]
        [Range(0,Double.MaxValue)]
        public double PropertyPrice { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public double Downpayment { get; set; }
        [Required]
        [Range(0.1, 99.99, ErrorMessage ="Annual intrest rate should be between 0.1 and 99.99.")]
        public double AnnualInterestRate { get; set; }
        [Required]
        [Range(5, 30)]
        public int Period { get; set; }
        [Required]
        [RegularExpression(@"^(?i)(weekly|biweekly|monthly)$",
         ErrorMessage = "paymentSchedule shoud be one of these options case-insensitive : weekly| biweekly | monthly")]
        public required string PaymentSchedule { get; set; }
	}
}

