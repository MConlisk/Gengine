using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PineLog;
using Gengine.System.Draw;
using Gengine.System.Sequence.Cycle;
using System.Diagnostics;
using Gengine.System.MathU;

namespace SpaceShooterGame
{
	public partial class Main : Form
	{


		private static readonly string GamePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		public static Bitmap FighterSheetBitmap;
		public static Pinelog Log;
		public SpriteSheet FighterSheet;
		public Animation FighterRun;
		public Animation FighterAttack;
		public Animation FighterDie;

		public Main()
		{
			Log = new Pinelog(this);

			InitializeSprites();
			InitializeComponent();

			WindowState = FormWindowState.Maximized;
			DoubleBuffered = true;
			AllowTransparency = true;
			CenterToScreen();



			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			UpdateStyles();



			//Buffer.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			//Buffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//Buffer.Graphics.CompositingQuality = CompositingQuality.HighQuality;
			//Buffer.Graphics.CompositingMode = CompositingMode.SourceOver;

		}

		private void InitializeSprites()
		{

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			_ = Pinelog.WriteEntry("Start Initializing Sprites");
			FighterSheetBitmap = new Bitmap(Path.Combine(GamePath, @"Game/Images/Fighter.png"));
			FighterSheet = new SpriteSheet(FighterSheetBitmap, 8, 28, true);
			_ = Pinelog.WriteEntry($"Initializing Sprites completed in {TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds} seconds.");
			stopwatch.Stop();
			stopwatch.Reset();

			stopwatch.Start();
			List<Bitmap> anim = new List<Bitmap>
			{   FighterSheet.Sprites[4].Sprite,
				FighterSheet.Sprites[5].Sprite,
				FighterSheet.Sprites[6].Sprite,
				FighterSheet.Sprites[7].Sprite,
				FighterSheet.Sprites[8].Sprite,
				FighterSheet.Sprites[9].Sprite,
				FighterSheet.Sprites[10].Sprite,
				FighterSheet.Sprites[11].Sprite };
			anim.Reverse();
			FighterRun = new Animation(anim, 0.5f, 1, CycleTypes.Forward);
			_ = Pinelog.WriteEntry($"Initializing Fighter Run Animation completed in {TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds} seconds.");
			stopwatch.Stop();
			stopwatch.Reset();
		}

		public void Draw()
		{

			
		}

		public new void Update()
		{

			Invalidate(true);
		}

		protected override void OnClosed(EventArgs e) // after click close
		{
			
			base.OnClosed(e);
		}

		protected override void OnLoad(EventArgs e) // 1
		{
			base.OnLoad(e);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//BufferedGraphicsContext Context = BufferedGraphicsManager.Current;

			//using (BufferedGraphics Buffer = Context.Allocate(CreateGraphics(), DisplayRectangle))
			//{
			//	Buffer.Graphics.Clear(Color.Black);
			//	Buffer.Graphics.DrawImage(FighterRun.ActiveSprite, FighterRun.Plot.X, FighterRun.Plot.Y);

			//	Buffer.Render(CreateGraphics());
			//	Buffer.Dispose();
			//}
			e.Graphics.Clear(Color.Black);
			e.Graphics.DrawImage(FighterRun.ActiveSprite, FighterRun.Plot.X, FighterRun.Plot.Y);
			this.Update();

			//base.OnPaintBackground(e);
		}

		protected override void OnPaint(PaintEventArgs e) // 2
		{
			//e.Graphics.Clear(Color.Blue);
			//e.Graphics.DrawImage(FighterRun.ActiveSprite, FighterRun.Plot.X, FighterRun.Plot.Y);
			//base.OnPaint(e);
			this.Draw();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			//base.OnKeyDown(e);
			//_ = Pinelog.WriteEntry($"onKeyDown event: KeyCode {e.KeyCode}");
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			//base.OnKeyPress(e);
			//_ = Pinelog.WriteEntry($"onKeyPress event: KeyChar {e.KeyChar}");
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			//base.OnKeyUp(e);
			//_ = Pinelog.WriteEntry($"onKeyUp event: KeyCode {e.KeyCode}");
			if (e.KeyCode == Keys.Escape) Dispose();
			if (e.KeyCode == Keys.F1) DoubleBuffered = true;
			if (e.KeyCode == Keys.F2) DoubleBuffered = false;
			if (e.KeyCode == Keys.Up) FighterRun.FpsAdjustment(0.1f);
			if (e.KeyCode == Keys.Down) FighterRun.FpsAdjustment(-0.1f);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			//base.OnMouseClick(e);
			//_ = Pinelog.WriteEntry($"onMouseClick event: Clicks {e.Clicks} ");
		}

		protected override void OnMouseMove(MouseEventArgs e) // gives X,Y value
		{
			float Xc = (e.X - Width) + (Width / 2);
			float Yc = -1 * ((e.Y - Height) + (Height / 2));

			//base.OnMouseMove(e);
			//_ = Pinelog.WriteEntry($"onMouseMove event: Actual {e.Location} From Center {Xc},{Yc} ");
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			//base.OnMouseUp(e);
			//_ = Pinelog.WriteEntry($"onMouseUP event: Button {e.Button} ");
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//base.OnMouseUp(e);
			//_ = Pinelog.WriteEntry($"onMouseDown event: Button {e.Button} ");
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			//base.OnSizeChanged(e);
		}

	}
}
