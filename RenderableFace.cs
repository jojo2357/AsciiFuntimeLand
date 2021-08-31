using System;
using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public partial class RenderableObject
	{
		public class RaytraceResult
		{
			public char text { get; }
			public bool result { get; }
			public float dist { get; }
			public Color color { get; }

			private static Color holdercolor;

			public static RaytraceResult EMPTY = new RaytraceResult(' ', false, -1, Color.Empty.ToArgb());

			public RaytraceResult(char txt, bool res, float dst, int kolor)
			{
				text = txt;
				result = res;
				dist = Math.Abs(dst);
				holdercolor = RenderController.createColor(kolor);
				color = holdercolor == Color.Empty ? Color.FromArgb(kolor) : holdercolor;
			}

			public RaytraceResult(char txt, bool res, float dst, int r, int g, int b) : this(txt, res, dst, r << 16 | g << 8 | b)
			{
			}

			public RaytraceResult(char txt, bool res, float dst, RenderableObject obj) : this(txt, res, dst, ((int) (obj.br / Math.Sqrt(dst)) / 16 * 16) << 16 | ((int) (obj.bg / Math.Sqrt(dst)) / 16 * 16) << 8 | ((int) (obj.bb / Math.Sqrt(dst)) / 16 * 16))
			{
			}
		}
	}

	public abstract class RenderableFace
	{
		public char renderChar = '#';
		public bool visible = true;

		protected RenderableFace() : this('!')
		{
		}

		protected RenderableFace(char myChar)
		{
			renderChar = myChar;
		}

		public abstract RenderableObject.RaytraceResult Raytrace(Camera camera, Vector2 offset);

		public abstract void Translate(Vector3 direction, float magnitude);

		public static bool intersects(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 lineDirection)
		{
			if (Vector3.Dot(planeNormal, Vector3.Normalize(lineDirection)) == 0)
			{
				return false;
			}

			return true;
		}

		public static Vector3 lineIntersection(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 lineDirection)
		{
			if (!intersects(planePoint, planeNormal, linePoint, lineDirection))
			{
				throw new ArgumentException("Parallel, please verify first");
			}

			float t = Vector3.Dot(planeNormal, planePoint) - Vector3.Dot(planeNormal, linePoint) / Vector3.Dot(planeNormal, Vector3.Normalize(lineDirection));
			return linePoint + Vector3.Normalize(lineDirection) * t;
		}
	}
}