using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragForce : MonoBehaviour
{
	[SerializeField, Range(0.0f, 1.0f)] float linearDrag;
	[SerializeField, Range(0.0f, 1.0f)] float angularDrag;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector3 velocity = rb.linearVelocity;
		velocity.y = 0;
		rb.linearVelocity +=-velocity* linearDrag;
		rb.angularVelocity +=-rb.angularVelocity* angularDrag;
	}
}
