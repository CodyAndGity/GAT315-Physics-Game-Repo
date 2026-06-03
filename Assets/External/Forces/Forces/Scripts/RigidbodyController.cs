using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{
	[SerializeField] float speed = 1;
	[SerializeField] float turnRate = 1;
	[SerializeField] float jumpHeight = 2;
	[SerializeField] ForceMode forceMode;
	[SerializeField] LayerMask layerMask = Physics.AllLayers;

	Rigidbody rb;
	Vector3 force;
	float rotation = 0.0f;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Vector3 direction = Vector3.zero;
		if (Keyboard.current.aKey.isPressed) direction.x = -1;
		if (Keyboard.current.dKey.isPressed) direction.x = +1;
		if (Keyboard.current.wKey.isPressed) direction.z = +1;
		if (Keyboard.current.sKey.isPressed) direction.z = -1;

        rotation = 0;
        if (Keyboard.current.qKey.isPressed) rotation = -1;
		if (Keyboard.current.eKey.isPressed) rotation = 1;

		force = direction * speed;

		if (Keyboard.current.spaceKey.wasPressedThisFrame)
		{
			rb.AddForce(Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight), ForceMode.Impulse);
		}
	}

	void FixedUpdate()
	{
		rb.AddRelativeForce(force, forceMode);
		rb.AddTorque(Vector3.up * rotation*turnRate, forceMode);
    }
}
