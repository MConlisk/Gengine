using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public class Reverse : CycleType
	{

		public Reverse(int max, int min, int rate)
		{
			Max = max;
			Min = min;
			Value = Min;
			Rate = rate >= 0 ? -1 : rate;
		}

		public override ArrayList GetCollection() => Collection;
		public override int GetMax() => Max;
		public override int GetMin() => Min;
		public override int GetRate() => Rate;
		public override int GetValue() => Value;
		public override void Increment() => Value = (Value - Rate) > Min ? Value - Rate : Max;
	}
}