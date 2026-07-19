using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Beer") || collision.gameObject.CompareTag("GoldenBeer"))
        {
            GameManager.Instance.BeerMiss(collision.gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            GameManager.Instance.WaterMiss(collision.gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("GoldenRecord"))
        {
            GameManager.Instance.GoldenRecordMiss(collision.gameObject);
            return;
        }
    }
}
