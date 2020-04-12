using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float hunger = 50;
    public float maxHunger = 100;

    public void Feed(float amount)
    {
        hunger = Mathf.Clamp(hunger - amount, 0, maxHunger);
    }

    public void IncreaseHunger()
    {
        hunger = Mathf.Clamp(hunger + 1, 0, maxHunger);
    }
}
