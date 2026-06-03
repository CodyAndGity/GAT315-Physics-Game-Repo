using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] float spawnInterval = 1.0f;

    float timer;
    // Update is called once per frame

    private void OnValidate()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            GameObject.Instantiate(prefabToSpawn, spawnPoint.position + spawnOffset, Quaternion.identity);
            timer = 0.0f;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(spawnPoint.position + spawnOffset, 0.1f);
    }
}
