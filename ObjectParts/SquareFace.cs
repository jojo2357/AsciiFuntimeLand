using System;
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
		public override RaytraceResult Raytrace(Vector3 looking, Vector3 position)
		{
			if (intersects(_pointA, plane.Normal, position, looking))
			{
				Nullable<Vector3> intersectUnchecked = lineIntersection(_pointA, plane.Normal, position, looking);
				if (intersectUnchecked == null || (_pointA - _pointB).Length() + (_pointC - _pointB).Length() < (_pointA - intersectUnchecked.Value).Length() + (_pointC - intersectUnchecked.Value).Length())
					return RaytraceResult.EMPTY;
				else 
					return new RaytraceResult(renderChar, true, (position - intersectUnchecked.Value).Length(), Color.White.ToArgb());
			}
			return RaytraceResult.EMPTY;
		}

		public override void Translate(Vector3 direction, float magnitude)
		{
			_pointA += direction * magnitude;
			_pointB += direction * magnitude;
			_pointC += direction * magnitude;
		}
	}
}