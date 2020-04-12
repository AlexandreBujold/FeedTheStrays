using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnerState { ON, OFF }
    public List<GameObject> objectsToSpawn;
    public float startDelay = 1f;
    public float timeBetweenSpawns = 1f;
    public List<Transform> spawnPositions;
    [Space]

    private Coroutine spawningCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        SetSpawnerState(SpawnerState.ON);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpawnerState(SpawnerState state)
    {
        switch (state)
        {
            case SpawnerState.ON:
                if (spawningCoroutine == null)
                {
                    spawningCoroutine = StartCoroutine(SpawnObjects());
                }
                break;

            case SpawnerState.OFF:
                if (spawningCoroutine != null)
                {
                    StopCoroutine(spawningCoroutine);
                }
                break;
        }
    }

    public IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(startDelay);
        for (; ; )
        {

            if (null != objectsToSpawn)
            {
                if (objectsToSpawn.Count > 0)
                {
                    int randomObjToSpawnIndex = Random.Range(0, objectsToSpawn.Count);
                    if (null != spawnPositions && spawnPositions.Count > 0)
                    {
                        int randomSpawnPosIndex = Random.Range(0, spawnPositions.Count);
                        GameObject newObj = Instantiate(objectsToSpawn[randomObjToSpawnIndex], spawnPositions[randomSpawnPosIndex].position, Quaternion.identity, transform);
                    }
                }
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
