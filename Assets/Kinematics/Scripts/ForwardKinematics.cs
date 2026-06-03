using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematics : MonoBehaviour
{
    [SerializeField] ForwardKinematicSegment segmentPrefab;
    [SerializeField] int segmentCount = 5;
    [SerializeField, Range(0.5f, 3.0f)] float segmentLength = 1.0f;

    private List<KinematicSegment> segments = new();

    void Start()
    {
        KinematicSegment parent = null;

        // create segments
        for (int i = 0; i < segmentCount; i++)
        {
            // create segment
            var segment = Instantiate(segmentPrefab, transform);
            segment.parent = parent;
            // if parent not null set start to parent end else set to Vector2(0,0)
            segment.start = (segment.parent != null) ? parent.end : Vector2.zero;
            segment.length = segmentLength;

            // add segment
            segments.Add(segment);

            // set parent
            parent = segment;
        }
    }


    void Update()
    {
        // update segments
        foreach (var segment in segments)
        {
            // update length
            segment.length = segmentLength;

            // set segment start to segment parent end
            if (segment.parent != null) segment.start=segment.parent.end ;

            segment.UpdateKinematics();
        }
    }
}
