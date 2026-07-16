using UnityEngine;

public class BeerParticleCleanup : MonoBehaviour
{
    private ParticleSystem ps;
    
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
