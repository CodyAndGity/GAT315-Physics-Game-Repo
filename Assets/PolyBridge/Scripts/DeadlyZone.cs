using UnityEngine;
[RequireComponent(typeof(BoxCollider))]

public class DeadlyZone : MonoBehaviour
{
    [SerializeField] public GameObject deathSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.TriggerPlayerRespawn();
            GameObject.Instantiate(deathSound);
            print("Player entered deadly zone, triggering respawn.");
        }
    }
}
