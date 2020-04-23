using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Gengine.System;
using Gengine.System.Sequence.Cycle;
using Gengine.System.MathU;

namespace SpaceShooterGame
{
	public partial class GameWindow : Form
	{
		private readonly BufferedGraphicsContext context;
		private BufferedGraphics grafx;

		private readonly Timer FpsDrawTimer;
		private byte FpsCounter;
		private string FpsText;
		private string CoordText;

		private DateTime GameTime = new DateTime();
		private DateTime FpsTimer = new DateTime();

		private readonly Font GameFont;
		private readonly Brush TextColor;

		private readonly List<Keys> KeysPressed = new List<Keys>();

		private static readonly string GamePath =
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		public static Bitmap PlayerImage;

		public AnimatedGameObject Player;

		public GameWindow() : base()
		{
			Width *= 2;
			Height *= 2;
			GameFont = new Font("Arial", 14);
			TextColor = Brushes.Black;
			Text = "GameWindow";
			FpsCounter = 0;

			GameTime = DateTime.Now;
			FpsTimer = GameTime + TimeSpan.FromSeconds(1);
			FpsDrawTimer = new Timer { Interval = 1 };
			FpsDrawTimer.Tick += new EventHandler(OnUpdate);
			MouseDown += new MouseEventHandler(MouseDownHandler);
			Resize += new EventHandler(OnResize);
			MouseMove += new MouseEventHandler(MouseMoveHandler);

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
			context = BufferedGraphicsManager.Current;
			context.MaximumBuffer = new Size(Width + 1, Height + 1);
			grafx = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

			InitializeSprites();

			FpsDrawTimer.Start();
			DrawToBuffer(grafx.Graphics);
		}

		

		private void InitializeSprites()
		{
			PlayerImage = new Bitmap(Path.Combine(GamePath, @"Game/Images/Fighter.png"));
			Player = new AnimatedGameObject(PlayerImage, 8, 28, true, new Plot2D(0, 0, 0), new Size());
			
			float Vres = Player.VerticalResolution / 2;
			float Hres = Player.HorizontalResolution / 2;

			Player.BuildAnim(Vres, Hres, 0, 3, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 4, 11, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 12, 15, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 16, 19, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 20, 21, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 22, 23, 5.0f, 1, CycleTypes.Forward);
			Player.BuildAnim(Vres, Hres, 24, 27, 5.0f, 1, CycleTypes.Forward);
		}

		private void MouseMoveHandler(object sender, MouseEventArgs e)
		{
			float mouseX = e.X - (Width / 2);
			float mouseY = (e.Y -(Height / 2)) * -1;
			float result = (float)((180 / Math.PI) * (Math.Atan2(mouseY, mouseX) - Math.Atan2(0,0)));
			int direction = result > 0
				? result > 20 ? result > 70 ? result > 110 ? result > 160 ? 0 : 1 : 2 : 3 : 4
				: result < -20 ? result < -70 ? result < -110 ? result < -160 ? 0 : 7 : 6 : 5 : 4;
			Player.SetDirection(d			CoordText = $"Result = { result} \n Direction={direction}";
			
		}

		private void MouseDownHandler(object sender, MouseEventArgs e)
		{ 
			switch (e.Button)
			{
				case MouseButtons.Right:
					Player.PreviousAnimation();
					break;

				case MouseButtons.Middle:
					Player.Plot.Move(e.X - Player.CurrentSprite.Width, e.Y - Player.CurrentSprite.Height);
					break;

				case MouseButtons.Left:
					Player.NextAnimation();
					break;

				default:
					Player.NextAnimation();
					break;

			}
		}

		private void OnUpdate(object sender, EventArgs e)
		{
			GameTime = DateTime.Now;
			FpsCounter++;
			if (GameTime >= FpsTimer)
			{
				FpsTimer = GameTime + TimeSpan.FromSeconds(1);
				FpsText = $"FPS:{FpsCounter}";
				FpsCounter = 0;
			}

			DrawToBuffer(grafx.Graphics);
			grafx.Render(Graphics.FromHwnd(Handle));
		}

		private void OnResize(object sender, EventArgs e)
		{
			context.MaximumBuffer = new Size(Width + 1, Height + 1);
			if (grafx != null)
			{
				grafx.Dispose();
				grafx = null;
			}
			grafx = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

			DrawToBuffer(grafx.Graphics);
			grafx.Render(Graphics.FromHwnd(Handle));
		}

		private void DrawToBuffer(Graphics g)
		{
			g.FillRectangle(Brushes.ForestGreen, 0, 0, Width, Height);
			g.DrawImage(Player.CurrentSprite, Player.Plot.X, Player.Plot.Y);
			g.DrawString(FpsText, GameFont, TextColor, 10, 7);
			g.DrawString(CoordText, GameFont, TextColor, 10, 27);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			grafx.Render(e.Graphics);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			KeysPressed.Add(e.KeyCode);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			KeysPressed.Remove(e.KeyCode);
		}
	}
}