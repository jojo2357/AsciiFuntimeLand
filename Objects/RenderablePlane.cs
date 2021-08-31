using System.Numerics;

namespace AsciiFuntimeLand
{
	public class RenderablePlane : RenderableObject
	{
		public RenderablePlane(SquareFace face) : base()
		{
			faces = new RenderableFace[1];
			faces[0] = face;
		}
	}
}