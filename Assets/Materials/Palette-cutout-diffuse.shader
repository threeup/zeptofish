Shader "Palette Shaders/Diffuse" {
	Properties {
        _Color ("Base", Color) = (1,1,1,1)
        _HighlightColor ("Highlight", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_ColorCount ("Mixed Color Count", float) = 4
		_PaletteHeight ("Palette Height", float) = 128
		_PaletteTex ("Palette", 2D) = "black" {}
		_Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
	}

	SubShader {
		Tags { "IgnoreProjector"="True" "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
		LOD 200

		BlendOp Max

		CGPROGRAM
		#pragma surface surf SimpleLambert vertex:vert finalcolor:dither alphatest:_Cutoff
		#include "CGIncludes/Palette.cginc"

		float4 _Color;
		float4 _HighlightColor;
        sampler2D _MainTex;
		sampler2D _PaletteTex;
		float _ColorCount;
		float _PaletteHeight;
		
       
		struct Input {
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}

		void surf(Input i, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, i.uv_MainTex)*_Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		void dither(Input i, SurfaceOutput o, inout fixed4 color) {
			color.rgb = GetPaletteColor(color.rgb, _PaletteTex,
				_PaletteHeight, _ColorCount);
            //color *= _HighlightColor;
		}
		ENDCG
	}

	Fallback "Transparent/Cutout/Diffuse"
}