using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class RespawnSetter : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetPlayerRespawnPoint(respawnPoint.position);
            print("Player entered respawn.");

        }
    }
}
