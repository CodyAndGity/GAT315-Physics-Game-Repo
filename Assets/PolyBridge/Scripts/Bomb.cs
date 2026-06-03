using UnityEngine;

public class Bomb : ExplosionForce
{
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject);
    }
}
