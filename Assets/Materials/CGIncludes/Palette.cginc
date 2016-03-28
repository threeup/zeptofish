#ifndef PALETTE_INCLUDED
#define PALETTE_INCLUDED

#include "UnityCG.cginc"

half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
    half NdotL = dot (s.Normal, lightDir);
    half4 c;
    c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
    c.a = s.Alpha;
    return c;
}

inline fixed3 Palletize(fixed3 color, sampler2D paletteTex)
{
    
    float hue = 0;
    float sat = 0;
    float bright = 0;
    float mx = max(color.r, max(color.g, color.b));
    float mn = min(color.r, min(color.g, color.b));
    float dif = mx - mn;
 
    
    if (color.g == mx)
    {
        hue = (color.b - color.r) / dif * 0.16667 + 0.33333;
    }
    else if (color.b == mx)
    {
        hue = (color.r - color.g) / dif * 0.16667 + 0.66667;
    }
    else if (color.b > color.g)
    {
        hue = (color.g - color.b) / dif * 0.16667 + 1;
    }
    else
    {
        hue = (color.g - color.b) / dif * 0.16667;
    }
    if (hue < 0)
    {
        hue = hue + 1;
    }
 
    sat = clamp(dif / mx, 0, 1);
    bright = mx;
        
    
    hue = clamp(round(hue*24)/24, 0.01, 0.99);
    sat = round(sat*3)/3;
    bright = clamp(round(bright*3)/3, 0.01, 0.99);

    float position = sat*3+bright;
    float2 paletteUV = float2(hue, position/4); 
            
    return tex2D(paletteTex, paletteUV).rgb;
        
    
}

#endif