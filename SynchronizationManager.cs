using System.Threading;

namespace AsciiFuntimeLand
{
	public class SynchronizationManager
	{
		public void Start()
		{
			while (Program.screenManager.open)
			{
				Thread.Sleep(Program.screenManager.sleepAmount);
				Program.screenManager.UpdateInput();
			}
		}
	}
}