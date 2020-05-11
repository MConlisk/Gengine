using System.Collections;

namespace Gengine.Core.Sequence.Cycle
{
	public interface ICycle
	{
		int GetMax();
		int GetRate();
		int GetValue();
		void Increment();
	}
}