using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using static AsciiFuntimeLand.Program;

namespace AsciiFuntimeLand
{
	public class RenderController
	{
		private const double heightConstant = 1.67708313;
		private const double widthConstant = 0.76367188;
		private static readonly Vector2 step = new Vector2((float) (8 * widthConstant), (float) (8 * heightConstant));
		private static readonly Vector2 FOV = new Vector2(1.1f, 1.1f);
		private static RaytraceResult res;
		private static RaytraceResult bestRes;

		private static bool beingModified;

		private static string[] lines;

		private static readonly List<RaytraceResult> layersMatrix = new List<RaytraceResult>();
		private static readonly Dictionary<Color, StringBuilder> layers = new Dictionary<Color, StringBuilder>();
		private static readonly List<Color> activeColors = new List<Color>();

		public static HashSet<Color> registeredColors = new HashSet<Color>();

		public static void Dispose()
		{
		}

		public static void PrepareRendering()
		{
			if (beingModified)
				return;
			beingModified = true;

			layersMatrix.Clear();
			activeColors.Clear();
			foreach (StringBuilder sb in layers.Values) sb.Clear();

			for (double y = 0; y < screenManager.Size.Height; y += step.Y)
			{
				for (double x = 0; x < screenManager.Size.Width; x += step.X)
				{
					bestRes = RaytraceResult.EMPTY;
					foreach (RenderableObject obj in Program.screenManager.currentWorld.RenderableObjects)
					{
						res = obj.Raytrace(screenManager.cam, new Vector2((float) ((x - screenManager.Size.Width / 2.0) / screenManager.Size.Width * (2 * FOV.X)), (float) ((y - screenManager.Size.Height / 2.0) / screenManager.Size.Height * (-2 * FOV.Y))));
						if (res.result)
						{
							if (bestRes == RaytraceResult.EMPTY)
								bestRes = res;
							else if (bestRes.dist > res.dist)
								bestRes = res;
						}
					}

					layersMatrix.Add(bestRes);
					if (bestRes.color != Color.Empty && !layers.ContainsKey(bestRes.color))
						layers.Add(bestRes.color, new StringBuilder());
					if (!activeColors.Contains(bestRes.color))
						activeColors.Add(bestRes.color);
				}

				layersMatrix.Add(null);
			}

			foreach (RaytraceResult rtr in layersMatrix)
			foreach (Color color in activeColors)
				if (rtr == null)
				{
					layers[color].Append('\n');
				}
				else
				{
					if (rtr.color == color)
						layers[color].Append(rtr.text);
					else
						layers[color].Append(' ');
				}

			beingModified = false;
		}

		public static void DoRendering(ScreenManager form, Graphics formGraphics)
		{
			if (beingModified)
				return;
			beingModified = true;
			foreach (Color color in activeColors)
			{
				//Console.WriteLine(formGraphics.MeasureString(layers[color].ToString(), form._drawFont));
				lines = layers[color].ToString().Split('\n');
				for (int i = 0; i < lines.Length; i++)
					if (lines[i].Trim().Length > 0)
						form.DrawString(lines[i], new Point(0, (int) Math.Ceiling(step.Y * i)), formGraphics, form._drawFont, color);
					//Console.WriteLine("Overhang: " + (form.Width - formGraphics.MeasureString(lines[i], form._drawFont).Width));
					//form.DrawString(lines[i], new Point(0, (int) Math.Ceiling(step.Y * i)), formGraphics, form._drawFont, color);
				//if (layers[color].ToString().Trim().Length != 0)
				//form.DrawString(layers[color].ToString().Replace("[ \r\n\t]+$", ""), new Point(0, 0), formGraphics, form._drawFont, color);
			}

			beingModified = false;
		}

		public static void PostRendering(ScreenManager form, Graphics formGraphics)
		{
			
		}

		public static Color createColor(int argb)
		{
			foreach (Color color in registeredColors)
				if (color.ToArgb() == argb)
					return color;

			registeredColors.Add(Color.FromArgb(argb));
			//recursion scary, better to repeat and then force quit than inf loop
			foreach (Color color in registeredColors)
				if (color.ToArgb() == argb)
					return color;

			throw new Exception("This should never happen, so if it did, you're fucked, sorry.");
		}
	}
}