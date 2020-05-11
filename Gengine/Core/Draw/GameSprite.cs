using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Gengine.Core.Animation;

namespace Gengine.Core.Draw
{
	public class GameSprite
	{
		private int CurrentFrame;
		private readonly Bitmap Sheet;
		private PointF CurrentPosition;
		private bool FrameLocked = true;
		private readonly AnimationSequence Sequence;
		private readonly int Step;
		private float DpiX;
		private float DpiY;

		public PointF Dpi => new PointF(DpiX, DpiY);
		public List<RectangleF> Frames { get; private set; }
		public AnimationCollection Animations { get; private set; }

		public GameSprite(Bitmap img, int rows, int columns, float fps, int step)
		{
			CurrentPosition = new PointF(0.0f, 0.0f);
			DpiY = DpiX = 96;
			(Sheet, Step) = (img, step);
			Frames = MakeFrames(rows, columns, new SizeF(Sheet.Width / columns, Sheet.Height / rows));
			CurrentFrame = 0;
			Animations = new AnimationCollection();
			Sequence = new AnimationSequence(fps);
			Sequence.OnIterate += Sequence_OnIterate;
		}

		public void SetDpi(float xValue, float yValue) => (DpiX, DpiY) = (xValue, yValue);

		private void Sequence_OnIterate(object sender, System.EventArgs e) => Update();

		public void StopSequence() => Sequence.Stop();
		public void StartSequence() => Sequence.Start();
		public void UnlockFrames() => FrameLocked = false;
		public void LockFrames() => FrameLocked = true;
		public PointF Position => CurrentPosition;
		public RectangleF ActiveHitBox => new RectangleF(CurrentPosition.X, CurrentPosition.Y, Frames[CurrentFrame].Width, Frames[CurrentFrame].Height);
		public Bitmap ActiveFrame
		{
			get
			{
				Bitmap img = Sheet.Clone(Frames[CurrentFrame], Sheet.PixelFormat);
				img.SetResolution(DpiX, DpiY);
				return img;
			}
		}

		private List<RectangleF> MakeFrames(int rows, int columns, SizeF frameSize)
		{
			List<RectangleF> frames = new List<RectangleF>();
			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					frames.Add(
						new RectangleF(
							x: x * frameSize.Width,
							y: y * frameSize.Height,
							width: frameSize.Width,
							height: frameSize.Height));
				}
			}
			return frames;
		}

		public void SetFrame(int index, bool keepFrameActive) => (CurrentFrame, FrameLocked) = (index, keepFrameActive);
		public void SetPosition(PointF point) => CurrentPosition = point;
		public void Update()
		{
			if (!FrameLocked)
			{
				Animations.CurrentAnimation.NextFrame(Step);
				CurrentFrame = Animations.CurrentAnimation.CurrentFrame;
			}
		}
	}
}