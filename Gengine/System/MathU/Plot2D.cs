namespace Gengine.System.MathU
{
	public class Plot2D
	{
		public float X { get; private set; }
		public float Y { get; private set; }
		public float Rot { get; private set; }

		public Plot2D() => Zero();
		public Plot2D(float x, float y, float rot)
		{
			X = x;
			Y = y;
			Rot = rot;
		}

		public void AdjustPosition(float xOffset, float yOffset)
		{
			X += xOffset;
			Y += yOffset;
		}
		
		public void Transform(Plot2D offset)
		{
			AdjustPosition(offset.X, offset.Y);
			Rotate(offset.Rot);
		}

		public void NewPlot(Plot2D offset)
		{
			Move(offset);
			NewRotation(offset.Rot);
		}
		public void Move(float xOffset, float yOffset)
		{
			X = xOffset;
			Y = yOffset;
		}

		public void Zero()
		{
			X = 0.0f;
			Y = 0.0f;
			Rot = 0.0f;
		}

		public void Adjust2D(Plot2D offset) => AdjustPosition(offset.X, offset.Y);
		public void Move(Plot2D offset) => Move(offset.X, offset.Y);
		public void NewRotation(float degree) => Rot = degree;
		public void Rotate(float degree) => Rot += degree;

		public override string ToString() => base.ToString();
		public override bool Equals(object obj) => base.Equals(obj);
		public override int GetHashCode() => base.GetHashCode();
	}
}
