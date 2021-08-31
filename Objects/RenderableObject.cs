using System.Collections.Generic;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class RenderableObject
	{
		protected RenderableFace[] faces;
		public bool visible = true;

		protected RenderableObject(World world)
		{
			world.RenderableObjects.Add(this);
		}

		public RaytraceResult Raytrace(Camera camera, Vector2 offset)
		{
			if (!visible)
				return RaytraceResult.EMPTY;
			Vector3 lookingNoRot = camera.getLookingNoRot(offset);
			RaytraceResult best = RaytraceResult.EMPTY;
			foreach (RenderableFace face in faces)
			{
				RaytraceResult res = face.Raytrace(lookingNoRot, camera.coords);
				if (res.result && res.dist < best.dist) best = res;
			}

			return best;
		}
	}
}