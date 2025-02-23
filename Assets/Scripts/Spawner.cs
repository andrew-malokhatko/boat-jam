 using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Spawner : Sounds
{
    public Hole hole;
    public Wheel wheel;

    private Hole holeInstance = null;

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
        if (holeInstance)
        {
            if (timeSinceHole > maxHoleTime)
            {
				SceneManager.LoadScene("Menu");
                // Game End
            }

            timeSinceHole += Time.deltaTime;
        }
        else
        {
            timeSinceHole = 0.0f;
        }

        if (wheel.requiresSteering)
        {
            if (timeSinceWheel > maxWheelTime)
            {
				SceneManager.LoadScene("Menu");
					PlaySound(sounds[2]);

                // Game end
            }

            timeSinceWheel += Time.deltaTime;
        }
        else
        {
            timeSinceWheel = 0.0f;
        }
    }

    private IEnumerator HoleSpawnLoop()
    {
        // spawn first hole after 9 - 12 seconds from start
        yield return new WaitForSeconds(Random.Range(10f, 17f));

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
			PlaySound(sounds[1]);

            // require steering every 9 to 12 seconds
            float waitTime = Random.Range(12f, 15f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnObjects()
    {
        Vector3 randomPos = new Vector3(Random.Range(-12f, 12f), Random.Range(-3.3f, -4f), 0);

        // spawn hole
        holeInstance = Instantiate(hole, randomPos, Quaternion.identity);
		PlaySound(sounds[0]);
    }
}