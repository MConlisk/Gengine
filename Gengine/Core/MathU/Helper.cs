using System;
using System.Collections.Generic;
using System.Drawing;

namespace Gengine.Core.MathU
{
	public class Helper
	{
		public int ReferenceDirection(Point a, Point b)
			=> D8(a, b);
		public int ReferenceDirection(int aX, int aY, int bX, int bY)
			=> D8(new Point(aX, aY), new Point(bX, bY));
		public int CRef_Direction(Point a, Size window)
			=> CD8(a, window);
		public int CRef_Direction(int aX, int aY, int width, int height)
			=> CD8(new Point(aX, aY), new Size(width, height));

		private int D8(Point a, Point b)
		{
			float result = (float)((180 / Math.PI) * (Math.Atan2(a.Y, a.X) - Math.Atan2(b.X, b.Y)));
			return result > 0
				? result > 20 ? result > 70 ? result > 110 ? result > 160 ? 0 : 1 : 2 : 3 : 4
				: result < -20 ? result < -70 ? result < -110 ? result < -160 ? 0 : 7 : 6 : 5 : 4;
		}

		private int CD8(Point a, Size s)
		{
			Point A = PointfromCenter(a, s);
			float result = (float)((180 / Math.PI) * Math.Atan2(A.Y, A.X));
			return result > 0
				? result > 20 ? result > 70 ? result > 110 ? result > 160 ? 0 : 1 : 2 : 3 : 4
				: result < -20 ? result < -70 ? result < -110 ? result < -160 ? 0 : 7 : 6 : 5 : 4;
		}

		public Point PointfromCenter(Point a, Size s)
			=> new Point(a.X - (s.Width / 2), -1 * (a.Y - (s.Height / 2)));

		public Point PointfromCenter(int x, int y, int width, int height)
			=> new Point(x - (width / 2), -1 * (y - (height / 2)));

		public static bool IsEven(float a)
		{
			float Aa = a % 2;
			bool result = Aa > 0 ? Aa < 1 ? true : false : true;
			return result;
		}
	}
}
