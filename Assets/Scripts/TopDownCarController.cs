using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;
    public float accelFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    //Local Variables
    float accelInput = 0;
    float steerInput = 0;

    public float rotaAngle = 0;

    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        //KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelInput > 0)
            return;

        //Limit so we cannot go faster than the max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelInput > 0)
            return;

        //Apply drag if there is no accelInput so the car stops when the player lets go of the accelerator
        if (accelInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;
        
        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelInput * accelFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limit the cars ability to turn when moving slowly
        float minTurningSpeed = (carRigidbody2D.velocity.magnitude / 2);
        minTurningSpeed = Mathf.Clamp01(minTurningSpeed);

        //Update the rotation angle based on input
        rotaAngle -= steerInput * turnFactor * minTurningSpeed;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotaAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steerInput = inputVector.x;
        accelInput = inputVector.y;
    }
}
