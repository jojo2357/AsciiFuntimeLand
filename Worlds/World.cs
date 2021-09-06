using System;
using System.Collections.Generic;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class World
	{
		public Camera camera = new Camera();
		public static readonly Dictionary<string, World> worlds = new Dictionary<string, World>();
		public List<RenderableObject> RenderableObjects = new List<RenderableObject>();
		public List<ITickable> Tickables = new List<ITickable>();
		public List<ICollidable> Hitcubes = new List<ICollidable>();
		protected InputManager manager;

		public World(string worldID)
		{
			worlds[worldID] = this;
		}

		public abstract bool HasPlayer();

		public void UpdateTickables()
		{
			foreach (ITickable tickable in Tickables)
				tickable.Tick();
		}

		public abstract void HandleUpdates();

		public abstract Player GetPlayer();
	}
}