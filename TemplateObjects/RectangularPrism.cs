using System.Numerics;

namespace AsciiFuntimeLand
{
	public struct RectangularPrism
	{
		public Vector3[] verticies;

		public RectangularPrism(Vector3 origin, Vector3 pointA, Vector3 pointB, Vector3 pointC)
		{
			verticies = new Vector3[8];
			verticies[0] = origin;
			verticies[1] = pointA;
			verticies[2] = pointB;
			verticies[3] = pointC;
			verticies[4] = origin + (pointA - origin) + (pointC - origin) + (pointB - origin);
			verticies[5] = pointA + (pointB - origin) + (pointC - pointA);
			verticies[6] = pointB + (pointC - origin) + (pointA - pointB);
			verticies[7] = pointC + (pointA - origin) + (pointB - pointC);
		}
	}
}