using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDisplay : MonoBehaviour
{
    public static PuzzleDisplay instance;

    private PuzzleManager puzzleManagerInstance;

    private int patternLength;

    [SerializeField] private List<GameObject> patternObjects;
    [SerializeField] private PuzzleColors[] colorList;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material yellow;
    [SerializeField] private Material purple;

    public GameObject newPatternEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        puzzleManagerInstance = PuzzleManager.instance;

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

    }

    public void FetchPattern(PuzzleColors[] pattern, int length)
    {
        colorList = pattern;
        patternLength = length;

        ChangeVisuals();
    }

    private void ChangeVisuals()
    {
        PuzzleColors color;
        if (newPatternEffect != null)
        {
            Instantiate(newPatternEffect, patternObjects[2].transform.position, Quaternion.identity, transform);
        }
        for (int i = 0; i < patternLength; i++)
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
