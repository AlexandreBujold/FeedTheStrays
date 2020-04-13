using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Food Properties")]
    public float hungerDecreasePerFood = 25f;
    public float hungerDepletionRatePerSecond = 1;

    [Space]
    public List<Cat> cats;

    private Coroutine catHunger;

    //Collider on this object to detect when food has arrived

    // Start is called before the first frame update
    void Start()
    {
        StartHungerDepletion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FeedCat()
    {
        if (cats != null)
        {
            float hunger = 0;
            Cat chosenCat = cats[0];
            foreach (Cat cat in cats)
            {
                if (cat.hunger >= hunger)
                {
                    hunger = cat.hunger;
                    chosenCat = cat;
                }
            }

            if (chosenCat != null)
            {
                chosenCat.Feed(hungerDecreasePerFood);
            }
        }
    }

    public void StartHungerDepletion()
    {
        if (catHunger == null)
        {
            catHunger = StartCoroutine(CatHunger());
        }
    }

    public IEnumerator CatHunger()
    {
        for (; ; )
        {
            if (cats != null)
            {
                foreach (Cat cat in cats)
                {
                    cat.IncreaseHunger();
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
