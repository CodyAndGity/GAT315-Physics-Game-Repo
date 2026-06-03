using UnityEngine;

public class PointEffector : MonoBehaviour
{
	public enum Falloff
	{
		InverseLinear,
		InverseSquare,
		Curve
	}

	[SerializeField] float force = 1.0f;
	[SerializeField, Range(0.1f, 20.0f)] float outerRadius = 1.0f;
	[SerializeField, Range(0.0f, 20.0f)] float innerRadius = 0.0f;
	[SerializeField] AnimationCurve curve;
	[SerializeField] Falloff falloff;

	Collider[] colliders;

	private void OnValidate()
	{
		innerRadius = Mathf.Min(innerRadius, outerRadius);
	}

	private void FixedUpdate()
	{
		colliders = Physics.OverlapSphere(transform.position, outerRadius);
				
		foreach (var collider in colliders)
		{
			if (collider.TryGetComponent<Rigidbody>(out var rigidbody))
			{
				// get direction from position to collider position
				Vector3 direction = collider.transform.position - transform.position;
                // get direction magnitude (distance)
                float distance = direction.magnitude;
                if (distance <= float.Epsilon) continue;

				// t = distance / outerradius (clamp 0-1)
				float t = Mathf.Clamp01(distance-innerRadius / outerRadius);
                // strength = 0
				float strength = 0.0f;
                switch (falloff)
				{
					case Falloff.InverseLinear:
						strength = 1 - t;
						break;
					case Falloff.InverseSquare:
						strength = 1 - (t * t);
						break;
					case Falloff.Curve:
						strength = curve.Evaluate(t);
						break;
					default:
						break;
				}
				rigidbody.AddForce((direction / distance) * force * strength);				
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, outerRadius);
		Gizmos.color = Color.orange;
		Gizmos.DrawWireSphere(transform.position, innerRadius);

	}
}
