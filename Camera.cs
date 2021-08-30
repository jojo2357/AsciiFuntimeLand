using System;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Quaternion = System.Numerics.Quaternion;

namespace AsciiFuntimeLand
{
	public class Camera
	{
		public Vector3 _looking;

		public Camera()
		{
			coords = new Vector3();
			setLooking(new Vector3());
		}

		public Vector3 coords { get; set; }

		private void setLooking(Vector3 value)
		{
			_looking = value;
		}

		private Vector3 getUp()
		{
			return new Vector3((float) (Math.Sin((_looking.X) * Math.PI / 180) * Math.Cos((_looking.Y + 90) * Math.PI / 180)), 
				(float) (Math.Cos((_looking.X) * Math.PI / 180) * Math.Cos((_looking.Y + 90) * Math.PI / 180)), 
				(float) Math.Sin((_looking.Y + 90) * Math.PI / 180));
		}
		
		private Vector3 getLeft()
		{
			return -Vector3.Cross(getUp(), getLookingNoRotUNMODIFIED());
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

		private Vector3 getLookingNoRotUNMODIFIED()
		{
			return new Vector3((float) (Math.Sin((_looking.X) * Math.PI / 180) * Math.Cos((_looking.Y) * Math.PI / 180)), 
				(float) (Math.Cos((_looking.X) * Math.PI / 180) * Math.Cos((_looking.Y) * Math.PI / 180)), 
				(float) Math.Sin((_looking.Y) * Math.PI / 180));
		}

		public Vector3 getLookingNoRot(Vector2 offset)
		{
			Vector3 normal = Vector3.Normalize(getLookingNoRotUNMODIFIED());
			Vector3 u = Vector3.Normalize(getLeft());
			Vector3 v = Vector3.Normalize(getUp());
			Vector3 l = normal - u / 2 - v / 2;
			return l + u * offset.X + v * offset.Y;
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