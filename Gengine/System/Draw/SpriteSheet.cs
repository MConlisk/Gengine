using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;
using Gengine.System.MathU;
using Gengine.System.Sequence;
using Gengine.System.Sequence.Cycle;
using PineLog;

namespace Gengine.System.Draw
{
	public class SpriteSheet
	{

		public Bitmap Spritesheet { get; private set; }
		public List<Rectangle> Frames { get; private set; }
		public List<Animation> Animations { get; private set; }
		public Bitmap ActiveFrame { get; private set; }
		public Rectangle HitBox { get; private set; }
		public Plot2D Plot { get; private set; }
		public Sequencer Cycle { get; private set; }

		private int AnimID = 0;  // Index for Animation Collection
		private int FrameID = 0;  // Index for Frame in Animation
		private bool Animated = false;

		public float HorizonitalResolution => ActiveFrame.HorizontalResolution;
		public float VerticalResolution => ActiveFrame.VerticalResolution;

		public int Rows { get; private set; } // not needed
		public int Columns { get; private set; } // not needed
		public bool IsUniform { get; private set; } // not needed
		public List<Sprite2D> Sprites { get; private set; } // not needed -- 12 seconds to process

		public SpriteSheet(Bitmap spritesheet, int rows, int columns)
		{
			Spritesheet = spritesheet;
			Frames = GetFrames(rows, columns);
			HitBox = Frames[FrameID];
			ActiveFrame = Spritesheet.Clone(Frames[FrameID], PixelFormat.Format32bppPArgb);
			ActiveFrame.SetResolution(HorizonitalResolution / 2, VerticalResolution / 2);
			_ = Pinelog.WriteEntry(this, $"SpriteSheet Created new method");
		}

		private List<Rectangle> GetFrames(int rows, int columns)
		{
			List<Rectangle> frames = new List<Rectangle>();
			int w = Spritesheet.Width / columns;
			int h = Spritesheet.Height / rows;
			int c = 0;

			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					c++;
					frames.Add(new Rectangle(x * w, y * h, w, h));
				}
			}
			_ = Pinelog.WriteEntry(this, $"created {c} Frame Rectangles from {Spritesheet}");
			return frames;
		}

		public void MakeAnimated(float fps, int max, int rate, CycleTypes cycleType)
		{
			Animated = true;
			Cycle = new Sequencer(fps, max, rate, cycleType);
			Cycle.Iterated += Cycle_Iterated;
		}

		private void Cycle_Iterated(object sender, SequencerEventArgs args)
		{
		}



		// old code below this line
		public SpriteSheet(Bitmap spritesheet, int rows, int columns, bool isUniform, List<Rectangle> metadata = null)
		{
			Spritesheet = spritesheet ?? throw new ArgumentNullException(nameof(spritesheet));
			Rows = rows;
			Columns = columns;
			IsUniform = isUniform;
			Sprites = new List<Sprite2D>();
			if (isUniform) Slice(); else MetaSlice(metadata);
			_ = Pinelog.WriteEntry(this, $"SpriteSheet Created old method");
		}

		public void Slice()
		{
			Plot2D plot = new Plot2D();
			Size size = new Size(Spritesheet.Width / Columns, Spritesheet.Height / Rows);
			int count = 0;
			if (!IsUniform) return;
			for (int y = 0; y < Rows; y++)
			{
				for (int x = 0; x < Columns; x++)
				{
					Point point = new Point(x * size.Width, y * size.Height);
					Rectangle rectangle = new Rectangle(point, size);
					Sprites.Add(new Sprite2D(Spritesheet.Clone(rectangle, Spritesheet.PixelFormat), plot, size));
					count++;
				}
			}
			_ = Pinelog.WriteEntry(this, $"{Spritesheet} was cut into {count} Slices");
		}

		public void MetaSlice(List<Rectangle> meta)
		{
			Plot2D plot = new Plot2D();
			foreach (Rectangle rect in meta)
			{
				Sprites.Add(new Sprite2D(Spritesheet.Clone(rect, Spritesheet.PixelFormat), plot, rect.Size));
			}
			_ = Pinelog.WriteEntry(this, $"{Spritesheet} was cut into {meta.Count} Slices");
		}
	}
}
