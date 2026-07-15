using UnityEngine;

public class BeerSpawner : MonoBehaviour
{
    public GameObject beerPrefab;
    public float spawnInterval = 1.5f;

    public float minX = -8f;
    public float maxX = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnBeer), 1f, spawnInterval);
    }

    void SpawnBeer()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        Instantiate(beerPrefab, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
