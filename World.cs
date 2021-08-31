using System.Collections.Generic;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class World
	{
		public List<RenderableObject> RenderableObjects = new List<RenderableObject>();

		public World()
		{
			RenderableObjects.Add(new RenderablePlane(this, new SquareFace('@', new Vector3(255, 255, -1), new Vector3(-255, 255, -1), new Vector3(-255, -255, -1))));
		}
		
		public char RaytraceAll(Camera camera, Vector2 offset)
		{
			RaytraceResult record = RaytraceResult.EMPTY;
			RaytraceResult holder;
			foreach (RenderableObject obj in RenderableObjects)
				if (obj.visible)
				{
					holder = obj.Raytrace(camera, offset);
					if (holder.result && holder.dist < record.dist)
						record = holder;
				}

			return record.text;
		}
	}
}