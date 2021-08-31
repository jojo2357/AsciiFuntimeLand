using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace AsciiFuntimeLand
{
	public class SquareFace : RenderableFace
	{
		private Vector3 _pointA;
		private Vector3 _pointB;
		private Vector3 _pointC;
		private readonly Vector3 _pointD;
		private readonly Plane plane;


		public SquareFace(char renderChar, Vector3 pointA, Vector3 pointB, Vector3 pointC) : base(renderChar)
		{
			Vector3[] vecs = orderPoints(pointA, pointB, pointC);
			_pointA = vecs[0];
			_pointB = vecs[1];
			_pointC = vecs[2];
			_pointD = _pointA + (_pointC - _pointB);
			plane = Plane.CreateFromVertices(_pointA, _pointB, _pointC);
		}

		private static Vector3[] orderPoints(params Vector3[] vecs)
		{
			int longestIndexa = -1, longestIndexb = -1;
			double longest = -1;
			for (int i = 0; i < vecs.Length; i++)
			for (int j = i + 1; j < vecs.Length; j++)
				if (Vector3.Dot(vecs[i], vecs[j]) /*Math.Abs((vecs[i] - vecs[j]).Length())*/ > longest)
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
				if (intersectUnchecked == null || !PointIsOnPlaneBox(intersectUnchecked.Value) /*(_pointA - _pointC).Length() + (_pointD - _pointB).Length() < (_pointA - intersectUnchecked.Value).Length() + (_pointD - intersectUnchecked.Value).Length()*/)
					return RaytraceResult.EMPTY;
				return new RaytraceResult(renderChar, true, (position - intersectUnchecked.Value).Length(), Color.White.ToArgb());
			}

			return RaytraceResult.EMPTY;
		}

		private bool PointIsOnPlaneBox(Vector3 point)
		{
			Vector3D solvedBA = Util.ToVector3D(_pointA - _pointB);
			Vector3D solvedBC = Util.ToVector3D(_pointC - _pointB);
			Vector3D solvedBP = Util.ToVector3D(point - _pointB);
			Vector3D solvedDA = Util.ToVector3D(_pointA - _pointD);
			Vector3D solvedDC = Util.ToVector3D(_pointC - _pointD);
			Vector3D solvedDP = Util.ToVector3D(point - _pointD);
			return Math.Abs(Vector3D.AngleBetween(solvedBA, solvedBP) + Vector3D.AngleBetween(solvedBP, solvedBC) - 90) < 0.00001 && Math.Abs(Vector3D.AngleBetween(solvedDA, solvedDP) + Vector3D.AngleBetween(solvedDP, solvedDC) - 90) < 0.00001;
		}

		public override void Translate(Vector3 direction, float magnitude)
		{
			_pointA += direction * magnitude;
			_pointB += direction * magnitude;
			_pointC += direction * magnitude;
		}
	}
}