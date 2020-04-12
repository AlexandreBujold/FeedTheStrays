﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [Header("Pattern Float Variables")]
    [SerializeField] private int patternLength; //Length of the pattern. Currently unused. Can be used if event requires
    [SerializeField] private float delayTime; //Delay between pattern generations when a player completes a pattern

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
    }

    private void Update() //USED FOR TESTING THE PATTERN GEN
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            patternEvent.Invoke();
            StartCoroutine(WaitForPatternMatch());
        }
    }

    /* Method to generate a new pattern. Uses the method from
     * PatternGenerator.cs */
    public void InitializePattern(int length)
    {
        pattern = PatternGenerator.GeneratePattern(length);
    }

    /* Method that waits until the boolean playerMatchPattern returns true
     * Will keep track of which player caused the boolean to change and will assign them a point*/
    public IEnumerator WaitForPatternMatch() 
    {
        yield return new WaitUntil(() => playerMatchPattern == true);
        Debug.Log("TRUE. NOW I DIE");

        StartCoroutine(PatternCooldown(delayTime));

        playerMatchPattern = false;
        yield break;
    }

    /* Method called once the pattern has been matched.
     * Enforces a delay before the next pattern is selected */
    private IEnumerator PatternCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        patternEvent.Invoke();
    }
}
