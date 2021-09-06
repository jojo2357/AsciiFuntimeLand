namespace AsciiFuntimeLand
{
	public class MenuWorld : World
	{
		public MenuWorld(string worldID) : base(worldID)
		{
			manager = new MainMenuManager();
		}

		public override bool HasPlayer()
		{
			return false;
		}

		public override void HandleUpdates()
		{
			manager.ManageSelf();
		}

		public override Player GetPlayer()
		{
			return null;
		}
	}
}