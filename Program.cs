using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsciiFuntimeLand
{
	public static class Program
	{
		private static Point _pos;
		public static ScreenManager screenManager { get; private set; }

		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			screenManager = new ScreenManager();
			Cursor.Position = screenManager.Location + Util.Divide(screenManager.Size, 2);
			new Task(DoIt).Start();
			new SynchronizationManager().Start();
		}

		private static void DoIt()
		{
			Application.Run(screenManager);
		}
	}
}