using UnityEngine;

public class Bomb : ExplosionForce
{
    [SerializeField] GameObject explosionEffect;
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
