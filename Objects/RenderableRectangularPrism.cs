using System.Numerics;

namespace AsciiFuntimeLand
{
	public class RenderableRectangularPrism : RenderableFacedObject
	{
		protected Vector3[] verticies;

		public RenderableRectangularPrism(World world, char myChar, RectangularPrism prism) : base(world)
		{
			verticies = prism.verticies;
			faces = new RenderableFace[6];
			faces[0] = new SquareFace(myChar, verticies[1], verticies[0], verticies[2]);
			faces[1] = new SquareFace(myChar, verticies[1], verticies[0], verticies[3]);
			faces[2] = new SquareFace(myChar, verticies[2], verticies[0], verticies[3]);
			faces[3] = new SquareFace(myChar, verticies[5], verticies[4], verticies[6]);
			faces[4] = new SquareFace(myChar, verticies[5], verticies[4], verticies[7]);
			faces[5] = new SquareFace(myChar, verticies[6], verticies[4], verticies[7]);
		}

		public RenderableRectangularPrism(World world, char myChar, Vector3 origin, Vector3 pointA, Vector3 pointB, Vector3 pointC) : this(world, myChar, new RectangularPrism(origin, pointA, pointB, pointC))
		{

		}
	}
}