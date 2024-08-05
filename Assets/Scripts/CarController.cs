using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    public float maxTorque = 200f;
    public float maxSteerAngle = 30f;
    public float brakeTorque = 300f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Steering
        float steer = maxSteerAngle * Input.GetAxis("Horizontal");
        frontLeftWheel.steerAngle = steer;
        frontRightWheel.steerAngle = steer;

        // Acceleration
        float torque = maxTorque * Input.GetAxis("Vertical");
        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;

        // Braking
        if (Input.GetKey(KeyCode.Space))
        {
            rearLeftWheel.brakeTorque = brakeTorque;
            rearRightWheel.brakeTorque = brakeTorque;
        }
        else
        {
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }

        UpdateWheelTransforms();
    }

    void UpdateWheelTransforms()
    {
        // Front Left Wheel
        UpdateWheelTransform(frontLeftWheel, frontLeftTransform);
        // Front Right Wheel
        UpdateWheelTransform(frontRightWheel, frontRightTransform);
        // Rear Left Wheel
        UpdateWheelTransform(rearLeftWheel, rearLeftTransform);
        // Rear Right Wheel
        UpdateWheelTransform(rearRightWheel, rearRightTransform);
    }

    void UpdateWheelTransform(WheelCollider wheelCol, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCol.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
