namespace AsciiFuntimeLand
{
	public class RenderablePlane : RenderableFacedObject
	{
		public RenderablePlane(World world, SquareFace face) : base(world)
		{
			faces = new RenderableFace[1];
			faces[0] = face;
		}
	}
}