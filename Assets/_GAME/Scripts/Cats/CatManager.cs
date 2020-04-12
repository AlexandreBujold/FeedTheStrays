using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    [Header("Cat Properties")]
    public float hungerDepletionRatePerSecond = 1;

    [Space]
    public List<Cat> cats;

    private Coroutine catHunger;

    // Start is called before the first frame update
    void Start()
    {
        StartHungerDepletion();
    }

    // Update is called once per frame
    void Update()
    {

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
