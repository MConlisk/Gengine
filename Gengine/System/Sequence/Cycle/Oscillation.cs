using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public class Oscillation : CycleType
	{
		private bool Forward = true;

		public Oscillation(int max,  int rate)
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
			if (Forward)
			{
				if (Value + Rate < Max)
				{
					Value += Rate;
				}
				else
				{
					Value = 0;
					Forward = true;
				}
			}
			else
			{
				if (Value - Rate > 0)
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