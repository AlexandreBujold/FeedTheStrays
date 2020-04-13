using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatternGenerator
{
    private static int colorEnumLength = (int)PuzzleColors.LengthOperator; //Gets length of the enum using the LengthOperator


    /// <summary>
    /// Method that generates a pattern of color types.
    /// </summary>
    /// <param name="length">Length of the array needed</param>
    /// <returns>Returns array of type PuzzleColors</returns>
    public static PuzzleColors[] GeneratePattern(int length)
    {
        //Debug.Log("Called");
        PuzzleColors[] newPattern = new PuzzleColors[length];

        for (int i = 0; i < length; i++)
        {
            PuzzleColors color = (PuzzleColors)Random.Range(0, colorEnumLength);

            if (i > 0)
            {
                while (color == newPattern[i - 1])
                {
                    color = (PuzzleColors)Random.Range(0, colorEnumLength);
                }
            }
            newPattern[i] = color;
        }

        return newPattern;
    }
}
