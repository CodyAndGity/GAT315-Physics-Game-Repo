using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionForce : MonoBehaviour
{
	[SerializeField] public float force = 1.0f;
	[SerializeField, Range(0.1f, 20.0f)] public float radius = 1.0f;
	[SerializeField, Range(0.0f, 20.0f)] public float upwardsModifier = 0.0f;

	void Update()
	{
		if (Keyboard.current.bKey.wasPressedThisFrame)
		{
			Explode();
		}
	}

	public void Explode()
	{
		// overlap sphere, apply explosion force to all rigidbodies
		var colliders=Physics.OverlapSphere(transform.position, radius);
		foreach (var collider in colliders)
		{
			var rb = collider.attachedRigidbody;
			if (rb != null)
			{
				rb.AddExplosionForce(force, transform.position, radius, upwardsModifier, ForceMode.VelocityChange);
			}
        }
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
