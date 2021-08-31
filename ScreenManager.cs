using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AsciiFuntimeLand
{
	public partial class ScreenManager : Form
	{
		public static bool paused;
		public static readonly Point ZeroPoint = new Point(0, 0);
		public readonly SolidBrush _drawBrush;
		public readonly Font _drawFont;

		private readonly callInvoke _callInvoke;
		public Camera cam;
		public StringFormat drawFormat = new StringFormat();
		public bool open = true;
		public StringBuilder todraw;
		
		private MouseManager ioManager = new MouseManager();
		private bool redrawRequested = false;
		public int sleepAmount = 10;
		public World currentWorld = new World();

		public ScreenManager()
		{
			InitializeComponent();
			BackColor = Color.Black;
			cam = new Camera();
			Paint += DoPaint;
			_drawFont = new Font(FontFamily.GenericMonospace, 8);
			_drawBrush = new SolidBrush(Color.White);
			Size = new Size(600, 600);
			KeyPreview = true;
			KeyDown += KeydownListener;
			_callInvoke = Refresh;
			SetStyle(ControlStyles.UserPaint |
			         ControlStyles.AllPaintingInWmPaint |
			         ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		public void UpdateInput()
		{
			ioManager.ManageSelf();
			if (redrawRequested)
			{
				RenderController.PrepareRendering();
				Invoke(_callInvoke);
			}
			redrawRequested = false;
		}

		private void KeydownListener(object sender, KeyEventArgs e)
		{
			ioManager.lastKeyArgs = e;
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);

			open = false;

			_drawFont.Dispose();
			_drawBrush.Dispose();
			RenderController.Dispose();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			RenderController.DoRendering(this, CreateGraphics());
		}

		private void DoPaint(object o, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
			RenderController.DoRendering(this, e.Graphics);
			//DrawString(e.Graphics);
		}

		public void DrawString(string str, Point loc, Graphics formGraphics, Font drawFont, Color color)
		{
			TextRenderer.DrawText(formGraphics, str, drawFont, loc, color);
		}

		public void HandleMouseMovement(Point p)
		{
			if (p.Equals(ZeroPoint))
				return;
			cam.AddLooking(new Vector2(p.X, -p.Y));
			Console.WriteLine(cam.Looking + " and " + cam.getLookingNoRot());
			/*RenderController.PrepareRendering();
			Invoke(_callInvoke);*/
			RequestRedraw();
		}

		public void RequestRedraw()
		{
			this.redrawRequested = true;
		}

		private delegate void callInvoke();

		public void requestClose()
		{
			this.open = false;
		}

		public void requestPause(bool pause)
		{
			paused = pause;
		}
	}
}