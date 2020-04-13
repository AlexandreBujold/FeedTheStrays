using System.Collections;
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
    [Header("Pattern bools")]
    [SerializeField] private bool playerMatchPattern = false; //Boolean to determine if any player has matched the pattern
    public bool canPressPad = false; //Bool to determine if pads are interactable

    [Space]
    [Header("Pattern")]
    [SerializeField] public PuzzleColors[] pattern; //The list that will contain the pattern

    [Space]
    [Header("Puzzle Visual")]
    [SerializeField] private PuzzleDisplay displayComp;

    public UnityEvent patternEvent;
    public DisplayEvent displayEvent;

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

    private void Start()
    {
        if(displayEvent == null)
        {
            displayEvent = new DisplayEvent();
        }

        displayEvent.AddListener(PuzzleDisplay.instance.FetchPattern);
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
        displayEvent.Invoke(pattern, length);
        patternLength = length;
        playerMatchPattern = false;
        canPressPad = true;
    }

    /* Method that waits until the boolean playerMatchPattern returns true
     * Will keep track of which player caused the boolean to change and will assign them a point*/
    public IEnumerator WaitForPatternMatch() 
    {
        yield return new WaitUntil(() => playerMatchPattern == true);
        Debug.Log("TRUE. NOW I DIE");

        StartCoroutine(PatternCooldown(delayTime));

        playerMatchPattern = false;
        canPressPad = false;
        yield break;
    }

    /* Method called once the pattern has been matched.
     * Enforces a delay before the next pattern is selected */
    private IEnumerator PatternCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        patternEvent.Invoke();
        yield break;
    }

    public int GetPatternLength()
    {
        return patternLength;
    }
}

[System.Serializable]
public class DisplayEvent : UnityEvent<PuzzleColors[], int> { }
