using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TopOfWater : RaycastPerception
{
    [SerializeField] float floatHeight = -.2f;
    void Start()
    {
        // if source not set, use game object transform
        source = source ?? transform;
    }

    void Update()
    {
        Vector3 position = this.gameObject.transform.position;
        position.y += floatHeight+1;
        ray.origin = position;
        ray.direction = new Vector3(0, -1, 0); ;

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);

        // check for raycast hit
        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, layerMask))
        {
            if (tagName == "" || raycastHit.collider.CompareTag(tagName))
            {
                // send event, pass hit game object
                perceivedGameObject?.Invoke();
                Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
            }
            //update height
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, raycastHit.point.y+ floatHeight, this.gameObject.transform.position.z);
            //update rotation with normal
            this.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
        }
    }
}
