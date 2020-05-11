using System;
using System.Timers;

namespace Gengine.Core.Animation
{
	public class AnimationSequence : IDisposable
	{
		private float Fps { get; set; }
		private Timer FpsTimer { get; set; }

		public AnimationSequence(float fps)
		{
			Fps = fps;
			if (fps > 0)
			{
				FpsTimer = new Timer(1000 / Fps);
				FpsTimer.Elapsed += TimerIterate;
				FpsTimer.Enabled = true;
			}
		}

		public void Start() { FpsTimer.Start(); }
		public void Stop() { FpsTimer.Stop(); }
		public void Dispose() { FpsTimer.Dispose(); }

		public event EventHandler OnIterate;
		protected virtual void TimerIterate(object sender, ElapsedEventArgs e) => OnIterate?.Invoke(this, e);
	}
}