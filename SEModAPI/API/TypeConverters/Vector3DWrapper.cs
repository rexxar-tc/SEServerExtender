﻿namespace SEModAPI.API.TypeConverters
{
	using VRage;
	using VRageMath;

	public struct Vector3DWrapper
	{
		private Vector3D _vector;

		public Vector3DWrapper(double x, double y, double z)
		{
			_vector.X = x;
			_vector.Y = y;
			_vector.Z = z;
		}
		public Vector3DWrapper(SerializableVector3D v)
		{
			_vector = v;
		}
		public Vector3DWrapper(Vector3D v)
		{
			_vector = v;
		}

		public double X
		{
			get { return _vector.X; }
			set { _vector.X = value; }
		}
		public double Y
		{
			get { return _vector.Y; }
			set { _vector.Y = value; }
		}
		public double Z
		{
			get { return _vector.Z; }
			set { _vector.Z = value; }
		}

		public static implicit operator Vector3DWrapper(SerializableVector3D v)
		{
			return new Vector3DWrapper(v);
		}

		public static implicit operator Vector3DWrapper(Vector3D v)
		{
			return new Vector3DWrapper(v);
		}

		public static implicit operator SerializableVector3D(Vector3DWrapper v)
		{
			return new SerializableVector3D(v.X, v.Y, v.Z);
		}

		public static implicit operator Vector3D(Vector3DWrapper v)
		{
			return new Vector3D(v.X, v.Y, v.Z);
		}

		public override string ToString()
		{
			return _vector.ToString();
		}
	}
}