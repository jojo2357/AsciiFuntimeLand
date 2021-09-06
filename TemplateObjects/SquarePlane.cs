using System.Numerics;

namespace AsciiFuntimeLand
{
	public struct SquarePlane
	{
		public Vector3 _pointA;
		public Vector3 _pointB;
		public Vector3 _pointC;
		public Vector3 _pointD;
		public Plane plane;
		
		public SquarePlane(Vector3 pointA, Vector3 pointB, Vector3 pointC)
		{
			_pointA = pointA;
			_pointB = pointB;
			_pointC = pointC;
			_pointD = _pointA + (_pointC - _pointB);
			plane = Plane.CreateFromVertices(_pointA, _pointB, _pointC);
		}
	}
}