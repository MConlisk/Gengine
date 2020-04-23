using System;
using System.Collections.Generic;
using System.Drawing;

using Gengine.System.MathU;
using Gengine.System.Sequence;
using Gengine.System.Sequence.Cycle;

using PineLog;

namespace Gengine.System.Draw
{
	public class Animation : IDisposable
	{
		public Bitmap ActiveSprite { get; private set; }
		public List<Bitmap> Frames { get; private set; }
		public Sequencer AnimSequencer { get; private set; }
		public float Fps { get; private set; }
		public int Rate { get; private set; }
		public CycleTypes Type { get; private set; }

		public Plot2D Plot = new Plot2D();

		public Animation(List<Bitmap> frames, float fps, int rate, CycleTypes animLoopType, bool isLooping = true)
		{
			_ = Pinelog.WriteEntry(this, $"New Animation created");
			Frames = frames;
			ActiveSprite = Frames[0];
			Type = animLoopType;
			Fps = fps;
			Rate = rate;
			AnimSequencer = new Sequencer(Fps, Frames.Count, Rate, animLoopType, isLooping);
			AnimSequencer.Iterated += AnimSequencer_Iterated;
		}

		private void AnimSequencer_Iterated(object sender, SequencerEventArgs args) 
			=> ActiveSprite = Frames[AnimSequencer.CurrentFrame];

		public void Start() => AnimSequencer.Start();
		public void Stop() => AnimSequencer.Stop();
		public void Reset() => AnimSequencer.Restart();

		public void FpsAdjustment(float value) 
		{ 
			AnimSequencer.AdjustFps(value);
			_ = Pinelog.WriteEntry(this, $"Animation Fps Adjusted to {AnimSequencer.Fps}");
		}

		public void Dispose() => ActiveSprite.Dispose();
	}
}
