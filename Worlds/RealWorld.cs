namespace AsciiFuntimeLand
{
	public class RealWorld : World
	{
		public Player player;
		
		public RealWorld(string worldID) : base(worldID)
		{
			player = new Player(this);
			player.camera = camera;
			player.camera.coords.Z = 1;
			manager = new MainManager();
		}

		public override bool HasPlayer()
		{
			return true;
		}

		public override void HandleUpdates()
		{
			manager.ManageSelf();
			player.Tick();
		}

		public override Player GetPlayer()
		{
			return player;
		}
	}
}