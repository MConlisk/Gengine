using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using PineLog;
using Gengine.System.Draw;
using System.Diagnostics;

namespace SpaceShooterGame
{
	public partial class Main : Form
	{
		private static readonly string GamePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		public static Bitmap FighterSheetBitmap;
		public Pinelog Log = new Pinelog();
		public SpriteSheet FighterSheet; 
		public Animation FighterRun;
		public Animation FighterAttack;
		public Animation FighterDie;
		public Main()
		{
			InitializeComponent();
			InitializeSprites();
		}

		private void InitializeSprites()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Log.WriteEntry(this, "Start Initializing Sprites");
			FighterSheetBitmap = new Bitmap(Path.Combine(GamePath, @"Game/Images/Fighter.png"));
			FighterSheet = new SpriteSheet(FighterSheetBitmap, 8, 32, true);
			Log.WriteEntry(this, $"Initializing Sprites completed in {stopwatch.ElapsedMilliseconds} milliseconds.");
			stopwatch.Stop();
		}


		public void Draw(Graphics g)
		{
			g.Clear(Color.Black);
			g.DrawImage(FighterSheet.Sprites[2].Sprite, FighterSheet.Sprites[2].Plot.X,FighterSheet.Sprites[2].Plot.Y);
			
		}



		public new void Update()
		{

		}



		protected override void OnClosed(EventArgs e) // after click close
		{
			base.OnClosed(e);
			Log.WriteEntry(Text, $"OnClosed event: EventArgs {e}");
		}

		protected override void OnLoad(EventArgs e) // 1
		{
			base.OnLoad(e);
			Log.WriteEntry(Text, $"onLoad event: EventArgs {e}");
		}

		protected override void OnPaint(PaintEventArgs e) // 2
		{
			base.OnPaint(e);
			e.Graphics.Flush();
			Draw(e.Graphics);

			Invalidate(); // caused onPaint to loop and still finishes OnPaint event
			//Refresh();
		}

		protected override void OnValidated(EventArgs e) // ??
		{
			Log.WriteEntry(Text, $"onValidated event: EventArgs {e}");
			base.OnValidated(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			Log.WriteEntry(Text, $"onKeyDown event: KeyCode {e.KeyCode}");
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			Log.WriteEntry(Text, $"onKeyPress event: KeyChar {e.KeyChar}");
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			Log.WriteEntry(Text, $"onKeyUp event: KeyCode {e.KeyCode}");
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			Log.WriteEntry(Text, $"onMouseClick event: Clicks {e.Clicks} ");
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			Log.WriteEntry(Text, $"onMouseHover event: EventArgs {e}");
		}

		protected override void OnMouseMove(MouseEventArgs e) // gives X,Y value
		{
			float Xc = (e.X - Width) + (Width /2);
			float Yc = -1 * ((e.Y - Height) + (Height / 2));

			base.OnMouseMove(e);
			Log.WriteEntry(Text, $"onMouseMove event: Actual {e.Location} From Center {Xc},{Yc} ");
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Log.WriteEntry(Text, $"onMouseUP event: Button {e.Button} ");
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Log.WriteEntry(Text, $"onMouseDown event: Button {e.Button} ");
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			Log.WriteEntry(Text, $"onSizeChanged event: New Size W={Width} H={Height} - EventArgs {e}");
		}





	}
}
