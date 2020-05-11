using System;
using System.ComponentModel;
using System.Drawing;

using Gengine.Core.MathU;

using Pine;

namespace Gengine.Core.Draw
{
	public class Sprite2D : IDisposable
	{
		public Bitmap Sprite { get; private set; }

		public void SetSprite(Bitmap image) => Sprite = image ?? throw new ArgumentNullException(nameof(image));

		public Sprite2D(Bitmap bitmap)
		{ 
			Sprite = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
		}

		public void Dispose() => Sprite.Dispose(); 
	}
}
