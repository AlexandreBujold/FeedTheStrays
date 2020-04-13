using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LWRP;
using UnityEngine.Rendering.PostProcessing;

public class CatPostProcessing : MonoBehaviour
{

    public PostProcessVolume volume;
    public float tickRate = 0.1f;

    public List<CatManager> catManagers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVolume());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator UpdateVolume()
    {
        for (; ; )
        {
            if (catManagers != null)
            {
                float weight = 0;
                int catsAlive = 0;
                int catsDead = 0;
                int total = 0;
                foreach (CatManager catManager in catManagers)
                {
                    foreach (Cat cat in catManager.cats)
                    {
                        if (cat.dead)
                        {
                            catsDead++;
                        }
                        else
                        {
                            catsAlive++;
                        }
                        total++;
                    }
                }

                weight = (float)catsDead / (float)total;

                if (volume != null)
                {
                    volume.weight = weight;
                }
            }
            yield return new WaitForSeconds(tickRate);
        }
    }
}
