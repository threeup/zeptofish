Shader "Palette Shaders/HSV" {
	Properties {
        _Color ("Base", Color) = (1,1,1,1)
        _HighlightColor ("Highlight", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_PaletteTex ("Palette", 2D) = "black" {}
	}

	SubShader {
		Tags { "IgnoreProjector"="True" "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
		LOD 200

		BlendOp Max

		CGPROGRAM
		#pragma surface surf SimpleLambert vertex:vert finalcolor:palettize alphatest:_Cutoff
		#include "CGIncludes/Palette.cginc"

		float4 _Color;
		float4 _HighlightColor;
        sampler2D _MainTex;
		sampler2D _PaletteTex;
		
       
		struct Input {
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
		}

		void surf(Input i, inout SurfaceOutput o) {
			fixed4 color = tex2D(_MainTex, i.uv_MainTex)*_Color;
            //color.rgb = Palletize(color.rgb, _PaletteTex);
            o.Albedo = color.rgb;
			o.Alpha = color.a;
		}

		void palettize(Input i, SurfaceOutput o, inout fixed4 color) {
			color.rgb = Palletize(color.rgb, _PaletteTex);
            color *= _HighlightColor;
		}
		ENDCG
	}

	Fallback "Transparent/Cutout/Diffuse"
}