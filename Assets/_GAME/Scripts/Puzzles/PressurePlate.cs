using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private PuzzleColors color;

    public PlateEvent onPressed;

    public Material pressedColor;
    private Material original;

    private void Start()
    {
        if (onPressed == null)
        {
            onPressed = new PlateEvent();
        }
        original = GetComponentInChildren<MeshRenderer>().material;
    }

    private void ChangeColor()
    {
        MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        if (mesh != null && pressedColor != null)
        {
            mesh.material = pressedColor;
            Invoke("ResetColor", 1f);
        }
    }

    private void ResetColor()
    {
        if (original != null)
        {
            MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material = original;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //This will be changed when players are in. Right now it collides with everythings
            onPressed.Invoke(color, other.gameObject);
            ChangeColor();
        }
    }
}

[System.Serializable]
public class PlateEvent : UnityEvent<PuzzleColors, GameObject> { }
