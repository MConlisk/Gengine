using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gengine.System.Sequence;
using Gengine.System.Sequence.Cycle;

using PineLog;

namespace Gengine.System.Draw
{
	public class Animation : IDisposable
	{
		public Sprite2D ActiveSprite { get; private set; }
		public List<Sprite2D> Frames { get; private set; }
		public Sequencer AnimSequencer { get; private set; }
		public float Fps { get; private set; }
		public int Rate { get; private set; }
		public CycleTypes Type { get; private set; }

		public Animation(List<Sprite2D> frames, float fps, int rate, CycleTypes animLoopType)
		{
			Pinelog Log = new Pinelog(this);
			_ = Pinelog.WriteEntry($"New Animation created");
			Frames = frames;
			ActiveSprite = frames[0];
			Type = animLoopType;
			Fps = fps;
			Rate = rate;
			AnimSequencer = new Sequencer(Fps, Frames.Count, 0, Rate, animLoopType);
			AnimSequencer.Iterated += AnimSequencer_Iterated;
		}

		private void AnimSequencer_Iterated(object sender, SequencerEventArgs args)
		{
			_ = Pinelog.WriteEntry($"Animation Iteration: new Frame ID:{AnimSequencer.CurrentFrame}");
			ActiveSprite.SetBitmap(Frames[AnimSequencer.CurrentFrame].Sprite);
		}

		public void SetState(AnimationState state)
		{
			switch (state)
			{
				case AnimationState.Activate:
					AnimSequencer.Start();
					break;

				case AnimationState.Stop:
					AnimSequencer.Stop();
					break;

				default:
					break;

			};
		}

		public void Dispose() => ActiveSprite.Dispose();
	}
}
