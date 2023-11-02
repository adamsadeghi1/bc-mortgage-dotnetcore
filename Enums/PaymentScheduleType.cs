using System;
using System.Runtime.Serialization;

namespace mortgage_calculator_dotNetCore.Enums
{
	public enum PaymentScheduleType
	{
        [EnumMember(Value = "WEEKLY")]
        WEEKLY ,
        [EnumMember(Value = "BIWEEKLY")]
        BIWEEKLY,
        [EnumMember(Value = "MONTHLY")]
        MONTHLY ,
    }
}

