using System.Collections;

namespace Gengine.Core.Sequence.Cycle
{
	public class Reverse : CycleType
	{

		public Reverse(int max, int rate)
		{
			Max = max;
			Value = 0;
			Rate = rate >= 0 ? -1 : rate;
		}

		public override int GetMax() => Max;
		public override int GetRate() => Rate;
		public override int GetValue() => Value;
		public override void Increment() => Value = (Value - Rate) > 0 ? Value - Rate : Max;
	}
}