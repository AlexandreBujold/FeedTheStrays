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
    public GameObject takenEffect;
    public GameObject fedEffect;

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

    public Cat FeedCat()
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
                return chosenCat;
            }
        }
        return null;
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
                    cat.IncreaseHunger(hungerDepletionRatePerSecond);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item") == true)
        {
            if (takenEffect != null)
            {
                Instantiate(takenEffect, other.gameObject.transform.position, Quaternion.identity, transform);
            }

            Destroy(other.gameObject);
            Cat cat = FeedCat();

            if (fedEffect != null && cat != null)
            {
                Instantiate(fedEffect, cat.gameObject.transform.position, Quaternion.identity, cat.transform);
            }
        }
    }
}
