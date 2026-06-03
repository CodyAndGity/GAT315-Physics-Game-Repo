using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
