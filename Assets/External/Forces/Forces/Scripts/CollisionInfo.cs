using UnityEngine;

public class CollisionInfo : MonoBehaviour
{
	Material material;
	Color color;

	Collision currentCollision = null;

	void Start()
	{
		material = GetComponent<Renderer>().material;
		color = material.color;
	}

	private void OnCollisionEnter(Collision collision)
	{
		material.color = Color.green;
		currentCollision = collision;
	}

	private void OnCollisionStay(Collision collision)
	{
		material.color = Color.red;
	}

	private void OnCollisionExit(Collision collision)
	{
		material.color = color;
		currentCollision = null;
	}

	// Draw visual representations of the contacts
	private void OnDrawGizmos()
	{
		if (currentCollision == null) return;

		Gizmos.color = Color.green;
		Gizmos.DrawRay(currentCollision.GetContact(0).point, currentCollision.GetContact(0).normal);
	}
}
