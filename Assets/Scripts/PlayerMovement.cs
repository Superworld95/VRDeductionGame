using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 60f;
    public Transform directionSource; // usually the headset or left hand/controller
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // LEFT STICK � Movement
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        Vector3 forward = directionSource.forward;
        Vector3 right = directionSource.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * input.y + right * input.x) * moveSpeed;
        Vector3 newVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        rb.linearVelocity = newVelocity;

        // RIGHT STICK � Rotation
        Vector2 rightInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float turn = rightInput.x * rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, turn, 0f);
    }
}

