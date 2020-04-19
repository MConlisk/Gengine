using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public interface ICycle
	{
		int GetMax();
		int GetMin();
		int GetRate();
		int GetValue();
		void Increment();
	}
}