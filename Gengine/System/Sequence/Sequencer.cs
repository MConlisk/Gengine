﻿using System;
using System.Collections;
using System.Drawing;
using System.Timers;

using Gengine.System.Sequence.Cycle;

namespace Gengine.System.Sequence
{
	public class Sequencer : IDisposable
	{
		public int Index { get; private set; }
		public Bitmap CurrentFrame { get; private set; }
		public ArrayList Frames { get; private set; }
		public float Fps { get; private set; }
		public CycleType CycleType { get; private set; }

		private readonly Timer timer = new Timer();

		public Sequencer(ArrayList frames, float fps, int max, int min, int rate, CycleTypes animationStyle)
		{
			Frames = frames;
			Fps = fps;
			timer.Interval = Fps;
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
			CycleType = GetCycleType(animationStyle, max, min, rate);
		}

		private CycleType GetCycleType(CycleTypes animationStyle, int max, int min, int rate)
			=> (animationStyle) switch
			{
				CycleTypes.Forward => new Forward(max, min, rate),
				CycleTypes.Backward => new Reverse(max, min, rate),
				CycleTypes.Oscillate => new Oscillation(max, min, rate),
				CycleTypes.None => null,
				_ => null
			};

		public void Start() => timer.Start();
		public void Stop() => timer.Stop();

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			CycleType.Increment();
			OnIteration();
		}
		
		public delegate void SequenceEventHandler(object sender, SequencerEventArgs args);
		public event SequenceEventHandler Iterated;
		protected virtual void OnIteration() => Iterated?.Invoke(this, new SequencerEventArgs() { CurrentFrame = (Bitmap)Frames[Index], Index = Index });

		#region IDisposable Support
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					timer.Dispose();
					CurrentFrame.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		 ~Sequencer()
		 {
		   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		   Dispose(false);
		 }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}