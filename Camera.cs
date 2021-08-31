using System;
using System.Numerics;

namespace AsciiFuntimeLand
{
	public class Camera
	{
		private Vector3 _looking;

		private Vector3 u;
		private Vector3 v;

		public Camera()
		{
			coords = new Vector3();
			setLooking(new Vector3());
		}

		public Vector3 Looking
		{
			get => _looking;
			set
			{
				_looking = value;
				_up = new Vector3((float) (Math.Sin(_looking.X * Math.PI / 180) * Math.Cos((_looking.Y + 90) * Math.PI / 180)),
					(float) (Math.Cos(_looking.X * Math.PI / 180) * Math.Cos((_looking.Y + 90) * Math.PI / 180)),
					(float) Math.Sin((_looking.Y + 90) * Math.PI / 180));
				_right = Vector3.Cross(getLookingNoRot(), getUp());
				_lookingResolved = new Vector3((float) (Math.Sin(_looking.X * Math.PI / 180) * Math.Cos(_looking.Y * Math.PI / 180)),
					(float) (Math.Cos(_looking.X * Math.PI / 180) * Math.Cos(_looking.Y * Math.PI / 180)),
					(float) Math.Sin(_looking.Y * Math.PI / 180));
				u = Vector3.Normalize(getRight());
				v = Vector3.Normalize(_up);
			}
		}

		public Vector3 _up { get; private set; }
		public Vector3 _right { get; private set; }
		public Vector3 _lookingResolved { get; private set; }

		public Vector3 coords { get; set; }

		private void setLooking(Vector3 value)
		{
			Looking = value;
		}

		private Vector3 getUp()
		{
			return _up;
		}

		private Vector3 getRight()
		{
			return _right;
		}

		public Vector4 getLooking()
		{
			return new Vector4((float) Math.Sin(_looking.X * Math.PI / 180),
				(float) Math.Cos(_looking.X * Math.PI / 180), (float) Math.Sin(_looking.Y * Math.PI / 180), _looking.Z);
		}

		public Vector4 getLooking(Vector2 offset)
		{
			return new Vector4((float) Math.Sin((_looking.X + offset.X) * Math.PI / 180),
				(float) Math.Cos((_looking.X + offset.X) * Math.PI / 180), (float) Math.Sin(
					(_looking.Y + offset.Y) * Math.PI / 180), _looking.Z);
		}

		public Vector4 getLooking(int offset)
		{
			return new Vector4((float) Math.Sin((_looking.X + offset) * Math.PI / 180),
				(float) Math.Cos((_looking.X + offset) * Math.PI / 180), (float) Math.Sin(
					(_looking.Y + offset) * Math.PI / 180), _looking.Z);
		}

		public Vector3 getLookingNoRot()
		{
			return _lookingResolved;
		}

		public Vector3 getLookingNoRot(Vector2 offset)
		{
			return _lookingResolved + u * offset.X + v * offset.Y;
			/*return new Vector3((float) (Math.Sin((_looking.X + offset.X) * Math.PI / 180) * Math.Cos((_looking.Y + offset.Y) * Math.PI / 180)), 
				(float) (Math.Cos((_looking.X + offset.X) * Math.PI / 180) * Math.Cos((_looking.Y + offset.Y) * Math.PI / 180)), 
				(float) Math.Sin((_looking.Y + offset.Y) * Math.PI / 180));*/
		}

		public void SetLooking(float yaw, float pitch, float rot)
		{
			setLooking(new Vector3(yaw, pitch, rot));
		}

		public void AddLooking(Vector3 vec)
		{
			SetLooking(_looking.X + vec.X, _looking.Y + vec.Y, _looking.Z + vec.Z);
		}

		public void AddLooking(Vector2 vec)
		{
			SetLooking(_looking.X + vec.X, Math.Max(Math.Min(_looking.Y + vec.Y, 90), -90), _looking.Z);
		}
	}
}