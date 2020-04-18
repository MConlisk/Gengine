using System.Collections;

namespace Gengine.System.Sequence.Cycle
{
	public interface ICycle
	{
		ArrayList GetCollection();
		int GetMax();
		int GetMin();
		int GetRate();
		int GetValue();
		void Increment();
	}
}