using System;
using mortgage_calculator_dotNetCore.Dtos;

namespace mortgage_calculator_dotNetCore.Interfaces
{
	public interface IMortgageCalculate
	{
        public MortgagePayment runCalculation(MortgageDto mortgage);
	}
}

