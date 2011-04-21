namespace Orbit.Configuration
{
	public struct CustomColor
	{
		public byte R;
		public byte G;
		public byte B;

		public CustomColor(byte r, byte g, byte b)
		{
			R=r;
			G=g;
			B=b;
		}

		public static implicit operator System.Drawing.Color(CustomColor color)
		{
			return System.Drawing.Color.FromArgb(color.R, color.G, color.B);
		}
	}
}