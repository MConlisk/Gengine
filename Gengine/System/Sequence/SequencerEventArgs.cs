using System;
using System.Drawing;

namespace Gengine.System.Sequence
{
	public class SequencerEventArgs : EventArgs
	{
		public int Index { get; set; }
		public Bitmap CurrentFrame { get; set; }
	}
}
