using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beer"))
        {
            GameManager.Instance.BeerCatch(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("GoldenBeer"))
        {
            GameManager.Instance.GoldenBeerStart(collision.gameObject);
            return;
        }
        
        if (collision.gameObject.CompareTag("Water"))
        {
            GameManager.Instance.WaterCatch(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("GoldenRecord"))
        {
            GameManager.Instance.GoldenRecordCatch(collision.gameObject);
            return;
        }
    }
}
