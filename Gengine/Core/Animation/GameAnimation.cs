using System.Collections.Generic;

namespace Gengine.Core.Animation
{
	public class GameAnimation
	{
		private readonly List<int> Frames;
		private int Index;

		public string Name { get; private set; }
		public int CurrentFrame => Frames[Index];

		public GameAnimation(string name, List<int> frames) => (Name, Frames) = (name, frames);

		public void SetIndex(int value) => Index = value;
		public void NextFrame(int step)
		{
			int M = Frames.Count - 1;
			int i = Index + step;
			Index = i >= 0 ? i <= M ? i : 0 : M;
		}
	}
}