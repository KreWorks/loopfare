using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
	FareColor fareColor;

	public void SetFareColor(FareColor fc)
	{
		fareColor = fc;
	}

	public FareColor GetFareColor()
	{
		return fareColor;
	}
}
