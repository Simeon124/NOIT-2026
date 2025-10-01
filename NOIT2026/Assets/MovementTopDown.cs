using UnityEngine;

public class MovementTopDown : MonoBehaviour
{
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    public float RotationSpeed = 2f;
    private float moveHorizontal;
    private float moveForward;
   // private Vector3 smoothmovement;
    private Vector3 targetVelocity;
    private Vector3 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        MovePlr();
        RotateWithMovement();
    }
    private void MovePlr()
    {
        movement = new Vector3(moveHorizontal, 0, moveForward).normalized;
        //movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        //smoothmovement = Vector3.SmoothDamp(smoothmovement, movement,ref targetVelocity, 2f);
        targetVelocity = movement * MoveSpeed;
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;
    }
    private void RotateWithMovement()
    {
        if (movement != Vector3.zero)
        {
            Quaternion Target = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion Rotation = Quaternion.RotateTowards(transform.rotation, Target, RotationSpeed * Time.deltaTime);
            transform.rotation = Rotation;
        }
    }
}
