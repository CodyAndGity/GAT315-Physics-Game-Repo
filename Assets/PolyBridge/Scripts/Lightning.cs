using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

// Ensures the GameObject has a LineRenderer component
[RequireComponent(typeof(LineRenderer))]
public class Lightning : MonoBehaviour
{
    // Reference to the LineRenderer component used to draw the lightning effect
    [SerializeField] LineRenderer lineRenderer;

    // Transform components marking the start and end points of the lightning bolt
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    // Number of segments the lightning bolt will be divided into, determining its detail level
    [SerializeField, Range(1, 20)] int segments;

    // Maximum random offset applied per segment to create a jagged effect
    [SerializeField, Range(0, 0.5f)] float radius;

    // Array to store the generated lightning bolt points
    Vector3[] positions;

    // Called once per frame to update the lightning effect dynamically
    void Update()
    {
        // Ensure the array is initialized or resized if the segment count changes
        if (positions == null || positions.Length != segments + 1)
        {
            positions = new Vector3[segments + 1];
            lineRenderer.positionCount = positions.Length;
        }

        // Calculate the direction vector between start and end points
        Vector3 direction = end.position-start.position;

        // Determine the fixed distance between each segment along the lightning path
        float segmentLength = direction.magnitude/segments;

        // Normalize the direction vector so it has a length of 1 (unit vector)
        direction=direction.normalized;

        // Set the first and last points of the lightning bolt
        positions[0] =  start.position ;
        positions[segments] =  end.position ;

        // Generate intermediate points to create the jagged lightning shape
        for (int i = 1; i < segments; i++)
        {
            // Calculate the position along the straight path between start and end
            positions[i] = start.position +  direction * segmentLength * i;

            // Apply a small random offset in all directions to create the lightning effect
            positions[i] += Random.insideUnitSphere*radius;
        }

        // Update the LineRenderer to reflect the new lightning shape
        lineRenderer.SetPositions(positions);
    }
}