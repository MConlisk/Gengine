using System;
using System.Drawing;

using Gengine.System.MathU;

namespace Gengine.System.Draw
{
	public class Sprite2D : IDisposable
	{
		public long ID { get; private set; }
		public Bitmap Sprite { get; private set; }
		public Plot2D Plot { get; private set; }
		public Size Hitbox { get; private set; }
		public Rectangle Bounds => new Rectangle((int)Plot.X, (int)Plot.Y, Hitbox.Width, Hitbox.Height);

		public Sprite2D(Bitmap bitmap, Plot2D plot, Size hitbox)
		{
			Hitbox = hitbox;
			ID = DateTime.Now.Ticks;
			Sprite = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
			Plot = plot;
		}
		public void AdjustPosition(Plot2D offset) => Plot.Adjust2D(offset);
		public void Rotate(float degree) => Plot.Rotate(degree);
		public void Transform(Plot2D transform) => Plot.Transform(transform);
		public void SetPlot(Plot2D newPlot) => Plot = newPlot;
		public void SetHitbox(Size newHitbox) => Hitbox = newHitbox;
		public void SetBitmap(Bitmap newBitmap) => Sprite = newBitmap;
		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => base.Equals(obj);
		public override int GetHashCode() => base.GetHashCode();
		public void Dispose() => Sprite.Dispose(); 
	}
}
