using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private PuzzleColors color;

    public PlateEvent onPressed;

    private void Start()
    {
        if (onPressed == null)
        {
            onPressed = new PlateEvent();
        }

        onPressed.AddListener(PuzzleMatcher.instance.AddValueToList);
    }

    private void OnTriggerEnter(Collider other)
    {
        //This will be changed when players are in. Right now it collides with everythings
        onPressed.Invoke(color, other.gameObject);
    }
}

[System.Serializable]
public class PlateEvent : UnityEvent<PuzzleColors, GameObject> { }
