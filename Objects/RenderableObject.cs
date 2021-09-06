using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class RenderableObject
	{
		public bool visible = true;

		protected RenderableObject(World world)
		{
			world.RenderableObjects.Add(this);
		}

		public abstract RaytraceResult Raytrace(Camera camera, Vector2 offset);

		public abstract void Translate(Vector3 direction, float distance);

		protected int GetColor(float dist)
		{
			return Color.White.ToArgb();
		}
	}
}