using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

using Gengine.Core.Draw;
using Gengine.Core.Input;
using Gengine.Core.Animation;

using Gengine.Core.MathU;

using Pine;
using System.Threading.Tasks;

namespace SpaceShooterGame
{
	public partial class GameWindow : Form
	{
		#region Declaration
		private const int DrawPadding = 50;
		private static BufferedGraphicsContext context;
		private static BufferedGraphics grafx;

		private static readonly Timer UpdateTimer = new Timer { Interval = 1 };
		private static readonly Stopwatch Watch = new Stopwatch();
		private static readonly InputHandler GameInput = new InputHandler();

		private static readonly Font GameFont = new Font("Games", 14);
		private static readonly Brush TextColor = Brushes.White;

		private static readonly string GamePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

		private Rectangle ViewRect;

		private DateTime GameTime = new DateTime();
		private DateTime FpsTime = new DateTime();
		private int FpsCounter = 0;
		private string FpsText = null;
		private GameSprite Player;
		private GameSprite BackgroundTiles;
		public List<GameSprite> MainLayer { get; private set; }
		public List<GameSprite> BackLayer { get; private set; }
		public List<GameSprite> ForeLayer { get; private set; }
		public void AddSprite(GameSprite obj) => MainLayer.Add(obj);
		public void AddBackground(GameSprite obj) => BackLayer.Add(obj);

		#endregion

		#region Initialization

		public GameWindow(Size size, string title) : base()
		{
			Log.Write(this, $"Initializing Surface Started");
			Watch.Start();

			InitializeTime();
			InitializeEvents();
			InitializeSprites();
			InitializeSurface(title, size);

			Watch.Stop();
			Log.Write(this, $"Initialization Finished, process took {Watch.ElapsedMilliseconds} milliseconds to complete ");
		}

		private void InitializeTime()
		{
			Log.Write(this, $"Initializing Timers");

			GameTime = DateTime.Now;
			FpsCounter = 0;
			FpsTime = GameTime + TimeSpan.FromSeconds(1);
			UpdateTimer.Tick += new EventHandler(OnUpdate);
			UpdateTimer.Start();
		}

		private void InitializeEvents()
		{
			Log.Write(this, $"Initializing Events");

			MouseDown += new MouseEventHandler(MouseDownHandler);
			MouseUp += new MouseEventHandler(MouseUpHandler);
			KeyDown += new KeyEventHandler(OnKeyDown);
			KeyUp += new KeyEventHandler(OnKeyUp);
			Resize += new EventHandler(OnResize);
			MouseMove += new MouseEventHandler(MouseMoveHandler);
		}

		private void InitializeSprites()
		{
			Log.Write(this, $"Initializing Sprites");
			Bitmap PlayerImage = new Bitmap(Path.Combine(GamePath, @"Game/Images/Fighter.png"));
			MainLayer = new List<GameSprite>();
			BackLayer = new List<GameSprite>();
			ForeLayer = new List<GameSprite>();

			Player = new GameSprite(
				PlayerImage, 8, 28, 15, 1);

			for (int d = 0; d < 8; d++)
			{
				int D = (d + 1) * 28;
				Player.Animations.Add(new GameAnimation($"Stand{d}", BuildListInt(D - (28 - 0), D - (28 - 3))));
				Player.Animations.Add(new GameAnimation($"Walk{d}", BuildListInt(D - (28 - 4), D - (28 - 11))));
				Player.Animations.Add(new GameAnimation($"Attack1{d}", BuildListInt(D - (28 - 12), D - (28 - 15))));
				Player.Animations.Add(new GameAnimation($"Attack2{d}", BuildListInt(D - (28 - 16), D - (28 - 19))));
				Player.Animations.Add(new GameAnimation($"Block{d}", BuildListInt(D - (28 - 20), D - (28 - 21))));
				Player.Animations.Add(new GameAnimation($"Hit{d}", BuildListInt(D - (28 - 22), D - (28 - 23))));
				Player.Animations.Add(new GameAnimation($"Death{d}", BuildListInt(D - (28 - 24), D - (28 - 27))));
			}
			Player.UnlockFrames();
			Player.Animations.SetAnimation("Walk0");
			Player.SetPosition(
				new PointF(
					(Width / 2) - Player.ActiveFrame.Width,
					(Height / 2) - Player.ActiveFrame.Height));
			Player.StartSequence();
			MainLayer.Add(Player);
			BuildWorld(5, 3);
		}

		private List<int> BuildListInt(int start, int end)
		{
			List<int> al = new List<int>();
			for (int i = start; i <= end; i++) { al.Add(i); }
			return al;
		}

		private void BuildWorld(int mapWidth, int mapHeight)
		{
			int row = 12;
			int col = 8;
			Random rnd = new Random();
			Bitmap ForestTiles = new Bitmap(Path.Combine(GamePath, @"Game/Images/SeasonalForestTiles.png"));
			BackgroundTiles = new GameSprite(
				ForestTiles, row, col, 0, 0);

			(int x, int y, int frame)[] WorldMap = new (int, int, int)[mapWidth * mapHeight];
			(int w, int h) TileSize = (ForestTiles.Width / col, ForestTiles.Height / row);
			(float Height, float Width) Center = (TileSize.h / 2, TileSize.w / 2);
			(int X, int Y, int Height) TilePosition = (0, 0, 0);


			Log.Write(this, $"Begin world creation");

			for (int count = 0; count <= (mapHeight * mapWidth) - 1; count++)
			{
				Log.Write(this, $"WorldMap:{count}/{mapHeight * mapWidth} = " +
					$"Height Position:{TilePosition.Height}, " +
					$"X:{TilePosition.X}, " +
					$"Y:{TilePosition.Y}");

				float posY = TilePosition.Height;
				if (TilePosition.X >= mapWidth - 1) { TilePosition.X = 0; TilePosition.Y++; }


				WorldMap[count] = (TilePosition.X == 0 || TilePosition.Y == 0) ? (mapWidth, mapHeight, 0)
					: (TilePosition.X == mapWidth || TilePosition.Y == mapHeight) ? (mapWidth, mapHeight, 0)
					: (TilePosition.X, TilePosition.Y, rnd.Next(1, mapWidth));


				float posX = Helper.IsEven(TilePosition.Y) ? (TileSize.w * TilePosition.X) : (TileSize.w * TilePosition.X) + Center.Width;
				PointF BackPos = new PointF(posX, posY);

				// get Random tile
				BackgroundTiles.SetFrame(rnd.Next(7 * 8, 9 * 8), true);
				Bitmap backImg = (Bitmap)BackgroundTiles.ActiveFrame.GetThumbnailImage(TileSize.w, TileSize.h, null, IntPtr.Zero);

				// create Sprite

				GameSprite background = new GameSprite(backImg, 1, 1, 0, 0);
				background.SetPosition(BackPos);
				//background.SetDpi(background.Dpi.X * 1.5f, background.Dpi.Y * 1.5f);
				AddBackground(background);

				//}
				TilePosition.X++;
				TilePosition.Height += (int)Center.Height;
			}

			//);
			Log.Write(this, $"End Parallel Map Creation");
		}

		private void InitializeSurface(string title, Size windowSize)
		{
			ViewRect = new Rectangle(new Point(0), windowSize);

			context = BufferedGraphicsManager.Current;
			context.MaximumBuffer = new Size(ViewRect.Width + 1, ViewRect.Height + 1);
			grafx = context.Allocate(CreateGraphics(), ViewRect);

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

			//grafx.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Text = title;
			Size = ViewRect.Size;

			//DrawToBuffer(grafx.Graphics);
		}

		#endregion

		#region Event

		private void OnResize(object sender, EventArgs e)
		{
			foreach (GameSprite sprite in MainLayer)
			{

			}

			context.MaximumBuffer = new Size(Width + 1, Height + 1);
			if (grafx != null)
			{
				grafx.Dispose();
				grafx = null;
			}
			grafx = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

			//DrawToBuffer(grafx.Graphics);
			//grafx.Render(Graphics.FromHwnd(Handle));
		}

		#endregion

		#region Update

		private void OnUpdate(object sender, EventArgs e)
		{
			GameTime = DateTime.Now;
			if (GameTime >= FpsTime)
			{
				FpsTime = GameTime + TimeSpan.FromSeconds(1);
				FpsText = $"FPS:{FpsCounter}";
				FpsCounter = 0;
			}

			DrawToBuffer(grafx.Graphics);

		}

		#endregion

		#region Draw

		private void DrawToBuffer(Graphics g)
		{
			FpsCounter++;
			g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

			foreach (GameSprite sprite in BackLayer)
			{
				if (sprite.Position.X > -1 * DrawPadding && sprite.Position.X < Width + DrawPadding)
				{
					if (sprite.Position.Y > -1 * DrawPadding && sprite.Position.Y < Height + DrawPadding)
					{
						g.DrawImage(sprite.ActiveFrame, sprite.Position);
					}
				}
			}
			foreach (GameSprite sprite in MainLayer)
			{
				if (sprite.Position.X > -1 * DrawPadding && sprite.Position.X < Width + DrawPadding)
				{
					if (sprite.Position.Y > -1 * DrawPadding && sprite.Position.Y < Height + DrawPadding)
					{
						g.DrawImage(sprite.ActiveFrame, sprite.Position);
					}
				}
			}
			foreach (GameSprite sprite in ForeLayer)
			{
				if (sprite.Position.X > -1 * DrawPadding && sprite.Position.X < Width + DrawPadding)
				{
					if (sprite.Position.Y > -1 * DrawPadding && sprite.Position.Y < Height + DrawPadding)
					{
						g.DrawImage(sprite.ActiveFrame, sprite.Position);
					}
				}
			}

			g.DrawString(FpsText, GameFont, TextColor, 10, 7);

			grafx.Render(Graphics.FromHwnd(Handle));
			context.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{

			//grafx.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			//grafx.Render(Graphics.FromHwnd(Handle));
			//Log.Write(this, $"OnPaint called, SmoothingMode = {grafx.Graphics.SmoothingMode}");
		}

		#endregion

		#region INPUT

		private void MouseMoveHandler(object sender, MouseEventArgs e) => GameInput.InputMove(new Point(e.X, e.Y));
		private void MouseDownHandler(object sender, MouseEventArgs e) => GameInput.InputClick_Down(e.Button, e);
		private void MouseUpHandler(object sender, MouseEventArgs e) => GameInput.InputClick_Up(e.Button);
		private void OnKeyDown(object sender, KeyEventArgs e) => GameInput.InputKey_Down(e.KeyCode, e);
		private void OnKeyUp(object sender, KeyEventArgs e) => GameInput.InputKey_Up(e.KeyCode);

		#endregion



	}
}

