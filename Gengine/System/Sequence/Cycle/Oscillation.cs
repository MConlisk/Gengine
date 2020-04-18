using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public class Oscillation : CycleType
	{
		private bool Forward = true;

		public Oscillation(int max, int min, int rate)
		{
			Max = max;
			Min = min;
			Value = Min;
			Rate = rate <= 0 ? 1 : rate;
		}

		public override ArrayList GetCollection() => Collection;
		public override int GetMax() => Max;
		public override int GetMin() => Min;
		public override int GetRate() => Rate;
		public override int GetValue() => Value;
		public override void Increment()
		{
			if (Forward)
			{
				if (Value + Rate < Max)
				{
					Value += Rate;
				}
				else
				{
					Value = Min;
					Forward = true;
				}
			}
			else
			{
				if (Value - Rate > Min)
				{
					Value -= Rate;
				}
				else
				{
					Value = Max;
					Forward = false;
				}
			}
		}
	}
}