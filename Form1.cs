using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;

namespace AsciiFuntimeLand
{
	public partial class Form1 : Form
	{
		[DllImport("gdi32.dll", EntryPoint = "ExtTextOutW")]
		static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions,
			[In] ref Rectangle lprc, [MarshalAs(UnmanagedType.LPWStr)] string lpString,
			uint cbCount, [In] IntPtr lpDx);

		private delegate void callInvoke();

		private callInvoke _callInvoke;
		private bool open = true;
		public static bool paused = false;
		public Camera cam;
		public readonly Font _drawFont;
		public readonly SolidBrush _drawBrush;
		public StringBuilder todraw;
		public System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
		public static readonly Point ZeroPoint = new Point(0, 0);

		public Form1()
		{
			InitializeComponent();
			BackColor = Color.Black;
			cam = new Camera();
			base.Paint += DoPaint;
			_drawFont = new System.Drawing.Font(FontFamily.GenericMonospace, 8);
			_drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
			this.Size = new Size(600, 600);
			KeyPreview = true;
			this.KeyDown += Form1_KeyDown;
			_callInvoke = Refresh;
			SetStyle(System.Windows.Forms.ControlStyles.UserPaint |
			         System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
			         System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
				true);
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

			RenderController.DoRendering(this, this.CreateGraphics());
		}

		private void DoPaint(Object o, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
			RenderController.DoRendering(this, e.Graphics);
			//DrawString(e.Graphics);
		}

		public void DrawString(Graphics formGraphics)
		{
			todraw = new StringBuilder();
			for (int y = 0; y < Size.Height; y += 4)
			{
				for (int x = 0; x < this.Size.Width; x += 4)
				{
					char traceResult = RenderableObject.RaytraceAll(cam,
						new Vector2((float) ((x - Size.Width / 2.0) / Size.Width * 180),
							(float) ((y - Size.Height / 2.0) / Size.Height * 180)));
					todraw.Append(traceResult);
					/*if (traceResult != ' ')
						DrawString(
							"" + traceResult,
							new Point(x, y), formGraphics, _drawFont, _drawBrush, drawFormat);*/
				}

				todraw.Append('\n');
			}

			DrawString(todraw.ToString(), ZeroPoint, formGraphics, _drawFont, Color.White);
			paused = false;
		}

		public void DrawString(string str, Point loc, Graphics formGraphics, Font drawFont, Color color)
		{
			TextRenderer.DrawText(formGraphics, str, drawFont, loc, color);
		}

		public bool IsOpen()
		{
			return open;
		}

		public void HandleMouseMovement(Point p)
		{
			if (p.Equals(ZeroPoint))
				return;
			cam.AddLooking(new Vector2(p.X, -p.Y));
			Console.WriteLine(cam.Looking + " and " + cam.getLookingNoRot());
			RenderController.PrepareRendering(this);
			Invoke(_callInvoke);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					e.SuppressKeyPress = true;
					open = false;
					break;
				case Keys.P:
					e.SuppressKeyPress = true;
					paused = true;
					break;
				case Keys.W:
					e.SuppressKeyPress = true;
					cam.coords = new Vector3(cam.coords.X + cam.getLooking().X, cam.coords.Y + cam.getLooking().Y, cam.coords.Z);
					RenderController.PrepareRendering(this);
					Invoke(_callInvoke);
					break;
				case Keys.S:
					e.SuppressKeyPress = true;
					cam.coords = new Vector3(cam.coords.X - cam.getLooking().X, cam.coords.Y - cam.getLooking().Y, cam.coords.Z);
					RenderController.PrepareRendering(this);
					Invoke(_callInvoke);
					break;
				case Keys.A:
					e.SuppressKeyPress = true;
					cam.coords = new Vector3(cam.coords.X + cam.getLooking(-90).X, cam.coords.Y + cam.getLooking(-90).Y, cam.coords.Z);
					RenderController.PrepareRendering(this);
					Invoke(_callInvoke);
					break;
				case Keys.D:
					e.SuppressKeyPress = true;
					cam.coords = new Vector3(cam.coords.X + cam.getLooking(90).X, cam.coords.Y + cam.getLooking(90).Y, cam.coords.Z);
					RenderController.PrepareRendering(this);
					Invoke(_callInvoke);
					break;
			}
		}
	}
}