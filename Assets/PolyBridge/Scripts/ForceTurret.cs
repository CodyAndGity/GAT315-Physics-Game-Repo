using System.Collections.Generic;
using UnityEngine;

public class ForceTurret : PhysicsRaycast
{
    //push and pull
    //Change color while active
    Renderer color;
    [SerializeField] float strength=1.0f;

    [Tooltip("Inactive,push,pull")]
    [SerializeField] List<Material> materials;
    [SerializeField] float timeNotActive = 1f;
    [SerializeField] float timePushing = 1f;
    [SerializeField] float timePulling = 1f;
    [SerializeField] LineRenderer line;
    float timer=0f;
    Dictionary<State, float> stateDurations = new Dictionary<State, float>();
    // Define possible states
    enum State
    {
        Idle,
        Pushing,
        Pulling
    }
    //Order of states
    [Tooltip("Sequence the turret will follow. First element is the first state in the cycle.")]
    [SerializeField] State[] stateOrder = new State[] { State.Pushing, State.Pulling };

    //Starting state
    [Tooltip("Index into State Order array indicating which state to start from.")]
    [SerializeField] int startAtIndex = 0;

    State currentState = State.Idle;


    // Called in editor when values change — keep startAtIndex valid
    void OnValidate()
    {
        if (stateOrder == null || stateOrder.Length == 0)
        {
            startAtIndex = 0;
            return;
        }

        if (startAtIndex < 0) startAtIndex = 0;
        if (startAtIndex >= stateOrder.Length) startAtIndex = stateOrder.Length - 1;
    }

    void Start()
    {
        color = gameObject.GetComponent<Renderer>();
        // Pick the starting state from the ordered list
        if (stateOrder != null && stateOrder.Length > 0)
            currentState = stateOrder[Mathf.Clamp(startAtIndex, 0, stateOrder.Length - 1)];
        else
            currentState = State.Idle;

        stateDurations[State.Idle] = timeNotActive;
        stateDurations[State.Pushing] = timePushing;
        stateDurations[State.Pulling] = timePulling;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        PerformRaycast(type);
        SetState();
        switch (currentState)
        {
            case State.Idle:
                //color.material.color = Color.gray;
                gameObject.GetComponent<Renderer>().material = materials[0];
                line.startColor = materials[0].color;
                line.endColor = materials[0].color;
                line.enabled = false;
                break;
            case State.Pushing:
                line.enabled = true;

                if (raycastHits != null)
                {
                    
                    foreach (var hit in raycastHits)
                    {
                        
                        if (hit.collider.gameObject == gameObject) continue;
                        Rigidbody rb;
                        if (hit.collider.gameObject.TryGetComponent(out rb))
                        {
                            rb.AddForceAtPosition(transform.forward*strength, hit.point, ForceMode.Acceleration); // Apply an impulse force at the hit point

                        }

                    }
                }
                //color.material.color = Color.blue;
                gameObject.GetComponent<Renderer>().material = materials[1];
                
                //line.startColor = materials[1].color;
                //line.endColor = materials[1].color;
                //line.startColor = Color.blue;
                //line.endColor = Color.blue;
                line.startColor = materials[1].color;
                line.endColor = materials[1].color;

                break;
            case State.Pulling:
                line.enabled = true;

                if (raycastHits != null)
                {

                    foreach (var hit in raycastHits)
                    {

                        if (hit.collider.gameObject == gameObject) continue;
                        Rigidbody rb;
                        if (hit.collider.gameObject.TryGetComponent(out rb))
                        {
                            rb.AddForceAtPosition(transform.forward * strength*-1, hit.point, ForceMode.Acceleration); // Apply an impulse force at the hit point

                        }

                    }
                }
                //color.material.color = Color.orange;
                gameObject.GetComponent<Renderer>().material = materials[2];
                //line.material = materials[2];
                line.startColor = materials[2].color;
                line.endColor = materials[2].color;
                //line.startColor = Color.blue;
                //line.endColor = Color.blue;
                break;
            
        };
    }

    public void SetState()
    {
        if (timer > stateDurations[currentState])
        {
            timer = 0f;
            // Move to the next state in the order
            if (stateOrder != null && stateOrder.Length > 0)
            {
                int currentIndex = System.Array.IndexOf(stateOrder, currentState);
                int nextIndex = (currentIndex + 1) % stateOrder.Length;
                currentState = stateOrder[nextIndex];
            }
            else
            {
                currentState = State.Idle; // Default to Idle if no order defined
            }
        }
    }
}
