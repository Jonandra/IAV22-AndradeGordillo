using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Terrain generator.
/// </summary>
public class TerrainGenerator : MonoBehaviour
{
  

    public GameObject cubePrefab;
    public int worldWidthX;
    public int worldWidthZ;

    public float lowerScaleValue;
    public float highestScaleValue;

    public float lowerHeightModifierValue;
    public float highestHeightModifierValue;

    private float noiseScale;
    private float noiseScaleModifier;

    PerlinNoise noiseFunc;
   
    /// Generates the cubes as children of a GameObject.    
    public void InitalizeCubes()
    {
        for (float x = 0; x <= this.worldWidthX; ++x)
        {
            for (float z = 0; z <= this.worldWidthZ; ++z)
            {
                GameObject cube = Instantiate(
                    this.cubePrefab,
                    new Vector3(x, 0, z),
                    this.cubePrefab.transform.rotation
                );
                cube.transform.parent = this.transform;
            }
        }
    }



    public void ApplyHeightToCube(Transform cube, float cubeHeight)
    {
        int newHeight = Mathf.RoundToInt(cubeHeight * this.noiseScaleModifier);
        Vector3 newPosition = new Vector3(cube.transform.position.x, newHeight, cube.transform.position.z);
        cube.transform.position = newPosition;
    }

 
    /// Actualiza la posicion de los cubos en funcion de ruido Perlin
   
    public void GenerateCubes()
    {
        foreach (Transform cube in transform)
        {
           float noiseScaleAddition = Random.Range(this.noiseScale / 10, this.noiseScale / 9);
            float cubeHeight = noiseFunc.GetNoise(
                (cube.transform.position.x / (this.noiseScale + noiseScaleAddition)),
                (cube.transform.position.z / (this.noiseScale + noiseScaleAddition))
            );           
      
            ApplyHeightToCube(cube, cubeHeight);
        }
    }

 

    private void Awake()
    {
        noiseFunc = noiseFunc = GetComponent<PerlinNoise>();
    }
    void Start()
    {
        this.noiseScale = Random.Range(this.lowerScaleValue, this.highestScaleValue);
        this.noiseScaleModifier = Random.Range(this.lowerHeightModifierValue, this.highestHeightModifierValue);

        this.InitalizeCubes();
        this.GenerateCubes();
    }
        
}