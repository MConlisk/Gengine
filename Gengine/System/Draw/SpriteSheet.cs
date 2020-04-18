using System;
using System.Collections;
using System.Drawing;

using Gengine.System.MathU;

using PineLog;

namespace Gengine.System.Draw
{
	public class SpriteSheet
	{
		public Pinelog Log = new Pinelog();
		public Bitmap Spritesheet { get; private set; }
		public int Rows { get; private set; }
		public int Columns { get; private set; }
		public bool IsUniform { get; private set; } 
		public ArrayList Sprites { get; private set; }

		public SpriteSheet(Bitmap spritesheet, int rows, int columns, bool isUniform, ArrayList metadata = null)
		{
			Log.WriteEntry(this, $"New Sprite sheet created");
			Spritesheet = spritesheet ?? throw new ArgumentNullException(nameof(spritesheet));
			Rows = rows;
			Columns = columns;
			IsUniform = isUniform;
			Sprites = new ArrayList();
			if (isUniform) Slice(); else MetaSlice(metadata);
		}

		public void Slice()
		{
			Plot2D plot = new Plot2D();
			Size size = new Size(Spritesheet.Width / Columns, Spritesheet.Height / Rows);
			int count = 0;
			if (!IsUniform) return;
			for (int x = 0; x < Columns; x++)
			{
				for (int y = 0; y < Rows; y++)
				{
					Point point = new Point(x * size.Width, y * size.Height);
					Rectangle rectangle = new Rectangle(point, size);
					Sprites.Add(new Sprite2D(Spritesheet.Clone(rectangle, Spritesheet.PixelFormat), plot, size));
					count++;
				}
			}
			Log.WriteEntry(this, $"{Spritesheet} was cut into {count} Slices");
		}

		public void MetaSlice(ArrayList meta)
		{
			Plot2D plot = new Plot2D();
			foreach (Rectangle rect in meta)
			{
				Sprites.Add(new Sprite2D(Spritesheet.Clone(rect, Spritesheet.PixelFormat), plot, rect.Size));
			}
			Log.WriteEntry(this, $"{Spritesheet} was cut into {meta.Count} Slices");
		}
	}
}
