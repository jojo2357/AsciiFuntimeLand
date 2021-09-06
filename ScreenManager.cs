using System;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace AsciiFuntimeLand
{
	public partial class ScreenManager : Form
	{
		public bool paused;
		public static readonly Point ZeroPoint = new Point(0, 0);

		private readonly callInvoke _callInvoke;
		public readonly SolidBrush _drawBrush;
		public readonly Font _drawFont;
		public World currentWorld = new RealWorld("main");
		public World realWorld = World.worlds["main"];

		public StringFormat drawFormat = new StringFormat();
		
		public bool open = true;
		private bool redrawRequested;
		public int sleepAmount = 10;
		public StringBuilder todraw;

		public ScreenManager()
		{
			new MenuWorld("menu");
			new BouncySphere(currentWorld, new Vector3(-25, -25, 25), 5);
			new Sphere(currentWorld, new Vector3(10, 10, 10), 5);
			new RenderablePlane(currentWorld, new SquareFace('@', new Vector3(255, 255, 0), new Vector3(-255, 255, 0), new Vector3(-255, -255, 0)));
			new RenderableRectangularPrism(currentWorld, '!', new Vector3(2, 2, 2), new Vector3(2, 4, 2), new Vector3(4, 2, 2), new Vector3(2, 2, 4));
			new RenderableWall(currentWorld, '@', new SquarePlane(new Vector3(255, 255, 0), new Vector3(-255, 255, 0), new Vector3(-255, 255, 10)), 255, 0, 0);
			new RenderableWall(currentWorld, '@', new SquarePlane(new Vector3(255, 255, 0), new Vector3(255, -255, 0), new Vector3(255, -255, 10)), 0, 255, 0);
			new RenderableWall(currentWorld, '@', new SquarePlane(new Vector3(255, -255, 0), new Vector3(-255, -255, 0), new Vector3(-255, -255, 10)), 0, 0, 255);
			new RenderableWall(currentWorld, '@', new SquarePlane(new Vector3(-255, 255, 0), new Vector3(-255, -255, 0), new Vector3(-255, -255, 10)), 255, 100, 0);
			new Hitcube(currentWorld, new RectangularPrism(new Vector3(255, 255, 0), new Vector3(-255, 255, 0), new Vector3(255, -255, 0), new Vector3(255, 255, -1)));
			InitializeComponent();
			BackColor = Color.Black;
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
			currentWorld.HandleUpdates();
			if (redrawRequested)
			{
				RenderController.PrepareRaytraceRendering();
				Invoke(_callInvoke);
			}

			redrawRequested = false;
		}

		private void KeydownListener(object sender, KeyEventArgs e)
		{
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
			RenderController.PostRendering(this, CreateGraphics());
		}

		private void DoPaint(object o, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
			RenderController.DoRendering(this, e.Graphics);
			RenderController.PostRendering(this, e.Graphics);
		}

		public void DrawString(string str, Point loc, Graphics formGraphics, Font drawFont, Color color)
		{
			TextRenderer.DrawText(formGraphics, str, drawFont, loc, color);
		}

		public void DrawString(string str, Point loc, Graphics formGraphics, Font drawFont, Color color, Color backColor)
		{
			TextRenderer.DrawText(formGraphics, str, drawFont, loc, color, backColor);
		}

		public void RequestRedraw()
		{
			redrawRequested = true;
		}

		public void requestClose()
		{
			open = false;
		}

		public void requestPause(bool pause)
		{
			if (pause)
				currentWorld = World.worlds["menu"];
			else
				currentWorld = realWorld;
			paused = pause;
		}

		private delegate void callInvoke();
	}
}