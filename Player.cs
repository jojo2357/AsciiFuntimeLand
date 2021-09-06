using System;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class Player : ITickable
	{
		private bool onGround = false;
		private bool hitSideways = false;
		public static float gravity = 10;
		public Camera camera;

		public Vector3 CameraOffset = new Vector3(0, 0, 1);
		public Vector3 velocity;
		public Hitcube hb;

		public Player(World world)
		{
			camera = new Camera();
			camera.coords.Z = CameraOffset.Z;
			hb = new Hitcube(world, new RectangularPrism(new Vector3(0.5f, -0.5f, 1.5f), new Vector3(-0.5f, -0.5f, 1.5f), new Vector3(0.5f, 0.5f, 1.5f), new Vector3(0.5f, -0.5f, 0)));
		}

		public float JumpSpeed = 2;

		public void Tick()
		{
			velocity.Z -= (float)(gravity / (1000.0 / Program.screenManager.sleepAmount));
			Translate(velocity);
			Vector3[] collisions = hb.Collided();
			onGround = hitSideways = false;
			if (collisions.Length > 0)
			{
				foreach(Vector3 vec in collisions)
				{
					hitSideways |= vec.X != 0 || vec.Y != 0;
					onGround |= vec.Z > 0;
				}
				Console.WriteLine("Player collision @ " + camera.coords);
				if (hitSideways && onGround)
				{
					Translate(-velocity);
					velocity = Vector3.Zero;
				}
				else if (hitSideways)
				{
					Translate(-new Vector3(velocity.X, velocity.Y, 0));
				} else if (onGround)
				{
					Translate(-new Vector3(0, 0, velocity.Z));
					velocity.Z = 0;
				}
				//Console.WriteLine(camera.coords);
			}
			velocity.X = 0;
			velocity.Y = 0;
			Program.screenManager.RequestRedraw();
		}

		public void Translate(Vector3 amount)
		{
			camera.coords += amount;
			hb.Translate(amount);
		}

		public void Jump()
		{
			if (onGround)
			{
				velocity.Z = JumpSpeed;
			}
		}
	}
}