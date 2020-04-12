using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private PuzzleColors color;

    private PuzzleMatcher matcherInstance;

    public UnityEvent<PuzzleColors, GameObject> onPressed;

    private void Awake()
    {
        matcherInstance = PuzzleMatcher.instance;
    }

    private void Start()
    {
        onPressed.AddListener(matcherInstance.AddValueToList);
    }

    private void OnTriggerEnter(Collider other)
    {
        //This will be changed when players are in. Right now it collides with everythings
        onPressed.Invoke(color, other.gameObject);
    }
}
