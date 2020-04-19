using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using PineLog;
using Gengine.System.Draw;
using Gengine.System.Sequence.Cycle;
using System.Diagnostics;

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
		}

		private void InitializeSprites()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			_ = Pinelog.WriteEntry("Start Initializing Sprites");
			FighterSheetBitmap = new Bitmap(Path.Combine(GamePath, @"Game/Images/Fighter.png"));
			FighterSheet = new SpriteSheet(FighterSheetBitmap, 8, 32, true);
			_ = Pinelog.WriteEntry($"Initializing Sprites completed in {TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds} seconds.");
			//stopwatch.Stop();
			stopwatch.Reset();
			//stopwatch.Start();
			List<Sprite2D> anim = new List<Sprite2D>
			{	FighterSheet.Sprites[4],
				FighterSheet.Sprites[5],
				FighterSheet.Sprites[6],
				FighterSheet.Sprites[7],
				FighterSheet.Sprites[8],
				FighterSheet.Sprites[9],
				FighterSheet.Sprites[10],
				FighterSheet.Sprites[11] };

			FighterRun = new Animation(anim, 3, 1, CycleTypes.Forward);
			_ = Pinelog.WriteEntry($"Initializing Fighter Run Animation completed in {TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds} seconds.");
			stopwatch.Stop();
			stopwatch.Reset();
		}


		public void Draw(Graphics g)
		{
			g.Clear(Color.Black);
			g.DrawImage(FighterRun.ActiveSprite.Sprite, FighterRun.ActiveSprite.Plot.X, FighterRun.ActiveSprite.Plot.Y);
			
		}



		public new void Update()
		{

		}



		protected override void OnClosed(EventArgs e) // after click close
		{
			base.OnClosed(e);
		}

		protected override void OnLoad(EventArgs e) // 1
		{
			base.OnLoad(e);
		}

		protected override void OnPaint(PaintEventArgs e) // 2
		{
			base.OnPaint(e);
			e.Graphics.Flush();
			Draw(e.Graphics);

			Invalidate(); 
		}



		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			_ = Pinelog.WriteEntry($"onKeyDown event: KeyCode {e.KeyCode}");
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			_ = Pinelog.WriteEntry($"onKeyPress event: KeyChar {e.KeyChar}");
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			_ = Pinelog.WriteEntry($"onKeyUp event: KeyCode {e.KeyCode}");
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			_ = Pinelog.WriteEntry($"onMouseClick event: Clicks {e.Clicks} ");
		}

		protected override void OnMouseMove(MouseEventArgs e) // gives X,Y value
		{
			float Xc = (e.X - Width) + (Width /2);
			float Yc = -1 * ((e.Y - Height) + (Height / 2));

			base.OnMouseMove(e);
			_ = Pinelog.WriteEntry($"onMouseMove event: Actual {e.Location} From Center {Xc},{Yc} ");
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_ = Pinelog.WriteEntry($"onMouseUP event: Button {e.Button} ");
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_ = Pinelog.WriteEntry($"onMouseDown event: Button {e.Button} ");
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
		}
	}
}
