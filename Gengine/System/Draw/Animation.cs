using System;
using System.Collections;
using System.Collections.Generic;

using Gengine.System.Sequence;
using Gengine.System.Sequence.Cycle;

using PineLog;

namespace Gengine.System.Draw
{
	public class Animation : IDisposable
	{
		private Pinelog Log = new Pinelog();
		public Sprite2D ActiveSprite { get; private set; }
		public List<Sprite2D> Frames { get; private set; }
		public Sequencer AnimSequencer { get; private set; }
		public float Fps { get; private set; }
		public int Rate { get; private set; }
		public CycleTypes Type { get; private set; }

		public Animation(List<Sprite2D> frames, float fps, int rate, CycleTypes animLoopType)
		{
			Frames = frames;
			Type = animLoopType;
			Fps = fps;
			Rate = rate;
			AnimSequencer = new Sequencer(GetImages(Frames), Fps, Frames.Count, 0, Rate, animLoopType);
			AnimSequencer.Iterated += AnimSequencer_Iterated;
			Log.WriteEntry(this, $"New Animation created");
		}

		private void AnimSequencer_Iterated(object sender, SequencerEventArgs args) 
			=> ActiveSprite.SetBitmap(AnimSequencer.CurrentFrame);

		private ArrayList GetImages(List<Sprite2D> framelist)
		{
			ArrayList frameImages = new ArrayList();
			foreach (Sprite2D sprite in framelist)
			{
				frameImages.Add(sprite.Sprite);
			}
			return frameImages;
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
