using UnityEngine;
using System.Collections;

namespace AlpacaSound
{
	[ExecuteInEditMode]
	[RequireComponent (typeof(Camera))]
	public class RetroPixel : MonoBehaviour
	{

		public int horizontalResolution = 600;
		public int verticalResolution = 800;

		public Color color0 = Color.black;
		public Color color1 = Color.white;
		public Color color2 = new Color32(255, 75, 75, 255);
		public Color color3 = new Color32(255, 186, 19, 255);
		public Color color4 = new Color32(234, 233, 0, 255);
		public Color color5 = new Color32(132, 207, 69, 255);
		public Color color6 = new Color32(0, 165, 202, 255);
		public Color color7 = new Color32(192, 106, 194, 255);

        public Shader shader;
        public string shaderName;

		Material m_material;
		Material material
		{
			get
			{
				if (m_material == null)
				{
                    if( shader == null )
                    {
                        string baseShaderName = "AlpacaSound/RetroPixel";
                        shader = Shader.Find (baseShaderName);
                    }                    
					
					m_material = new Material (shader);
					m_material.hideFlags = HideFlags.DontSave;
				}
				return m_material;
			} 
		}

		void Start ()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				enabled = false;
				return;
			}
		}
		
		public void OnRenderImage (RenderTexture src, RenderTexture dest)
		{
			horizontalResolution = Mathf.Clamp(horizontalResolution, 1, 2048);
			verticalResolution = Mathf.Clamp(verticalResolution, 1, 2048);

			if (material)
			{
				
				material.SetColor ("_Color0", color0);
				material.SetColor ("_Color1", color1);
				material.SetColor ("_Color2", color2);
				material.SetColor ("_Color3", color3);
				material.SetColor ("_Color4", color4);
				material.SetColor ("_Color5", color5);
				material.SetColor ("_Color6", color6);
				material.SetColor ("_Color7", color7);
				
				RenderTexture scaled = RenderTexture.GetTemporary (horizontalResolution, verticalResolution);
				scaled.filterMode = FilterMode.Point;
				Graphics.Blit (src, scaled, material);
				Graphics.Blit (scaled, dest);
				RenderTexture.ReleaseTemporary (scaled);
				
			}
			else
			{
				Graphics.Blit (src, dest);
			}
		}

		void OnDisable ()
		{
			if (m_material)
			{
				Material.DestroyImmediate (m_material);
			}
		}
	}
}



