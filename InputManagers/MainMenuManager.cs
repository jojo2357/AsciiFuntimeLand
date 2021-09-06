using System.Windows.Forms;

namespace AsciiFuntimeLand
{
	public class MainMenuManager : InputManager
	{
		protected override void ManageMouse()
		{
			Cursor.Show();
		}

		protected override void ManageKeyboard()
		{
			if (IsKeyDown(Keys.Escape))
			{
				if (!escPress)
				{
					Program.screenManager.requestPause(false);
					escPress = true;
				}
			}
			else
				escPress = false;
		}
	}
}