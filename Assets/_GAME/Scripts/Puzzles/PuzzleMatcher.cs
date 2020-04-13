using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleMatcher : MonoBehaviour
{
    public static PuzzleMatcher instance;

    [Header("Puzzle Manager Instance")]
    [SerializeField] private PuzzleManager manager;
    public ItemLauncher launcher;

    [Space]
    [Header("Player References")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Space]
    [Header("Player Pattern Variables")]
    [SerializeField] private int patternLength;
    [SerializeField] private PuzzleColors[] player1Pattern;
    [SerializeField] private PuzzleColors[] player2Pattern;
    [SerializeField] private int player1Increment = 0;
    [SerializeField] private int player2Increment = 0;

    private void OnEnable()
    {
        if (instance == null)
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
        manager = PuzzleManager.instance;
    }

    public void CreateSizedLists()
    {
        patternLength = manager.GetPatternLength();

        player1Pattern = new PuzzleColors[patternLength];
        player2Pattern = new PuzzleColors[patternLength];
    }

    public void AddValueToList(PuzzleColors color, GameObject player)
    {
        Debug.Log("Called by " + player.name, player);
        if (player == player1)
        {
            player1Pattern[player1Increment] = color;
            player1Increment++;

            if (player1Increment == patternLength)
            {
                CheckPatternMatch(player1Pattern, player);
            }
        }
        else if (player == player2)
        {
            player2Pattern[player2Increment] = color;
            player2Increment++;

            if (player2Increment == patternLength)
            {
                CheckPatternMatch(player2Pattern, player);
            }
        }
        else
        {
            Debug.LogWarning("Object not of type Player. Functionality skipped");
        }
    }

    private void CheckPatternMatch(PuzzleColors[] pattern, GameObject player)
    {
        if (player == player1)
        {
            if (Enumerable.SequenceEqual(pattern, manager.pattern))
            {
                Debug.Log("Both arrays equal. Give point to player 1");
                manager.playerMatchPattern = true;
                if (launcher != null)
                {
                    launcher.LaunchItemAtPlayer1();
                }
                ClearPlayerArrays();
                //Do further code here
            }
            else
            {
                Debug.Log("Both arrays inequal. Resetting player 1 array");
                Array.Clear(player1Pattern, 0, patternLength);
                player1Increment = 0;
            }
        }
        else if (player == player2)
        {
            if (Enumerable.SequenceEqual(pattern, manager.pattern))
            {
                Debug.Log("Both arrays equal. Give point to player 2");
                manager.playerMatchPattern = true;
                if (launcher != null)
                {
                    launcher.LaunchItemAtPlayer2();
                }
                ClearPlayerArrays();
                //Do further code here
            }
            else
            {
                Debug.Log("Both arrays inequal. Resetting player 2 array");
                Array.Clear(player2Pattern, 0, patternLength);
                player2Increment = 0;
            }
        }
    }

    public void ClearPlayerArrays()
    {
        Array.Clear(player1Pattern, 0, patternLength);
        Array.Clear(player2Pattern, 0, patternLength);
        player1Increment = 0;
        player2Increment = 0;
    }
}
