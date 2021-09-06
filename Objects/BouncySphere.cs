using System.Numerics;

namespace AsciiFuntimeLand
{
	public class BouncySphere : Sphere, ITickable
	{
		private Vector3 velocity = new Vector3();

		public BouncySphere(World world, Vector3 location, double rad) : base(world, location, rad)
		{
			world.Tickables.Add(this);
		}

		public void Tick()
		{
			velocity.Z -= (float) (Player.gravity / (1000.0 / Program.screenManager.sleepAmount));
			Translate(velocity, velocity.Length());
			if (loc.Z - radius < 0)
			{
				Translate(velocity, -velocity.Length());
				velocity.Z = -0.9f * velocity.Z;
			}

			Program.screenManager.RequestRedraw();
		}
	}
}