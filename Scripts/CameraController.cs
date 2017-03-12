using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject TrackedObject;
    private Rigidbody TrackedRigidBody;
    public float maxFollowDistance;
    public float minFollowDistance;
    private float followDistance;
    private float dtheta = 1;
    private Vector3 relativePosition;
    private Vector3 cameraPosition;
    private Vector3 lastCameraPosition;
    private Quaternion cameraHeading;
    private Quaternion lastCameraHeading;
    private float minPolarAngle = 15;
    private float pitch;
    private float yaw;
    public float anglingSensitivity;
    
    // Use this for initialization
    void Start () {
        // Initialize rigid body
        TrackedRigidBody = TrackedObject.GetComponent<Rigidbody>();
        // Initialize relative position
        relativePosition = this.transform.position - TrackedObject.transform.position;
        cameraPosition = new Vector3(1, 0, 0);
        lastCameraPosition = new Vector3(1,0,0);
        cameraPosition = TrackedRigidBody.position + followDistance * cameraPosition;
        cameraHeading = new Quaternion(1, 0, 0, 0);
        followDistance = maxFollowDistance;
    }

    void Update() {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {

        //float pitchIncrement = anglingSensitivity * Input.GetAxis("Vertical");
        //float yawIncrement = anglingSensitivity * Input.GetAxis("Horizontal");
        //pitch += pitchIncrement;
        //yaw += yawIncrement;
        //if (Mathf.Abs(pitchIncrement) < 90) {
        //    pitch = Mathf.Sign(pitch) * 90;
        //}
        //if (Mathf.Abs(yawIncrement) < 90) {
        //    yaw = Mathf.Sign(yaw) * 90;
        //}

        // Calculate where camera should be based on tracked object
        CalculateCameraPosition();

        // Compute total rotation
        //Vector3 localRotation = new Vector3(Mathf.Cos(pitch)* Mathf.Cos(yaw),
        //    Mathf.Cos(pitch) * Mathf.Sin(yaw),
        //    Mathf.Sin(pitch));
        //Vector3 totalRotation = cameraHeading * localRotation;
        
        // Interpolation
        //this.transform.position = Vector3.Lerp(lastCameraPosition, cameraPosition, 0.0001f);

        // Update camera position
        this.transform.position = cameraPosition;
        // Update camera heading
        this.transform.rotation = cameraHeading;
    }

    void CalculateCameraPosition() {
        Vector3 trackedVelocity = TrackedRigidBody.velocity;

        // Compute follow distance
        if (trackedVelocity.magnitude != 0) {
            followDistance = maxFollowDistance * (1.5f - trackedVelocity.magnitude / SugarController.lightSpeed);
            if (followDistance > maxFollowDistance)
                followDistance = maxFollowDistance;
            if (followDistance < minFollowDistance)
                followDistance = minFollowDistance;
        }
        else {
            followDistance = maxFollowDistance;
        }

        // Get new position if velocity is not zero
        Vector3 velocityUnitVector = trackedVelocity / trackedVelocity.magnitude;
        // Avoid singularity when facing directly upward
        if (Vector3.Angle(Vector3.up, velocityUnitVector) < minPolarAngle)
            velocityUnitVector[1] = Mathf.Sign(velocityUnitVector[1]) *
                Mathf.Sqrt(velocityUnitVector[2] * velocityUnitVector[2] + velocityUnitVector[0] * velocityUnitVector[0]) / Mathf.Tan(minPolarAngle);
        if (trackedVelocity.magnitude != 0) {
            cameraPosition = TrackedRigidBody.position - followDistance * velocityUnitVector;
            // Calculate heading of the camera
            Quaternion rotation = Quaternion.LookRotation(velocityUnitVector);
            cameraHeading = rotation;
        }
        else {
            cameraPosition = lastCameraPosition;
            cameraHeading = lastCameraHeading;
        }

        

        // Save camera position and heading
        lastCameraPosition = cameraPosition;
        lastCameraHeading = cameraHeading;
    }

}
