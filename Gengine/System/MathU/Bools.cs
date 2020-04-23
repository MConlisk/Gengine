using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gengine.System.MathU
{
	public class Helper
	{
		public static bool IsBetween(int left, int right, int value) => (value > left && value < right);
		public static bool IsBetween(float left, float right, float value) => (value > left && value < right);
	}
}
