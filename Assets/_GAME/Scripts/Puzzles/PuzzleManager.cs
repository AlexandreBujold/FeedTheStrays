using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [Header("Pattern Length")]
    [SerializeField] private int patternLength; //Length of the pattern

    [Space]
    [Header("Matched pattern bool")]
    [SerializeField] private bool playerMatchPattern = false; //Boolean to determine if any player has matched the pattern

    [Space]
    [Header("Pattern")]
    [SerializeField] public PuzzleColors[] pattern; //The list that will contain the pattern

    public UnityEvent patternEvent;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        InitializePattern(patternLength);
    }

    private IEnumerator WaitForPatternMatch()
    {
        yield return new WaitUntil(() => playerMatchPattern == true);
        //Logic here for progressing stuff
        yield break;
    }

    public void InitializePattern(int length)
    {
        pattern = PatternGenerator.GeneratePattern(length);
    }
}
