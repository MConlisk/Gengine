using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;
using Gengine.Core.MathU;
using Gengine.Core.Sequence;
using Gengine.Core.Sequence.Cycle;
using Pine;

namespace Gengine.Core.Draw
{
	public class SpriteSheet
	{

		public Bitmap Spritesheet { get; private set; }
		public List<Rectangle> Frames { get; private set; }
		public List<Animation_Old> Animations { get; private set; }
		public Bitmap ActiveFrame { get; private set; }
		public Sequencer Cycle { get; private set; }

		private int FrameID;  // Index for Frame in Animation
		private bool Animated;

		public float HorizonitalResolution => ActiveFrame.HorizontalResolution;
		public float VerticalResolution => ActiveFrame.VerticalResolution;

		public SpriteSheet(Bitmap spritesheet, int rows, int columns)
		{
			FrameID = 0;
			Spritesheet = spritesheet;
			Frames = GetFrames(rows, columns);
			ActiveFrame = new Bitmap(Spritesheet.Clone(Frames[FrameID], PixelFormat.Format32bppPArgb));
			Log.Write(this, $"SpriteSheet Created new method");
		}

		private List<Rectangle> GetFrames(int rows, int columns)
		{
			List<Rectangle> frames = new List<Rectangle>();
			int width = Spritesheet.Width / columns;
			int height = Spritesheet.Height / rows;
			int count = 0;

			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					count++;
					frames.Add(new Rectangle(x * width, y * height, width, height));
				}
			}
			Log.Write(this, $"created {count} Frame Rectangles from {Spritesheet}");
			return frames;
		}

		public void MakeAnimated(float fps, int max, int rate, Types cycleType)
		{
			Animated = true;
			Cycle = new Sequencer(fps, max, rate, cycleType);
			Cycle.Iterated += Cycle_Iterated;
			Cycle.Start();
		}

		private void Cycle_Iterated(object sender, SequencerEventArgs args)
		{
			ActiveFrame = Animated ?
				Spritesheet.Clone(Frames[Animations[FrameID].FrameIndexes[FrameID]], PixelFormat.Format32bppPArgb) :
				Spritesheet.Clone(Frames[FrameID], Spritesheet.PixelFormat);

			ActiveFrame.SetResolution(HorizonitalResolution / 2, VerticalResolution / 2);
		}
	}
}
