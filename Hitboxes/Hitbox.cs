using System.Numerics;

namespace AsciiFuntimeLand
{
	public class Hitbox : ICollidable
	{
		public Vector3 _pointD;
		public Plane plane;
		public Vector3 _pointA;
		public Vector3 _pointB;
		public Vector3 _pointC;
		private static Vector3 _normalizedLine;

		public Hitbox(SquarePlane face)
		{
			_pointA = face._pointA;
			_pointB = face._pointB;
			_pointC = face._pointC;
			_pointD = face._pointD;
			plane = face.plane;
		}
		public Hitbox(Vector3 pointA, Vector3 pointB, Vector3 pointC) : this(new SquarePlane(pointA, pointB, pointC))
		{
		}

		/*public double Raytrace(Vector3 looking, Vector3 position)
		{
			if (intersects(_pointA, plane.Normal, position, looking))
			{
				Vector3? intersectUnchecked = lineIntersection(_pointA, plane.Normal, position, looking);
				if (intersectUnchecked != null && PointIsOnPlaneBox(intersectUnchecked.Value))
					return Vector3.Distance(position, intersectUnchecked.Value);
				return double.NegativeInfinity;
			}

			return double.NegativeInfinity;
		}*/
		
		public static Vector3 Raytrace(Hitbox box, Vector3 looking, Vector3 position)
		{
			if (intersects(box._pointA, box.plane.Normal, position, looking))
			{
				Vector3? intersectUnchecked = lineIntersection(box._pointA, box.plane.Normal, position, looking);
				if (intersectUnchecked != null && PointIsOnPlaneBox(box, intersectUnchecked.Value))
					return intersectUnchecked.Value - position;
				return Vector3.Zero;
			}

			return Vector3.Zero;
		}

		private static bool PointIsOnPlaneBox(Hitbox box, Vector3 point)
		{
			return Vector3.Dot(box._pointA, box._pointB - box._pointA) <= Vector3.Dot(point, box._pointB - box._pointA) && Vector3.Dot(point, box._pointB - box._pointA) <= Vector3.Dot(box._pointB, box._pointB - box._pointA) && Vector3.Dot(box._pointA, box._pointD - box._pointA) <= Vector3.Dot(point, box._pointD - box._pointA) && Vector3.Dot(point, box._pointD - box._pointA) <= Vector3.Dot(box._pointD, box._pointD - box._pointA);
		}

		public void Translate(Vector3 direction)
		{
			_pointA += direction;
			_pointB += direction;
			_pointC += direction;
			_pointD += direction;
			plane = Plane.CreateFromVertices(_pointA, _pointB, _pointC);
		}
		
		public static bool intersects(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 lineDirection)
		{
			return Vector3.Dot(planeNormal, lineDirection) != 0;
		}

		public static Vector3? lineIntersection(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 lineDirection)
		{
			_normalizedLine = Vector3.Normalize(lineDirection);
			if (!intersects(planePoint, planeNormal, linePoint, _normalizedLine)) return null;
			
			float t = (Vector3.Dot(planeNormal, planePoint) - Vector3.Dot(planeNormal, linePoint)) / Vector3.Dot(planeNormal, _normalizedLine);
			if (t < 0)
				return null;
			return linePoint + _normalizedLine * t;
		}

		public Hitbox[] getHitboxes()
		{
			return new[] { this };
		}
	}
}