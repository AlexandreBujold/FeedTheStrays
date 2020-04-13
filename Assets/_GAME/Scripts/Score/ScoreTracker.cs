using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;

    [Header("Score limit")]
    public int scoreLimit;

    [Space]
    [Header("Player scores")]
    public int player1Score = 0;
    public int player2Score = 0;

    [Space]
    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Space]
    [Header("Text Objects")]
    public TextMeshPro player1ScoreText;
    public TextMeshPro player2ScoreText;

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

    private void Update()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    public void IncreaseScore(GameObject player)
    {
        if (player.transform.parent.parent.gameObject == player1)
        {
            player1Score++;
        }
        else if(player.transform.parent.parent.gameObject == player2)
        {
            player2Score++;
        }
    }
}
