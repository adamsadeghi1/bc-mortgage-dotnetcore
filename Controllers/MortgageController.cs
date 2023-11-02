using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using mortgage_calculator_dotNetCore.Dtos;
using mortgage_calculator_dotNetCore.Interfaces;

namespace mortgage_calculator_dotNetCore.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class MortgageController: ControllerBase
    {
        private readonly IMortgageCalculateFactory mortgageCalculateFactory;

        public MortgageController(IMortgageCalculateFactory mortgageCalculateFactory)
		{
            this.mortgageCalculateFactory = mortgageCalculateFactory;
        }


        [HttpGet()]
        public string HelloWorld()
        {
            return "Hello World";
        }

  

        [HttpPost()]
        [EnableCors("AllowReactOrigin")]
        public IActionResult CalculateMortgage([FromBody] MortgageDto mortgageDto)
        {
            var calculate = mortgageCalculateFactory.GetMortgageCalculator(mortgageDto);
            try
            {
                var result = calculate.runCalculation(mortgageDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

