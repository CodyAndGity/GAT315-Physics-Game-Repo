using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour
{
    [SerializeField] InverseKinematicSegment segmentPrefab;
    [SerializeField] int segmentCount = 5;
    [SerializeField][Range(0.1f, 3.0f)] float segmentLength = 1;

    [SerializeField] Transform targetTransform;
    [SerializeField] Transform anchor;

    List<InverseKinematicSegment> segments = new();

    private void Start()
    {
        KinematicSegment parent = null;

        // create segments from root to tip
        // segments[0] = root, segments[last] = tip
        // root -> segment -> segment ->tip
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

            // set parent to current segment
            parent = segment;
        }
    }

    void Update()
    {
        // forward pass: tip chases target, each segment chases the one closer to the tip
        // iterates tip -> root (segments[last] -> segments[0])
        for (int i = segments.Count - 1; i >= 0; i--)
        {
            segments[i].length = segmentLength;

            // tip chases the target transform, all other segments chase the next segment's start
            // if index is tip (segments[last]), set target to target transform position, else set to next segment start
            Vector2 target = (i == segments.Count - 1) ? (Vector2)targetTransform.position : segments[i + 1].start;

            segments[i].Follow(target);
        }

        // anchor pass: pin the root to the anchor position, then propagate toward the tip
        // this corrects positions after the forward pass pulled the chain toward the target
        if (anchor != null)
        {
            // pin the root segment (segments[0]) to the anchor
            segments[0].start = anchor.position;

            // propagate from root toward tip (segments[1] -> segments[last])
            // each segment's start is placed at the previous (closer to root) segment's end
            for (int i = 1; i < segments.Count; i++)
            {
                // set start of index segment to previous index end
                segments[i].start = segments[i-1].end;
            }
        }

        // update kinematics
        foreach (var segment in segments)
        {
            segment.UpdateKinematics();
        }
    }

}
