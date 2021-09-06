using System.Numerics;

namespace AsciiFuntimeLand
{
	public class RenderableWall : RenderablePlane, ICollidable
	{
		private Hitbox hb;

		public Hitbox[] getHitboxes()
		{
			return new[] { hb };
		}

		public RenderableWall(World world, char renderChar, SquarePlane plane, int r, int g, int b) : base(world, new ColorableSquareFace(renderChar, plane, r, g, b))
		{
			hb = new Hitbox(plane);
			world.Hitcubes.Add(this);
		}
	}
}