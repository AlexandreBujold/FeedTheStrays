using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternGenerator
{
    private static int colorEnumLength = (int)PuzzleColors.LengthOperator;

    //private void OnEnable()
    //{
    //    colorEnumLength = (int)PuzzleColors.LengthOperator; //Length
    //}

    public static PuzzleColors[] GeneratePattern(int length)
    {
        Debug.Log("Called");
        PuzzleColors[] newPattern = new PuzzleColors[length];

        for(int i=0;i<length;i++)
        {
            PuzzleColors color = (PuzzleColors)Random.Range(0, colorEnumLength);
            newPattern[i] = color;
        }

        return newPattern;
    }
}
