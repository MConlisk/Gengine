using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using Gengine.Core.Draw;
using Gengine.Core.MathU;
using Gengine.Core.Sequence.Cycle;

using Pine;

namespace Gengine.Core
{
	public class old_GameObject : Sprite2D
	{
		public Plot2D Plot { get; private set; }
		public int Rows { get; private set; }
		public int Columns { get; private set; }

		public SpriteSheet Spritesheet { get; private set; }
		public List<Animation_Old> Animations { get; private set; }
		public int AnimationIndex { get; private set; }
		private int direction = 0;
		private int GetDirection()
		{
			int newIndex = DirectionalObject ? (Animations.Count * (direction + 1)) - (Animations.Count - AnimationIndex) : AnimationIndex + 1;
			if (newIndex >= Animations.Count) newIndex = 0;
			return newIndex;
		}

		public bool DirectionalObject { get; set; }
		public float VerticalResolution { get; private set; }
		public float HorizontalResolution { get; private set; }


		public Bitmap CurrentSprite => Sprite;

		public void SetSprite(int index = 0)
		{
			Bitmap b = Spritesheet.Spritesheet.Clone(Spritesheet.Frames[index], PixelFormat.Format32bppPArgb);
			b.SetResolution(VerticalResolution * 0.80f, HorizontalResolution * 0.80f);
			if (DirectionalObject) 
			SetSprite(b);
		}

		public void SetAnimation(int value) => AnimationIndex = (value >= 0 && value <= Animations.Count) ? value : AnimationIndex;
		public void SetDirection(int value) => direction = value;

		public old_GameObject(Bitmap spritesheet, int rows, int columns) : base(spritesheet)
		{
			Plot = new Plot2D();

			Stopwatch ElapsedSeconds = new Stopwatch();
			ElapsedSeconds.Start();
			Log.Write(this, $"New Game Object Creation Started");

			AnimationIndex = 0;
			direction = 0;
			Rows = rows;
			Columns = columns;

			DirectionalObject = false;
			Spritesheet = new SpriteSheet(spritesheet, Rows, Columns);
			Animations = new List<Animation_Old>();

			VerticalResolution = Spritesheet.ActiveFrame.VerticalResolution;
			HorizontalResolution = Spritesheet.ActiveFrame.HorizontalResolution;
			Log.Write(this, $"New Game Object Creation Finished, process took {ElapsedSeconds.ElapsedMilliseconds} milliseconds to complete ");
			ElapsedSeconds.Reset();
		}

		public void BuildAnim(int start, int stop, float fps, int rate, Types cycleTypes, bool isLooping = true)
		{
			List<int> newarray = BuildArray(start, stop);
			Animations.Add(new Animation_Old(newarray, fps, rate, cycleTypes, isLooping));
			AnimationIndex = Animations.Count - 1;
		}

		private List<int> BuildArray(int start, int stop)
		{
			List<int> anim = new List<int>();
			for (int i = start; i <= stop; i++)
			{
				anim.Add(i);
			}
			return anim;
		}

	}
}
