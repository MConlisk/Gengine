using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using Gengine.System.Draw;
using Gengine.System.MathU;
using Gengine.System.Sequence.Cycle;

using PineLog;

namespace Gengine.System
{
	public class AnimatedGameObject : Sprite2D
	{
		public SpriteSheet Spritesheet { get; private set; }
		public List<Animation> Animations { get; private set; }
		public int AnimationIndex { get; private set; }
		public int Direction { get; private set; }
		public float VerticalResolution { get; private set; }
		public float HorizontalResolution { get; private set; }

		public void AddAnim(Animation anim) => Animations.Add(anim);
		public Bitmap CurrentSprite => Animations[AnimationIndex].ActiveSprite;
		public void NextAnimation() => AnimationIndex = AnimationIndex + 1 <= Animations.Count - 1 ? AnimationIndex + 1 : 0;
		public void PreviousAnimation() => AnimationIndex = AnimationIndex - 1 >= 0 ? AnimationIndex - 1 : Animations.Count - 1;
		public void SetAnimation(int value) => AnimationIndex = (value >= 0 && value <= Animations.Count - 1) ? value : AnimationIndex;

		public AnimatedGameObject(Bitmap spritesheet, int rows, int columns, bool isUniform, Plot2D plot, Size size, List<Rectangle> meta = null) : base(spritesheet, plot, size) 
		{
			Stopwatch secpassed = new Stopwatch();

			secpassed.Start();
			_ = Pinelog.WriteEntry(this, $"New Game Object Creation Started");
			Spritesheet = new SpriteSheet(spritesheet, rows, columns);
			Animations = new List<Animation>();
			AnimationIndex = 0;
			Direction = 0;
			VerticalResolution = Spritesheet.ActiveFrame.VerticalResolution;
			HorizontalResolution = Spritesheet.ActiveFrame.HorizontalResolution;
			_ = Pinelog.WriteEntry(this, $"New Game Object Creation Finished, process took {secpassed.ElapsedMilliseconds} milliseconds to complete ");
			secpassed.Reset();


			secpassed.Start();
			_ = Pinelog.WriteEntry(this, $"Game Object Creation Started");
			Spritesheet = new SpriteSheet(spritesheet, rows, columns, isUniform, meta);
			Animations = new List<Animation>();
			AnimationIndex = 0;
			Direction = 0;
			VerticalResolution = Spritesheet.Sprites[0].Sprite.VerticalResolution;
			HorizontalResolution = Spritesheet.Sprites[0].Sprite.HorizontalResolution;
			_ = Pinelog.WriteEntry(this, $"Game Object Creation Finished, process took {secpassed.ElapsedMilliseconds} milliseconds to complete ");
			secpassed.Reset();


		}

		public void BuildAnim(float verticalDpi, float horizontalDpi, int start, int stop, float fps, int rate, CycleTypes cycleTypes, bool isLooping = true)
		{
			List<Bitmap> newarray = BuildArray(Spritesheet, verticalDpi, horizontalDpi, start, stop);
			Animations.Add(new Animation(newarray, fps, rate, cycleTypes, isLooping));
		}

		private List<Bitmap> BuildArray(SpriteSheet sprites, float verticalDpi, float horizontalDpi, int start, int stop)
		{
			List<Bitmap> anim = new List<Bitmap>();
			for (int i = start; i <= stop; i++)
			{
				sprites.Sprites[i].Sprite.SetResolution(horizontalDpi, verticalDpi);
				anim.Add(sprites.Sprites[i].Sprite);

			}
			return anim;
		}

		
	}
}
