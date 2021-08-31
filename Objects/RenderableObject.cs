using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class RenderableObject
	{
		public static readonly List<RenderableObject> ObjectRegistry = new List<RenderableObject>();
		protected RenderableFace[] faces;
		public bool visible = true;

		protected RenderableObject()
		{
			ObjectRegistry.Add(this);
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
				if (res.result && res.dist < best.dist)
				{
					best = res;
				}
			}
			return best;
		}

		public static char RaytraceAll(Camera camera, Vector2 offset)
		{
			RaytraceResult record = RaytraceResult.EMPTY;
			RaytraceResult holder;
			foreach (RenderableObject obj in ObjectRegistry)
			{
				if (obj.visible)
				{
					holder = obj.Raytrace(camera, offset);
					if (holder.result && holder.dist < record.dist)
						record = holder;
				}
			}

			return record.text;
		}
	}
}