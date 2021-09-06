using System;
using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class Sphere : RenderableObject
	{
		protected Vector3 loc;
		protected double radius;

		public Sphere(World world, Vector3 location, double rad) : base(world)
		{
			loc = location;
			radius = rad;
		}

		public override RaytraceResult Raytrace(Camera camera, Vector2 offset)
		{
			return FindLineSphereIntersections(camera.coords, camera.coords + camera.getLookingNoRot(offset), loc, radius);
		}

		public override void Translate(Vector3 direction, float distance)
		{
			loc += Vector3.Normalize(direction) * distance;
		}

		public RaytraceResult FindLineSphereIntersections(Vector3 linePoint0, Vector3 linePoint1, Vector3 circleCenter, double circleRadius)
		{
			double vx = linePoint1.X - (double) linePoint0.X;
			double vy = linePoint1.Y - (double) linePoint0.Y;
			double vz = linePoint1.Z - (double) linePoint0.Z;

			double A = vx * vx + vy * vy + vz * vz;
			double B = 2.0 * (linePoint0.X * vx + linePoint0.Y * vy + linePoint0.Z * vz - vx * circleCenter.X - vy * circleCenter.Y - vz * circleCenter.Z);
			double C = linePoint0.X * (double) linePoint0.X - 2 * (double) linePoint0.X * circleCenter.X + circleCenter.X * (double) circleCenter.X + linePoint0.Y * (double) linePoint0.Y - 2 * (double) linePoint0.Y * circleCenter.Y + circleCenter.Y * (double) circleCenter.Y +
				linePoint0.Z * (double) linePoint0.Z - 2 * (double) linePoint0.Z * circleCenter.Z + circleCenter.Z * (double) circleCenter.Z - circleRadius * circleRadius;

			// discriminant
			double D = B * B - 4 * A * C;

			if (D < 0)
			{
				return RaytraceResult.EMPTY;
			}

			double t1 = (-B - Math.Sqrt(D)) / (2.0 * A);

			Vector3 solution1 = new Vector3((float) (linePoint0.X * (1 - t1) + t1 * linePoint1.X),
				(float) (linePoint0.Y * (1 - t1) + t1 * linePoint1.Y),
				(float) (linePoint0.Z * (1 - t1) + t1 * linePoint1.Z));
			if (D == 0)
			{
				return new RaytraceResult(':', true, (linePoint0 - solution1).Length(), Color.White.ToArgb());
			}

			double t2 = (-B + Math.Sqrt(D)) / (2.0 * A);
			Vector3 solution2 = new Vector3((float) (linePoint0.X * (1 - t2) + t2 * linePoint1.X),
				(float) (linePoint0.Y * (1 - t2) + t2 * linePoint1.Y),
				(float) (linePoint0.Z * (1 - t2) + t2 * linePoint1.Z));

			// prefer a solution that's on the line segment itself

			if (t1 < 0 && t2 < 0)
				return RaytraceResult.EMPTY;
			if (t2 < 0 || Math.Abs(t1 - 0.5) < Math.Abs(t2 - 0.5))
			{
				return new RaytraceResult(':', true, (solution1 - linePoint0).Length(), Color.White.ToArgb());
				//return new Point3D[] { solution1, solution2 };
			}

			return new RaytraceResult(':', true, (solution2 - linePoint0).Length(), Color.White.ToArgb());
			//return new Point3D[] { solution2, solution1 };
		}
	}
}