using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using static AsciiFuntimeLand.Program;

namespace AsciiFuntimeLand
{
	public class MouseManager
	{
		private Point _pos;
		
		public KeyEventArgs lastKeyArgs {
			set
			{
				_lastKeyArgs = value;
				newKeyArgs = true;
			} 
		}

		private KeyEventArgs _lastKeyArgs;
		private bool newKeyArgs = false;

		public MouseManager()
		{
			lastKeyArgs = new KeyEventArgs(0);
		}

		public void ManageSelf()
		{
			ManageKeyboard();
			ManageMouse();
		}

		private void ManageMouse()
		{
			_pos = Cursor.Position;
			Cursor.Position = screenManager.Location + Util.Divide(screenManager.Size, 2);
			screenManager.HandleMouseMovement(new Point(_pos.X - (screenManager.Location.X + screenManager.Size.Width / 2), _pos.Y - (screenManager.Location.Y + screenManager.Size.Height / 2)));
		}
		
		private void ManageKeyboard()
		{
			switch (_lastKeyArgs.KeyCode)
			{
				case Keys.Escape:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.requestClose();
					break;
				case Keys.P:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.requestPause(true);
					break;
				case Keys.W:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.cam.coords = new Vector3(screenManager.cam.coords.X + screenManager.cam.getLooking().X, screenManager.cam.coords.Y + screenManager.cam.getLooking().Y, screenManager.cam.coords.Z);
					screenManager.RequestRedraw();
					break;
				case Keys.S:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.cam.coords = new Vector3(screenManager.cam.coords.X - screenManager.cam.getLooking().X, screenManager.cam.coords.Y - screenManager.cam.getLooking().Y, screenManager.cam.coords.Z);
					screenManager.RequestRedraw();
					break;
				case Keys.A:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.cam.coords = new Vector3(screenManager.cam.coords.X + screenManager.cam.getLooking(-90).X, screenManager.cam.coords.Y + screenManager.cam.getLooking(-90).Y, screenManager.cam.coords.Z);
					screenManager.RequestRedraw();
					break;
				case Keys.D:
					_lastKeyArgs.SuppressKeyPress = true;
					screenManager.cam.coords = new Vector3(screenManager.cam.coords.X + screenManager.cam.getLooking(90).X, screenManager.cam.coords.Y + screenManager.cam.getLooking(90).Y, screenManager.cam.coords.Z);
					screenManager.RequestRedraw();
					break;
			}
		}
	}
}