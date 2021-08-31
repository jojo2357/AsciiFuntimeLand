using System.Numerics;

namespace AsciiFuntimeLand
{
	public class RectangularPrism : RenderableObject
	{
		public RectangularPrism(World world, char myChar, Vector3 origin, Vector3 pointA, Vector3 pointB, Vector3 pointC) : base(world)
		{
			faces = new RenderableFace[6];
			faces[0] = new SquareFace(myChar, pointA, origin, pointB);
			faces[1] = new SquareFace(myChar, pointA, origin, pointC);
			faces[2] = new SquareFace(myChar, pointB, origin, pointC);
			Vector3 otherorigin = origin + (pointA - origin) + (pointC - origin) + (pointB - origin); //pointC - (pointB - (pointA - origin));
			Vector3 otherA = pointA + (pointB - origin) + (pointC - pointA);
			Vector3 otherB = pointB + (pointC - origin) + (pointA - pointB);
			Vector3 otherC = pointC + (pointA - origin) + (pointB - pointC);
			faces[3] = new SquareFace(myChar, otherA, otherorigin, otherB);
			faces[4] = new SquareFace(myChar, otherA, otherorigin, otherC);
			faces[5] = new SquareFace(myChar, otherB, otherorigin, otherC);
		}
	}
}