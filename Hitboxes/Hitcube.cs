using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class Hitcube : ICollidable
	{
		private Hitbox[] hitboxes = new Hitbox[6];
		public Vector3[] verticies;

		public Hitcube(World world, RectangularPrism prismIn)
		{
			world.Hitcubes.Add(this);
			verticies = prismIn.verticies;
			hitboxes[0] = new Hitbox(verticies[1], verticies[0], verticies[2]);
			hitboxes[1] = new Hitbox(verticies[1], verticies[0], verticies[3]);
			hitboxes[2] = new Hitbox(verticies[2], verticies[0], verticies[3]);
			hitboxes[3] = new Hitbox(verticies[5], verticies[4], verticies[6]);
			hitboxes[4] = new Hitbox(verticies[5], verticies[4], verticies[7]);
			hitboxes[5] = new Hitbox(verticies[6], verticies[4], verticies[7]);

		}

		public Vector3[] Collided()
		{
			List<Vector3> collisions = new List<Vector3>();
			foreach (ICollidable hc in Program.screenManager.realWorld.Hitcubes)
			{
				if (hc != this)
				{
					collisions.AddRange(Collided(hc));
				}
			}
			return collisions.ToArray();
		}

		public List<Vector3> Collided(ICollidable other)
		{
			List<Vector3> back = new List<Vector3>();
			foreach (Hitbox hb in other.getHitboxes())
			{
				back.AddRange(Collided(hb));
			}
			return back;
		}

		public List<Vector3> Collided(Hitbox other)
		{
			List<Vector3> back = new List<Vector3>();
			foreach (Hitbox hb in hitboxes)
			{
				Vector3 win;
				win = Hitbox.Raytrace(hb, other._pointA - other._pointB, other._pointB);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (other._pointA - other._pointB).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(hb, other._pointA - other._pointD, other._pointD);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (other._pointA - other._pointD).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(hb, other._pointC - other._pointB, other._pointB);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (other._pointC - other._pointB).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(hb, other._pointC - other._pointD, other._pointD);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (other._pointC - other._pointD).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(other, hb._pointA - hb._pointB, hb._pointB);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (hb._pointA - hb._pointB).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(other, hb._pointA - hb._pointD, hb._pointD);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (hb._pointA - hb._pointD).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(other, hb._pointC - hb._pointB, hb._pointB);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (hb._pointC - hb._pointB).LengthSquared())
					back.Add(win);
				win = Hitbox.Raytrace(other, hb._pointC - hb._pointD, hb._pointD);
				if (win.LengthSquared() != 0 && win.LengthSquared() < (hb._pointC - hb._pointD).LengthSquared())
					back.Add(win);
			}

			return back;
		}

		public void Translate(Vector3 translation)
		{
			foreach (Hitbox hb in hitboxes)
			{
				hb.Translate(translation);
			}

			for (int i = 0; i < verticies.Length; i++)
				verticies[i] += translation;
		}

		public Hitbox[] getHitboxes()
		{
			return hitboxes;
		}
	}
}