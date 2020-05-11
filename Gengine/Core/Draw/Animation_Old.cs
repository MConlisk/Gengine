using System;
using System.Collections.Generic;
using System.Drawing;

using Gengine.Core.MathU;
using Gengine.Core.Sequence;
using Gengine.Core.Sequence.Cycle;

using Pine; //_ = Log.Write(this, $"EntryString");

namespace Gengine.Core.Draw
{
	public class Animation_Old //: IDisposable
	{
		
		public List<int> FrameIndexes = new List<int>();
		public int ActiveIndex { get; private set; }
		//public Bitmap ActiveSprite { get; private set; }
		//public List<Bitmap> Frames { get; private set; }
		public Sequencer AnimSequencer { get; private set; }

		public float Fps { get; private set; }
		public int Rate { get; private set; }
		public Types Cycle { get; private set; }
		//public Plot2D Plot { get; private set; }

		public void SetRate(int newRate) => Rate = newRate;
		public void SetCycle(Types newCycle) => Cycle = newCycle;
		//public void SetPlot(Plot2D newPlot) => Plot = newPlot;
		public void SetFps(float newFps) => Fps = newFps;

		public void Start() => AnimSequencer.Start();
		public void Stop() => AnimSequencer.Stop();
		public void Reset() => AnimSequencer.Restart();

		public Animation_Old(List<int> frames, float fps, int rate, Types animLoopType, bool isLooping = true)
		{
			//Plot = new Plot2D();
			FrameIndexes = frames;
			ActiveIndex = FrameIndexes[0];
			Cycle = animLoopType;
			Fps = fps;
			Rate = rate;
			AnimSequencer = new Sequencer(Fps, FrameIndexes.Count, Rate, animLoopType, isLooping);
			AnimSequencer.Iterated += AnimSequencer_Iterated;
		}

		//public Animation(List<Bitmap> frames, float fps, int rate, Types animLoopType, bool isLooping = true)
		//{
		//	Plot = new Plot2D();
		//	Frames = frames;
		//	ActiveSprite = Frames[0];
		//	Cycle = animLoopType;
		//	Fps = fps;
		//	Rate = rate;
		//	AnimSequencer = new Sequencer(Fps, Frames.Count, Rate, animLoopType, isLooping);
		//	AnimSequencer.Iterated += AnimSequencer_Iterated;
		//}

		//private void AnimSequencer_Iterated(object sender, SequencerEventArgs args) 
		//	=> ActiveSprite = Frames[AnimSequencer.CurrentFrame];

		private void AnimSequencer_Iterated(object sender, SequencerEventArgs args)
			=> ActiveIndex = AnimSequencer.CurrentFrame;

		public void FpsAdjustment(float value) 
		{ 
			AnimSequencer.AdjustFps(value);
			Log.Write(this, $"Animation Fps Adjusted to {AnimSequencer.Fps}");
		}

		//public void Dispose() => ActiveSprite.Dispose();
	}
}
