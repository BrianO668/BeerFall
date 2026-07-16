using UnityEngine;

public class BeerSpawner : MonoBehaviour
{
    public GameObject[] beerPrefabs;
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
            SpawnBeer();
            timer = 0f;
        }
    }

    void SpawnBeer()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        GameObject beer = Instantiate(beerPrefabs[Random.Range(0, beerPrefabs.Length)], spawnPosition, Quaternion.identity);

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
