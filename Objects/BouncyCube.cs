using System.Numerics;

namespace AsciiFuntimeLand
{
	public class BouncyCube : RenderableRectangularPrism, ITickable
	{
		private Vector3 velocity = new Vector3();

		public BouncyCube(World world, char myChar, Vector3 origin, Vector3 pointA, Vector3 pointB, Vector3 pointC) : base(world, myChar, origin, pointA, pointB, pointC)
		{
		}

		public void Tick()
		{
		}
	}
}