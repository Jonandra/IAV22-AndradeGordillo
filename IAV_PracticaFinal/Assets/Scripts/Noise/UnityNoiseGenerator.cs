
using UnityEngine;
using System.Collections;

// Create a texture and fill it with Perlin noise.
// Try varying the xOrg, yOrg and scale values in the inspector
// while in Play mode to see the effect they have on the noise.

public class UnityNoiseGenerator : MonoBehaviour
{
    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    PerlinNoise noiseFunc;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;

    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;


    void Awake()
    {
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        noiseFunc = GetComponent<PerlinNoise>();
    }
    void Start()
    {

        xOrg = Random.Range(0f,10000f);
        yOrg = Random.Range(0f,10000f);

        rend = GetComponent<Renderer>();
       
        rend.material.mainTexture = noiseTex;
       
    }
    private void Update()
    {
        CalcNoise();
    }

    void CalcNoise()
    {
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = noiseFunc.GetNoise(xCoord,yCoord);
               
                          
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

      
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

   
}