using System;
using System.Timers;

using Gengine.Core.Sequence.Cycle;

namespace Gengine.Core.Sequence
{
	public class Sequencer : IDisposable
	{
		public int CurrentFrame { get; private set; }
		public float Fps { get; private set; }
		public CycleType CycleType { get; private set; }
		public bool IsLooping { get; private set; }
		public int MaxFrame { get; private set; }

		private readonly Timer timer = new Timer();

		public Sequencer(float fps, int max, int rate, Types animationStyle, bool isLooping = true)
		{
			Fps = fps;
			MaxFrame = max;
			IsLooping = isLooping;
			timer.Interval = 1000 / Fps;
			CycleType = GetCycleType(animationStyle, max, rate);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
		}

		private CycleType GetCycleType(Types animationStyle, int max, int rate)
			=> (animationStyle) switch
			{
				Types.Forward => new Forward(max, rate),
				Types.Backward => new Reverse(max, rate),
				Types.Oscillate => new Oscillation(max, rate),
				Types.None => new Forward(max, rate),
				_ => new Forward(max, rate)
			};

		public void Start() => timer.Start();
		public void Stop() => timer.Stop();
		public void Restart()
		{
			timer.Stop();
			CycleType.Value = 0;
			CurrentFrame = CycleType.Value;
			timer.Start();
		}
		public void AdjustFps(float value)
		{
			Fps = 1000 / (Fps + value) <= 0.1f ? 0.1f * 1000 : Fps + value;
			timer.Stop();
			timer.Interval = 1000 / Fps;
			timer.Start();
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!(!IsLooping && CycleType.Value == MaxFrame - 1))
			{ 
				CycleType.Increment();
				CurrentFrame = CycleType.Value;
			}
			OnIteration();
		}

		public delegate void SequenceEventHandler(object sender, SequencerEventArgs args);
		public event SequenceEventHandler Iterated;
		protected virtual void OnIteration() => Iterated?.Invoke(this, new SequencerEventArgs() { });

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
