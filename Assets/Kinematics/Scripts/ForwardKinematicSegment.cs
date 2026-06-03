using UnityEngine;

public class ForwardKinematicSegment : KinematicSegment
{
	[Header("Input")]
	[SerializeField, Range(-180, 180)] float inputAngle;

	[Header("Noise")]
	[SerializeField, Range(0, 90)] float noiseAngleRange = 90;
	[SerializeField, Range(0, 2)] float noiseRate = 0;
	[SerializeField] bool enableNoise = true;
	float noiseOffset;

	private void Start()
	{
		// set random noise offset
		noiseOffset = Random.value * 100;
	}

	//private void Update()
	//{
	//	UpdateKinematics();
	//}

	public override void UpdateKinematics()
	{
		// apply perlin noise to create motion
		float noiseAngle = 0;
		if (enableNoise)
		{
			noiseOffset += Time.deltaTime * noiseRate;
			float t = Mathf.PerlinNoise(noiseOffset, 0);
			noiseAngle = Mathf.Lerp(-noiseAngleRange, noiseAngleRange, t) * Mathf.Deg2Rad;
		}

		angle = (inputAngle * Mathf.Deg2Rad) + noiseAngle;
		// accumulate angle from parent (if not null) so each segment inherits the chain's rotation
		 if (parent != null) angle += parent.angle;

        // set rotation and scale of segment
        transform.localRotation = Quaternion.AngleAxis(angle*Mathf.Rad2Deg, Vector3.forward);
		transform.localScale = Vector3.one * length;
	}
}
