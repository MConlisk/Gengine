using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Gengine.Core.MathU;

namespace Gengine.Core.Input
{
	public class InputHandler
	{
		private readonly Helper Helper;
		private Point MoveInput;

		private readonly Dictionary<Keys, KeyEventArgs> KeyInput;
		private readonly Dictionary<MouseButtons, MouseEventArgs> ButtonInput;

		public InputHandler()
		{
			Helper = new Helper();
			MoveInput = new Point(0);

			KeyInput = new Dictionary<Keys, KeyEventArgs>();
			ButtonInput = new Dictionary<MouseButtons, MouseEventArgs>();
		}

		public void InputMove(Point location) => MoveInput = location;

		public void InputKey_Up(Keys keys) => KeyInput.Remove(keys);
		public void InputKey_Down(Keys keys, KeyEventArgs e) => KeyInput.Add(keys, e);

		public void InputClick_Up(MouseButtons button) => ButtonInput.Remove(button);
		public void InputClick_Down(MouseButtons button, MouseEventArgs e) => ButtonInput.Add(button, e);

		public void Update()
		{
			Helper.ReferenceDirection(MoveInput, new Point(0));
		}

	}
}
