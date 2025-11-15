using UnityEngine;

public class MovementNormal : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    public Transform cameraTransform;
    [Header("Movement")]
    [SerializeField] private float speed;
    private float normalSpeed;
    bool isNotMoving;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {




    }
    private void FixedUpdate()
    {

        var xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var yAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        RotateCamera();
        if (Input.GetAxis("Horizontal") <1 || Input.GetAxis("Vertical") <1) { rb.linearVelocity = Vector3.zero; }
        if (xAxis != 0 || yAxis != 0)
        {
            isNotMoving = false;
        }
        else
        {
            isNotMoving = true;
        }

        Vector3 movementDir = xAxis * gameObject.transform.right + yAxis * gameObject.transform.forward;
        rb.MovePosition(rb.position + movementDir * speed * Time.fixedDeltaTime);


    }
    void RotateCamera()
    {
        float horizontalRotation =Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -40f, 40f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
