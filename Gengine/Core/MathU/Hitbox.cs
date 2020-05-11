using System.Drawing;
using System.Windows;

namespace Gengine.Core.MathU
{
	public class Hitbox
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Thickness Padding { get; private set; }

		public Hitbox(int width, int height, Thickness padding)
		{
			Width = width;
			Height = height;
			Padding = padding;
		}

		public Hitbox(Size size, Thickness padding)
		{
			Width = size.Width;
			Height = size.Height;
			Padding = padding;
		}

		public Size Size => new Size(Width, Height);

		public void NoPad() => Padding = new Thickness(0.0f);
		public void NewPad(Thickness padding) => Padding = padding;
	}
}
