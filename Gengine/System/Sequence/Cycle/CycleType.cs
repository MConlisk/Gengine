using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public abstract class CycleType : ICycle
	{

		internal int Value { get; set; }
		internal int Rate { get; set; }
		internal int Max { get; set; }
		internal int Min { get; set; }

		public abstract int GetValue();
		public abstract int GetRate();
		public abstract int GetMax();
		public abstract int GetMin();
		public abstract void Increment();
	}
}