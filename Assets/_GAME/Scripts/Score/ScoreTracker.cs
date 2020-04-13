using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    [Header("Score limit")]
    public int scoreLimit;

    [Space]
    [Header("Player scores")]
    public int player1Score = 0;
    public int player2Score = 0;

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

    public void IncreaseScore(GameObject player)
    {

    }
}
