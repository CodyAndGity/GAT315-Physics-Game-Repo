using UnityEngine;

public class Win : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHasWon();
            
        }
    }

   
}
