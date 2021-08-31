using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class SquareFace : RenderableFace
	{
		private Vector3 _pointA;
		private Vector3 _pointB;
		private Vector3 _pointC;
		private Vector3 _pointD;
		private Plane plane;
		public SquareFace(char renderChar, Vector3 pointA, Vector3 pointB, Vector3 pointC) : base(renderChar)
		{
			_pointA = pointA;
			_pointB = pointB;
			_pointC = pointC;
			_pointD = (pointA - pointB) + (pointC - pointB);
			plane = Plane.CreateFromVertices(pointA, pointB, pointC);
		}
		public override RenderableObject.RaytraceResult Raytrace(Camera camera, Vector2 offset)
		{
			if (intersects(_pointA, plane.Normal, camera.coords, camera.getLookingNoRot(offset)))
			{
				Vector3 intersect = lineIntersection(_pointA, plane.Normal, camera.coords, camera.getLookingNoRot(offset));
				if ((_pointA - _pointB).Length() + (_pointC - _pointB).Length() < (_pointA - intersect).Length() + (_pointC - intersect).Length())
					return RenderableObject.RaytraceResult.EMPTY;
				else 
					return new RenderableObject.RaytraceResult(renderChar, true, (camera.coords - intersect).Length(), Color.White.ToArgb());
			}
			return RenderableObject.RaytraceResult.EMPTY;
		}

		public override void Translate(Vector3 direction, float magnitude)
		{
			_pointA += direction * magnitude;
			_pointB += direction * magnitude;
			_pointC += direction * magnitude;
		}
	}
}