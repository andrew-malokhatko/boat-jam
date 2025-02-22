 using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public Hole hole;
    public Wheel wheel;

    // Max time for each object in seconds
    private const float maxHoleTime = 25.0f;
    private const float maxWheelTime = 12.0f;

    private float timeSinceWheel = 0.0f;
    private float timeSinceHole = 0.0f;

    private void Start()
    {
        StartCoroutine(HoleSpawnLoop());
        StartCoroutine(SteeringActionLoop());
    }

    private void Update()
    {
        if (hole)
        {
            if (timeSinceHole > maxHoleTime)
            {
                Debug.Log("End game");
				SceneManager.LoadScene("Menu");
                // Game End
            }

            timeSinceHole += Time.deltaTime;
        }

        if (wheel.requiresSteering)
        {
            if (timeSinceWheel > maxWheelTime)
            {
                Debug.Log("End game");
				SceneManager.LoadScene("Menu");
                // Game end
            }

            timeSinceWheel += Time.deltaTime;
        }
    }

    private IEnumerator HoleSpawnLoop()
    {
        // spawn first hole after 9 - 12 seconds from start
        yield return new WaitForSeconds(Random.Range(12f, 17f));

        while (true)
        {
            float waitTime = Random.Range(17f, 24f);
            SpawnObjects();

            // spawn hole every 9 to 12 seconds
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator SteeringActionLoop()
    {
        // Require steering after 3 - 9 seconds from start
        yield return new WaitForSeconds(Random.Range(2.5f, 5f));

        while (true)
        {
            wheel.requiresSteering = true;

            // require steering every 9 to 12 seconds
            float waitTime = Random.Range(9f, 12f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnObjects()
    {
        Vector3 randomPos = new Vector3(Random.Range(-6f, 6f), Random.Range(-3.3f, -4f), 0);

        // spawn hole
        Instantiate(hole, randomPos, Quaternion.identity);
    }
}