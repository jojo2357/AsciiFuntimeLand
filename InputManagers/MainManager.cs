using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static AsciiFuntimeLand.Program;

namespace AsciiFuntimeLand
{
	public class MainManager : InputManager
	{
		private Point _pos;

		public MainManager()
		{
		}

		public void ManageSelf()
		{
			ManageKeyboard();
			ManageMouse();
		}

		protected override void ManageMouse()
		{
			_pos = Cursor.Position;
			Cursor.Hide();
			Cursor.Position = screenManager.Location + Util.Divide(screenManager.Size, 2);
			Point p = new Point(_pos.X - (screenManager.Location.X + screenManager.Size.Width / 2), _pos.Y - (screenManager.Location.Y + screenManager.Size.Height / 2));
			if (p.Equals(ScreenManager.ZeroPoint))
				return;
			screenManager.currentWorld.camera.AddLooking(p.X, -p.Y);
			//Console.WriteLine(screenManager.currentWorld.camera.Looking + " and " + screenManager.currentWorld.camera.getLookingNoRot());
			screenManager.RequestRedraw();
		}

		protected override void ManageKeyboard()
		{
			if (IsKeyDown(Keys.Escape))
			{
				if (!escPress)
				{
					if (screenManager.paused)
						screenManager.requestPause(false);
					else
						screenManager.requestClose();
					escPress = true;
				}
			}
			else
			{
				escPress = false;
			}

			if (IsKeyDown(Keys.P))
			{
				if (!screenManager.paused)
					screenManager.requestPause(true);
			}

			if (screenManager.currentWorld.HasPlayer())
			{
				if (IsKeyDown(Keys.W))
				{
					
					screenManager.currentWorld.GetPlayer().velocity.X = screenManager.currentWorld.GetPlayer().camera.getLooking().X;
					screenManager.currentWorld.GetPlayer().velocity.Y = screenManager.currentWorld.GetPlayer().camera.getLooking().Y;
					//screenManager.currentWorld.GetPlayer().Translate(new Vector3(screenManager.currentWorld.GetPlayer().camera.getLooking().X, screenManager.currentWorld.GetPlayer().camera.getLooking().Y, 0));
					//screenManager.currentWorld.camera.coords = new Vector3(screenManager.currentWorld.camera.coords.X + screenManager.currentWorld.GetPlayer().camera.getLooking().X, screenManager.currentWorld.GetPlayer().camera.coords.Y + screenManager.currentWorld.GetPlayer().camera.getLooking().Y, screenManager.currentWorld.GetPlayer().camera.coords.Z);
					screenManager.RequestRedraw();
				}

				if (IsKeyDown(Keys.S))
				{
					screenManager.currentWorld.GetPlayer().velocity.X = -screenManager.currentWorld.GetPlayer().camera.getLooking().X;
					screenManager.currentWorld.GetPlayer().velocity.Y = -screenManager.currentWorld.GetPlayer().camera.getLooking().Y;
					//screenManager.currentWorld.GetPlayer().Translate(new Vector3(- screenManager.currentWorld.GetPlayer().camera.getLooking().X, - screenManager.currentWorld.GetPlayer().camera.getLooking().Y, 0));
					//screenManager.currentWorld.camera.coords = new Vector3(screenManager.currentWorld.camera.coords.X - screenManager.currentWorld.GetPlayer().camera.getLooking().X, screenManager.currentWorld.GetPlayer().camera.coords.Y - screenManager.currentWorld.GetPlayer().camera.getLooking().Y, screenManager.currentWorld.GetPlayer().camera.coords.Z);
					screenManager.RequestRedraw();
				}

				if (IsKeyDown(Keys.A))
				{
					screenManager.currentWorld.GetPlayer().velocity.X = screenManager.currentWorld.GetPlayer().camera.getLooking(-90).X;
					screenManager.currentWorld.GetPlayer().velocity.Y = screenManager.currentWorld.GetPlayer().camera.getLooking(-90).Y;
					//screenManager.currentWorld.GetPlayer().Translate(new Vector3(screenManager.currentWorld.GetPlayer().camera.getLooking(-90).X, screenManager.currentWorld.GetPlayer().camera.getLooking(-90).Y, 0));
					//screenManager.currentWorld.camera.coords = new Vector3(screenManager.currentWorld.camera.coords.X + screenManager.currentWorld.GetPlayer().camera.getLooking(-90).X, screenManager.currentWorld.GetPlayer().camera.coords.Y + screenManager.currentWorld.GetPlayer().camera.getLooking(-90).Y, screenManager.currentWorld.GetPlayer().camera.coords.Z);
					screenManager.RequestRedraw();
				}

				if (IsKeyDown(Keys.D))
				{
					screenManager.currentWorld.GetPlayer().velocity.X = screenManager.currentWorld.GetPlayer().camera.getLooking(90).X;
					screenManager.currentWorld.GetPlayer().velocity.Y = screenManager.currentWorld.GetPlayer().camera.getLooking(90).Y;
					//screenManager.currentWorld.GetPlayer().Translate(new Vector3(screenManager.currentWorld.GetPlayer().camera.getLooking(90).X, screenManager.currentWorld.GetPlayer().camera.getLooking(90).Y, 0));
					//screenManager.currentWorld.camera.coords = new Vector3(screenManager.currentWorld.camera.coords.X + screenManager.currentWorld.GetPlayer().camera.getLooking(90).X, screenManager.currentWorld.GetPlayer().camera.coords.Y + screenManager.currentWorld.GetPlayer().camera.getLooking(90).Y, screenManager.currentWorld.GetPlayer().camera.coords.Z);
					screenManager.RequestRedraw();
				}

				if (IsKeyDown(Keys.Space))
				{
					screenManager.currentWorld.GetPlayer().Jump();
					//screenManager.currentWorld.GetPlayer().velocity.Z += screenManager.currentWorld.GetPlayer().JumpSpeed;
					screenManager.RequestRedraw();
				}
			}
		}
	}
}