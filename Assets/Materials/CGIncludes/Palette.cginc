#ifndef PALETTE_INCLUDED
#define PALETTE_INCLUDED

#include "UnityCG.cginc"


inline fixed3 GetPaletteColor(fixed3 color, sampler2D paletteTex,
							 float paletteHeight, float colorCount) {
	// To find the palette color to use for this pixel:
	//	The row offset decides which row of color squares to use.
	//	The red component decides which column of color squares to use.
	//	The green and blue components points to the color in the 16x16 pixel square.
	
	float2 paletteUV = float2(
		min(floor(color.r * 16), 15) / 16 + clamp(color.b * 16, 0.5, 15.5) / 256,
		(clamp(color.g * 16, 0.5, 15.5) + floor(colorCount) * 16) / paletteHeight);

	// Return the new color from the palette texture
	return tex2D(paletteTex, paletteUV).rgb;
}

#endif