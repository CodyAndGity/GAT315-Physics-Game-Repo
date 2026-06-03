using UnityEngine;

public class InverseKinematicSegment : KinematicSegment
{
	//private void Update()
	//{
	//	UpdateKinematics();
	//}

	public override void UpdateKinematics()
	{
        // update rotation and scale of segment
        //transform.localRotation = rotate around the z-axis using angle axis (angle is in radians)
        
        transform.localRotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        transform.localScale = Vector3.one * length;
    }


	public void Follow(Vector2 target)
	{
		// compute direction (target <- start) with segment length
		Vector2 direction = (target - start).normalized * length;
		// convert direction cartesian to polar
		polar = Polar.Vector2ToPolar(direction);
		// set start to target - direction
		start = target - direction;
	}
}
