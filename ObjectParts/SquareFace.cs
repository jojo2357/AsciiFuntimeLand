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

		public SquareFace(char renderChar, SquarePlane face) : base(renderChar)
		{
			_pointA = face._pointA;
			_pointB = face._pointB;
			_pointC = face._pointC;
			_pointD = face._pointD;
			plane = face.plane;
		}

		public SquareFace(char renderChar, Vector3 pointA, Vector3 pointB, Vector3 pointC) : this(renderChar, new SquarePlane(pointA, pointB, pointC))
		{
		}

		private static Vector3[] orderPoints(params Vector3[] vecs)
		{
			int longestIndexa = -1, longestIndexb = -1;
			double longest = -1;
			for (int i = 0; i < vecs.Length; i++)
			for (int j = i + 1; j < vecs.Length; j++)
				if (Vector3.DistanceSquared(vecs[i], vecs[j]) /*Math.Abs((vecs[i] - vecs[j]).Length())*/ > longest)
				{
					longest = Vector3.Dot(vecs[i], vecs[j]); //Math.Abs((vecs[i] - vecs[j]).Length());
					longestIndexa = i;
					longestIndexb = j;
				}

			if (longestIndexa == 0 && longestIndexb == 1)
				return vecs;
			Vector3 holder = vecs[0];
			vecs[0] = vecs[longestIndexa];
			vecs[longestIndexa] = holder;
			holder = vecs[vecs.Length - 1];
			vecs[vecs.Length - 1] = vecs[longestIndexb];
			vecs[longestIndexb] = holder;

			return vecs;
		}

		public override RaytraceResult Raytrace(Vector3 looking, Vector3 position)
		{
			if (intersects(_pointA, plane.Normal, position, looking))
			{
				Vector3? intersectUnchecked = lineIntersection(_pointA, plane.Normal, position, looking);
				if (intersectUnchecked == null || !PointIsOnPlaneBox(intersectUnchecked.Value))
					return RaytraceResult.EMPTY;
				return new RaytraceResult(renderChar, true, (position - intersectUnchecked.Value).Length(), GetColor((position - intersectUnchecked.Value).Length()));
			}

			return RaytraceResult.EMPTY;
		}

		private bool PointIsOnPlaneBox(Vector3 point)
		{
			return Vector3.Dot(_pointA, _pointB - _pointA) <= Vector3.Dot(point, _pointB - _pointA) && Vector3.Dot(point, _pointB - _pointA) <= Vector3.Dot(_pointB, _pointB - _pointA) && Vector3.Dot(_pointA, _pointD - _pointA) <= Vector3.Dot(point, _pointD - _pointA) && Vector3.Dot(point, _pointD - _pointA) <= Vector3.Dot(_pointD, _pointD - _pointA);
		}

		private static double AngleBetween(Vector3 vector1, Vector3 vector2)
		{
			return 180 / Math.PI * (Vector3.Dot(vector1, vector2) >= 0.0 ? 2.0 * Math.Asin((vector1 - vector2).Length() / 2.0) : Math.PI - 2.0 * Math.Asin((-vector1 - vector2).Length() / 2.0));
		}

		public override void Translate(Vector3 direction, float magnitude)
		{
			_pointA += direction * magnitude;
			_pointB += direction * magnitude;
			_pointC += direction * magnitude;
			_pointD += direction * magnitude;
			plane = Plane.CreateFromVertices(_pointA, _pointB, _pointC);
		}

		protected override int GetColor(float dist)
		{
			return Color.White.ToArgb();
		}
	}
}