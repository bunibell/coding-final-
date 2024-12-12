using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject fallingObjectPrefab;
    public float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnFallingObjects());
    }

    IEnumerator SpawnFallingObjects()
    {
        while (true)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector2 spawnPosition = new Vector2(randomX, 5f);
            Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}