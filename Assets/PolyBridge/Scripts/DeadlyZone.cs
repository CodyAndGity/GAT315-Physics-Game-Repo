using UnityEngine;
[RequireComponent(typeof(BoxCollider))]

public class DeadlyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.TriggerPlayerRespawn();
            print("Player entered deadly zone, triggering respawn.");
        }
    }
}
