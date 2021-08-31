using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public partial class RenderableObject
	{
		public static readonly List<RenderableObject> ObjectRegistry = new List<RenderableObject>();
		public Vector3 dims { get; }
		public Vector3 pos { get; }
		public char renderChar = '#';
		public bool visible = true;
		private int br, bg, bb;

		public RenderableObject() : this(Vector3.One * 2, new Vector3(1, 1, -1))
		{
		}

		public RenderableObject(Vector3 dim, Vector3 sition)
		{
			dims = dim;
			pos = sition;
			ObjectRegistry.Add(this);
		}

		public RenderableObject(Vector3 dim, Vector3 sition, char myChar, Color startingColor)
		{
			dims = dim;
			pos = sition;
			renderChar = myChar;
			br = (startingColor.ToArgb() >> 16) & 0xFF;
			bg = (startingColor.ToArgb() >> 8) & 0xFF;
			bb = startingColor.ToArgb() & 0xFF;
			ObjectRegistry.Add(this);
		}

		public RaytraceResult Raytrace(Camera camera, Vector2 offset)
		{
			if (!visible)
				return RaytraceResult.EMPTY;
			Vector3 lookingNoRot = camera.getLookingNoRot(offset);
			if (lookingNoRot.Z > 0 == (pos.Z - camera.coords.Z) > 0)
			{
				Vector3 znormalized = camera.coords + lookingNoRot /
					(lookingNoRot.Z / (pos.Z - camera.coords.Z));
				if (znormalized.X >= pos.X &&
				    znormalized.X <= pos.X + dims.X &&
				    znormalized.Y >= pos.Y && znormalized.Y <= pos.Y + dims.Y)
					return new RaytraceResult(renderChar, true, (znormalized - camera.coords).Length(), this);
			}			if (lookingNoRot.Y / (pos.Y - camera.coords.Y) > 0)
			{
				Vector3 ynormalized = camera.coords + lookingNoRot /
					(lookingNoRot.Y / (pos.Y - camera.coords.Y));
				if (ynormalized.X >= pos.X &&
				    ynormalized.X <= pos.X + dims.X &&
				    ynormalized.Z >= pos.Z && ynormalized.Z <= pos.Z + dims.Z)
					return new RaytraceResult(renderChar, true, (ynormalized - camera.coords).Length(), this);
			}

			if (lookingNoRot.X > 0 == (pos.X - camera.coords.X) > 0)
			{
				Vector3 xnormalized = camera.coords + lookingNoRot /
					(lookingNoRot.X / (pos.X - camera.coords.X));
				if (xnormalized.Y >= pos.Y &&
				    xnormalized.Y <= pos.Y + dims.Y &&
				    xnormalized.Z >= pos.Z && xnormalized.Z <= pos.Z + dims.Z)
					return new RaytraceResult(renderChar, true, (xnormalized - camera.coords).Length(), this);
			}

			if (lookingNoRot.Z > 0 == (pos.Z + dims.Z - camera.coords.Z) > 0)
			{
				Vector3 zfarnormalized = camera.coords + lookingNoRot /
					(lookingNoRot.Z / (pos.Z + dims.Z - camera.coords.Z));
				if (zfarnormalized.X >= pos.X &&
				    zfarnormalized.X <= pos.X + dims.X &&
				    zfarnormalized.Y >= pos.Y && zfarnormalized.Y <= pos.Y + dims.Y)
					return new RaytraceResult(renderChar, true, (zfarnormalized - camera.coords).Length(), this);
			}

			if (lookingNoRot.Y > 0 == (pos.Y + dims.Y - camera.coords.Y) > 0)
			{
				Vector3 yfarnormalized = camera.coords + lookingNoRot /
					(lookingNoRot.Y / (pos.Y + dims.Y - camera.coords.Y));
				if (yfarnormalized.X >= pos.X &&
				    yfarnormalized.X <= pos.X + dims.X &&
				    yfarnormalized.Z >= pos.Z && yfarnormalized.Z <= pos.Z + dims.Z)
					return new RaytraceResult(renderChar, true, (yfarnormalized - camera.coords).Length(), this);
			}

			if (lookingNoRot.X > 0 == (pos.X + dims.X - camera.coords.X) > 0)
			{
				Vector3 xfarnormalized = camera.coords + lookingNoRot /
					(lookingNoRot.X / (pos.X + dims.X - camera.coords.X));
				if (xfarnormalized.Y >= pos.Y &&
				    xfarnormalized.Y <= pos.Y + dims.Y &&
				    xfarnormalized.Z >= pos.Z && xfarnormalized.Z <= pos.Z + dims.Z)
					return new RaytraceResult(renderChar, true, (xfarnormalized - camera.coords).Length(), this);
			}

			return RaytraceResult.EMPTY;
		}

		public static char RaytraceAll(Camera camera, Vector2 offset)
		{
			RaytraceResult holder;
			foreach (RenderableObject obj in ObjectRegistry)
			{
				holder = obj.Raytrace(camera, offset);
				if (holder.result)
					return holder.text;
			}

			return ' ';
		}
	}
}