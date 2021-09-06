using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class RenderableFacedObject : RenderableObject
	{
		protected RenderableFace[] faces;

		public override RaytraceResult Raytrace(Camera camera, Vector2 offset)
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

		protected RenderableFacedObject(World world) : base(world)
		{
		}

		public override void Translate(Vector3 direction, float distance)
		{
			foreach (RenderableFace face in faces)
				face.Translate(direction, distance);
		}
	}
}