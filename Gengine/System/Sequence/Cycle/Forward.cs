using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public class Forward : CycleType
	{
		public Forward(int max,  int rate)
		{
			Max = max;
			Value = 0;
			Rate = rate <= 0 ? 1 : rate;
		}

		public override int GetMax() => Max;
		public override int GetRate() => Rate;
		public override int GetValue() => Value;
		public override void Increment() 
		{ 

			Value = (Value + Rate) < Max ? Value + Rate : 0; 

		}
	}
}