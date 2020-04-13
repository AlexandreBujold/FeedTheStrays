using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPuzzleVisual : MonoBehaviour
{
    public GameObject currentPlayer;

    public GameObject player1;
    public GameObject player2;

    public PuzzleColors[] colorList;

    private List<GameObject> patternObjects;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material yellow;
    [SerializeField] private Material purple;

    private void Awake()
    {
        patternObjects = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            patternObjects.Add(child.gameObject);
        }
    }

    private void OnEnable()
    {
        red = Resources.Load<Material>("Red");
        blue = Resources.Load<Material>("Blue");
        green = Resources.Load<Material>("Green");
        yellow = Resources.Load<Material>("Yellow");
        purple = Resources.Load<Material>("Purple");
    }

    private void Update()
    {
        GetPlayerPuzzle();

        if (colorList != null)
        {
            SetChildColors();
        }
    }

    private void GetPlayerPuzzle()
    {
        if (currentPlayer == player1)
        {
            colorList = PuzzleMatcher.instance.player1Pattern;
        }
        else if(currentPlayer == player2)
        {
            colorList = PuzzleMatcher.instance.player2Pattern;
        }
    }

    private void SetChildColors()
    {
        PuzzleColors color;

        for (int i = 0; i < 5; i++)
        {
            color = colorList[i];

            switch (color)
            {
                case PuzzleColors.RED:
                    patternObjects[i].GetComponent<Renderer>().material = red;
                    break;

                case PuzzleColors.BLUE:
                    patternObjects[i].GetComponent<Renderer>().material = blue;
                    break;

                case PuzzleColors.GREEN:
                    patternObjects[i].GetComponent<Renderer>().material = green;
                    break;

                case PuzzleColors.YELLOW:
                    patternObjects[i].GetComponent<Renderer>().material = yellow;
                    break;

                case PuzzleColors.PURPLE:
                    patternObjects[i].GetComponent<Renderer>().material = purple;
                    break;

                default:
                    patternObjects[i].GetComponent<Renderer>().material = red;
                    break;
            }
        }
    }
}
