using UnityEngine;

public abstract class KinematicSegment : MonoBehaviour
{
	public Polar polar;
	public KinematicSegment parent { get; set; }

	public Vector2 start
	{
		get => transform.localPosition;
		set => transform.localPosition = value;
	}

	public Vector2 end
	{
		get => start + polar.ToVector2();
	}

	public float length
	{
		get => polar.length;
		set => polar.length = value;
	}

	public float angle
	{
		get => polar.angle;
		set => polar.angle = value;
	}

	public abstract void UpdateKinematics();
}
