namespace Win32.GDI
{
	/// <summary>
	/// Specifies a raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.
	/// </summary>
	public enum RasterOperation:long
	{
		/// <summary>
		/// Fills the destination rectangle using the color associated with index 0 in the physical palette. (This color is black for the default physical palette.)
		/// </summary>
		Blackness=0x00000042,
		/// <summary>
		/// Windows 98/Me, Windows 2000/XP: Includes any windows that are layered on top of your window in the resulting image. By default, the image only contains your window. Note that this generally cannot be used for printing device contexts.
		/// </summary>
		CaptureBlit=0x40000000,
		/// <summary>
		/// Inverts the destination rectangle.
		/// </summary>
		DestinationInvert=0x00550009,
		/// <summary>
		/// Merges the colors of the source rectangle with the brush currently selected in hdcDest, by using the Boolean AND operator.
		/// </summary>
		MergeCopy=0x00C000CA,
		/// <summary>
		/// Merges the colors of the inverted source rectangle with the colors of the destination rectangle by using the Boolean OR operator.
		/// </summary>
		MergePaint=0x00BB0226,
		/// <summary>
		/// Windows 98/Me, Windows 2000/XP: Prevents the bitmap from being mirrored.
		/// </summary>
		NoMirrorBitmap=0x80000000,
		/// <summary>
		/// Copies the inverted source rectangle to the destination.
		/// </summary>
		NotSourceCopy=0x00330008,
		/// <summary>
		/// Combines the colors of the source and destination rectangles by using the Boolean OR operator and then inverts the resultant color.
		/// </summary>
		NotSourceErase=0x001100A6,
		/// <summary>
		/// Copies the brush currently selected in hdcDest, into the destination bitmap.
		/// </summary>
		PatternCopy=0x00F00021,
		/// <summary>
		/// Combines the colors of the brush currently selected in hdcDest, with the colors of the destination rectangle by using the Boolean XOR operator.
		/// </summary>
		PatternInvert=0x005A0049,
		/// <summary>
		/// Combines the colors of the brush currently selected in hdcDest, with the colors of the inverted source rectangle by using the Boolean OR operator. The result of this operation is combined with the colors of the destination rectangle by using the Boolean OR operator.
		/// </summary>
		PatternPaint=0x00FB0A09,
		/// <summary>
		/// Combines the colors of the source and destination rectangles by using the Boolean AND operator.
		/// </summary>
		SourceAnd=0x008800C6,
		/// <summary>
		/// Copies the source rectangle directly to the destination rectangle.
		/// </summary>
		SourceCopy=0xCC0020,
		/// <summary>
		/// Combines the inverted colors of the destination rectangle with the colors of the source rectangle by using the Boolean AND operator.
		/// </summary>
		SourceErase=0x00440328,
		/// <summary>
		/// Combines the colors of the source and destination rectangles by using the Boolean XOR operator.
		/// </summary>
		SourceInvert=0x00660046,
		/// <summary>
		/// Combines the colors of the source and destination rectangles by using the Boolean OR operator.
		/// </summary>
		SourcePaint=0x00EE0086,
		/// <summary>
		/// Fills the destination rectangle using the color associated with index 1 in the physical palette. (This color is white for the default physical palette.)
		/// </summary>
		Whiteness=0x00FF0062
	}
}