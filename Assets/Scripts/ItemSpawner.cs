using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float spawnInterval;
    public float minX;
    public float maxX;
    public float beerGravity = 0.4f;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        int randoInt = Random.Range(0, 100);

        GameObject prefabToSpawn;

        if (randoInt < 20)
        {
            prefabToSpawn = itemPrefabs[2];
        }
        else if (randoInt > 19 && randoInt < 23)
        {
            prefabToSpawn = itemPrefabs[3];
        }
        else
        {
            prefabToSpawn = itemPrefabs[Random.Range(0, 2)];
        }
            float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        GameObject beer = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = beer.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = beerGravity;
        }
    }

    public void IncreaseDifficulty()
    {
        spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f);

        beerGravity = Mathf.Min(1.5f, beerGravity += 0.05f);
    }
}
