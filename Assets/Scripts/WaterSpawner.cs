using UnityEngine;
using System.Collections;

public class WaterSpawner : MonoBehaviour
{
    public ParticleSystem waterParticles;
    public Sprite[] holes;

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
        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0);

        // Spawn water particle system
        Instantiate(waterParticles, randomPos, Quaternion.identity);

        // Spawn hole sprite
        GameObject holeObject = new GameObject("Hole");
        SpriteRenderer sr = holeObject.AddComponent<SpriteRenderer>();
        sr.sortingOrder = 5;
        sr.sprite = holes[Random.Range(0, holes.Length)];
        holeObject.transform.position = randomPos;
    }
}