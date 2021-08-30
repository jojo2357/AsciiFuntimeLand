using System.Numerics;

namespace AsciiFuntimeLand
{
	public abstract class RenderableFace
	{
		public Vector3 dims { get; }
		public Vector3 pos { get; }
		public char renderChar = '#';
		public bool visible = true;
		
		protected RenderableFace() : this(Vector3.One, new Vector3(1, 1, 1), '!'){}

		protected RenderableFace(Vector3 dim, Vector3 sition, char myChar)
		{
			dims = dim;
			pos = sition;
			renderChar = myChar;
		}
		
		public abstract bool Raytrace(Camera camera, Vector2 offset);
	}
}