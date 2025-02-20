using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Hole hole;

    private List<Hole> holes;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnObjects();
            float waitTime = Random.Range(30f, 40f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnObjects()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3.3f, -4f), 0);

        // spawn hole
        Instantiate(hole, randomPos, Quaternion.identity);
    }
}