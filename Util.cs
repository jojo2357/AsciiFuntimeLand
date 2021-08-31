using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace AsciiFuntimeLand
{
	public static class Util
	{
		public static Vector3D ToVector3D(Vector3 vec)
		{
			return new Vector3D(vec.X, vec.Y, vec.Z);
		}


		public static float MathematicalMod(double value, double modulo)
		{
			return (float) (value - Math.Floor(value / modulo) * modulo);
		}

		public static Size Divide(Size a, int b)
		{
			return new Size(a.Width / 2, a.Height / 2);
		}
	}
}