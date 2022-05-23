// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
	const int Arrlength = 256;
	int[] P = new int[Arrlength * 2];

	struct Vector2
	{
		float x, y;
		public Vector2(float xA, float yA)
		{
			x = xA;
			y = yA;
		}
		public float dot(Vector2 o)
		{
			return x * o.x + y * o.y;
		}
	}


	void Shuffle()
	{
		for (int n = Arrlength - 1; n > 0; n--)
		{
			int index = (Random.Range(0, n - 1));
			int temp = P[n];

			P[n] = P[index];
			P[index] = temp;
		}
	}
	void CreateArray()
	{
		for (int i = 0; i < Arrlength; i++)		
			P[i] = i;
		
		Shuffle();
		for (int i = Arrlength; i < Arrlength * 2; i++)		
			P[i] = P[i - Arrlength];		
	}

	float Fade(float t)
	{		
		return ((6 * t - 15) * t + 10) * t * t * t;
	}

	float Lerp(float t, float a1, float a2)
	{
		return a1 + t * (a2 - a1);
	}
	Vector2 GetConstantVector(int v)
	{		
		int h = v & 3;
        switch (h)
        {
			case 0:	return new Vector2(1.0f, 1.0f);
			case 1: return new Vector2(-1.0f, 1.0f);
			case 2: return new Vector2(-1.0f, -1.0f);
			default: return new Vector2(1.0f, -1.0f);
		}
	}
	public float GetNoise(float x, float y)
	{
		int X = (int)(Mathf.Floor(x)) & 255;
		int Y = (int)(Mathf.Floor(y)) & 255;

		float xf = x - Mathf.Floor(x);
		float yf = y - Mathf.Floor(y);

		Vector2 topRight = new Vector2(xf - 1.0f, yf - 1.0f);
		Vector2 topLeft = new Vector2(xf, yf - 1.0f);
		Vector2 bottomRight = new Vector2(xf - 1.0f, yf);
		Vector2 bottomLeft = new Vector2(xf, yf);

		//Select a value in the array for each of the 4 corners
		int valueTopRight = P[P[X + 1] + Y + 1];
		int valueTopLeft = P[P[X] + Y + 1];
		int valueBottomRight = P[P[X + 1] + Y];
		int valueBottomLeft = P[P[X] + Y];

		float dotTopRight = topRight.dot(GetConstantVector(valueTopRight));
		float dotTopLeft = topLeft.dot(GetConstantVector(valueTopLeft));
		float dotBottomRight = bottomRight.dot(GetConstantVector(valueBottomRight));
		float dotBottomLeft = bottomLeft.dot(GetConstantVector(valueBottomLeft));

		float u = Fade(xf);
		float v = Fade(yf);

		return Lerp(u,
			Lerp(v, dotBottomLeft, dotTopLeft),
			Lerp(v, dotBottomRight, dotTopRight)
		);
	


	}
	void Awake()
	{
		CreateArray();
		
	}
}




