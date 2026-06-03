using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] float swingAmplitude = 30f; // Maximum angle of swing in degrees
    [SerializeField] float swingFrequency = 1f; // Speed of swinging
    [SerializeField] Vector3 swingAxis = Vector3.forward; // Axis around which to swing
    [SerializeField] float initialAngle;
    private Quaternion initialRotation;

    void Awake()
    {
        initialRotation = transform.rotation; //Quaternion.Euler(initialAngle, 0f, 0f);
    }

    void LateUpdate()
    {
        //need to offset the swing by the initial angle so that it starts at the correct position
        
        float swingAngle = initialAngle+ swingAmplitude * Mathf.Sin(Time.time * swingFrequency * 2f * Mathf.PI);
        
        Quaternion swingRotation = Quaternion.AngleAxis(swingAngle, swingAxis);
        transform.rotation = initialRotation * swingRotation;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
