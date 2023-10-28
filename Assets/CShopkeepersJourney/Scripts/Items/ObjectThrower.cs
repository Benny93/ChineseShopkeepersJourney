using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public GameObject objectToThrow;
    public float throwForce = 10f;
    public int arcSteps = 30;

    private Vector3 initialPosition;
    private Vector3 initialVelocity;
    private float gravity;

    private void Start()
    {
        gravity = Mathf.Abs(Physics.gravity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;        

        // Draw a forward-facing line to indicate the throwing direction
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);

        if (objectToThrow == null)
            return;       

        // Calculate initial velocity based on throwForce and angle
        initialVelocity = transform.forward * throwForce;

        // Calculate the time of flight
        float timeOfFlight = (2f * initialVelocity.y) / gravity;

        // Calculate the time step
        float timeStep = timeOfFlight / arcSteps;

        // Store the initial position
        initialPosition = transform.position;

        for (int i = 1; i <= arcSteps; i++)
        {
            // Calculate the position at each time step
            float t = i * timeStep;
            Vector3 nextPosition = CalculatePosition(t);
            //Gizmos.DrawLine(initialPosition, nextPosition);
            Gizmos.DrawSphere(initialPosition, 0.1f);
            initialPosition = nextPosition;
        }
    }

    private Vector3 CalculatePosition(float time)
    {
        float x = initialPosition.x + initialVelocity.x * time;
        float y = initialPosition.y + (initialVelocity.y - 0.5f * gravity * time * time);
        float z = initialPosition.z + initialVelocity.z * time;

        return new Vector3(x, y, z);
    }

    public void ThrowObject()
    {
        GameObject thrownObject = Instantiate(objectToThrow, transform.position, Quaternion.identity);
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = initialVelocity;
        }
    }

}
