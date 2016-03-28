using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreatePaletteEditor {

	private const int MaxColors = 256;
    
    private const int HueCount = 24;
    private const int RowCount = 16;

	private CreatePalette script;
    
    private static string loadpath = "";


    [MenuItem("Tools/CreatePalette")]
	private static void CreatePalette() 
    {

		// Load the color image to a Texture2D
		Texture2D colorTexture = LoadTexture();
		if (colorTexture == null) return;

		// Create a list of all the unique colors in the color image
		List<Color> paletteColors = new List<Color>();
		bool proceed = false;
		for (int x = 0; x < colorTexture.width; x++)
			for (int y = 0; y < colorTexture.height; y++)
				if (!paletteColors.Contains(colorTexture.GetPixel(x, y))) {
					paletteColors.Add(colorTexture.GetPixel(x, y));
					if (paletteColors.Count > MaxColors && !proceed) {
						proceed = EditorUtility.DisplayDialog("Error", "Source image contains more than " + MaxColors + " colors. Continuing may lock up Unity for a long time", "Continue", "Stop");
						if (!proceed)
							return;
					}
				}
         
        Debug.LogWarning("paletteColors "+paletteColors.Count +" W"+colorTexture.width+" H"+colorTexture.height);

        List<HSBColor> hsbColors = GetSortedHSB(paletteColors);
        
        
        Texture2D paletteTexture = new Texture2D(HueCount, RowCount, TextureFormat.RGB24, false);
		paletteTexture.name = colorTexture.name + "_pal";
        
        int colorIdx = 0;
        int workingHue = 0;
        int lastHue = 0;
        for(int x=0; x<HueCount; ++x)
        {
            lastHue = x;
            List<HSBColor> matches = new List<HSBColor>();
            for(int c=0; c < paletteColors.Count; ++c)
            {
                HSBColor color = hsbColors[c];
                workingHue = color.RoundedHue(HueCount);
                if( Mathf.Abs(workingHue - lastHue) <= 0 )
                { 
                    matches.Add(color);
                }
            }
            
            if( matches.Count > 0 )
            {
                float sum = 0f;
                foreach(HSBColor c in matches)
                {
                    sum += c.h;
                }
                float averageHue = sum / matches.Count;
                HSBColor hsbcolor = new HSBColor(averageHue,0,0);    
                for(int ss=0; ss<4; ++ss)
                {
                    for(int bb=0; bb<4; ++bb)
                    {
                        hsbcolor.s = ss/3f;
                        hsbcolor.b = bb/3f;
                        paletteTexture.SetPixel(x, ss*4+bb, HSBColor.ToColor(hsbcolor));
                    }    
                }
            }
            else
            {
                for(int y=0; y<RowCount; ++y)
                {
                    if ( x == 0  )
                    {
                        paletteTexture.SetPixel(x, y, Color.black);
                    }
                    else 
                    {
                        paletteTexture.SetPixel(x, y, paletteTexture.GetPixel( x-1, y ));
                    }
                }
            }
        }

		// Save the palette image
		SaveTexture(paletteTexture);
	}
    
    private static List<HSBColor> GetSortedHSB(List<Color> colors)
    {
        List<HSBColor> hsbs = new List<HSBColor>();
        foreach(Color c in colors)
        {
            hsbs.Add(HSBColor.FromColor(c));
        }
        hsbs.Sort((h1,h2)=>h1.TopLeft.CompareTo(h2.TopLeft));
        return hsbs;
    }

	/// <summary>
	///  Opens a file dialog and loads a .png image to a Texture2D.
	/// </summary>
	private static Texture2D LoadTexture() {
		loadpath = EditorUtility.OpenFilePanel("Select your .PNG color image", "", "png");
		if (loadpath.Length == 0) return null;

		Texture2D texture = new Texture2D(4, 4, TextureFormat.ARGB32, false);
		texture.name = Path.GetFileNameWithoutExtension(loadpath);

		new WWW("file://" + loadpath).LoadImageIntoTexture(texture);

		return texture;
	}

	/// <summary>
	///  Opens a file dialog and saves a Texture2D to a .png image.
	/// </summary>
	private static void SaveTexture(Texture2D texture) {
        string loadpathdir = Path.GetDirectoryName(loadpath);
		string path = EditorUtility.SaveFilePanel("Save your .PNG palette image", loadpathdir, texture.name + ".png", "png");
		if (path.Length == 0) return;

		byte[] bytes = texture.EncodeToPNG();
		File.WriteAllBytes(path, bytes);
	}


}