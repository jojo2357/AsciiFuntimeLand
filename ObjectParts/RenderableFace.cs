using System;
using System.Numerics;
using Color = System.Drawing.Color;

namespace AsciiFuntimeLand
{
	public class RaytraceResult
	{
		private static Color holdercolor;

		public static RaytraceResult EMPTY = new RaytraceResult(' ', false, float.MaxValue, Color.Empty.ToArgb());

		public RaytraceResult(char txt, bool res, float dst, int kolor)
		{
			text = txt;
			result = res;
			dist = Math.Abs(dst);
			holdercolor = RenderController.createColor(kolor);
			color = holdercolor == Color.Empty ? Color.FromArgb(kolor) : holdercolor;
		}

		public RaytraceResult(char txt, bool res, float dst, int r, int g, int b) : this(txt, res, dst, (r << 16) | (g << 8) | b)
		{
		}

		public char text { get; }
		public bool result { get; }
		public float dist { get; }
		public Color color { get; }
	}

	public abstract class RenderableFace
	{
		private int br, bg, bb;
		public char renderChar = '#';
		public bool visible = true;
		private static Vector3 _normalizedLine;

		protected RenderableFace() : this('!')
		{
		}

		protected RenderableFace(char myChar)
		{
			renderChar = myChar;
		}

		public abstract RaytraceResult Raytrace(Vector3 looking, Vector3 position);

		public abstract void Translate(Vector3 direction, float magnitude);

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

		protected abstract int GetColor(float dist);
	}
}