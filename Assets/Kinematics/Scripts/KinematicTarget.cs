using UnityEngine;
using UnityEngine.InputSystem;

public class KinematicTarget : MonoBehaviour
{
	void Update()
	{
		if (Mouse.current.leftButton.isPressed)
		{
			Vector2 mousePosition = Mouse.current.position.value;
			Vector3 position = new Vector3(mousePosition.x, mousePosition.y, 10);
			transform.position = (Vector2)Camera.main.ScreenToWorldPoint(position);
		}
	}
}
