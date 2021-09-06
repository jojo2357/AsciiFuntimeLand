using System;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class ColorableSquareFace : SquareFace
	{
		private int baseR, baseG, baseB;
		private double renderDistance = 200;

		public ColorableSquareFace(char renderChar, SquarePlane plane, int baser, int baseg, int baseb) : base(renderChar, plane)
		{
			baseR = baser;
			baseG = baseg;
			baseB = baseb;
		}
		public ColorableSquareFace(char renderChar, Vector3 pointA, Vector3 pointB, Vector3 pointC, int baser, int baseg, int baseb) : this(renderChar, new SquarePlane(pointA, pointB, pointC), baser, baseg, baseb)
		{

		}

		protected override int GetColor(float distance)
		{
			if (distance > renderDistance)
				return 0;
			return (((((int) (Math.Sqrt(baseR * baseR * (1 - distance / renderDistance)) / 16) * 16) & 0xFF) << 16) | ((((int) (Math.Sqrt(baseG * baseG * (1 - distance / renderDistance)) / 16) * 16) & 0xFF) << 8) | ((((int) (Math.Sqrt(baseB * baseB * (1 - distance / renderDistance)) / 16) * 16) & 0xFF)));
		}
	}
}