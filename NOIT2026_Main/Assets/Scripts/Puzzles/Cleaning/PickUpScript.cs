using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private Camera cam;
    private Rigidbody pickedRb;
    private ConfigurableJoint joint;
    private float pickupDistance;
    private Vector3 localHit;

    [Header("Joint Settings")]
    public float spring = 50f;
    public float damper = 9f;
    public float maxForce = 10f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    void FixedUpdate()
    {
        if (joint != null)
        {
            // Keep the joint anchor at the mouse�s world position
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = pickupDistance;
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

            joint.connectedAnchor = worldPos;
        }
    }

    void TryPickObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.rigidbody != null)
            {
                pickedRb = hit.rigidbody;

                // Measure distance so it stays at correct depth
                pickupDistance = 3.8f;
                localHit = pickedRb.transform.InverseTransformPoint(hit.point);
                // Add a ConfigurableJoint
                joint = pickedRb.gameObject.AddComponent<ConfigurableJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = hit.point;

                // Allow translation with springy motion
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;

                // Allow free rotation around all axes
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;

                // Configure drives (spring/damper)
                JointDrive drive = new JointDrive
                {
                    positionSpring = spring,
                    positionDamper = damper,
                    maximumForce = maxForce
                };

                joint.xDrive = drive;
                joint.yDrive = drive;
                joint.zDrive = drive;
            }
        }
    }

    void DropObject()
    {
        if (joint != null)
        {
            Vector3 throwVelocity = pickedRb.linearVelocity;
            Vector3 throwAngularVelocity = pickedRb.angularVelocity;

            Destroy(joint);
            joint = null;

            // restore momentum
            pickedRb.linearVelocity = throwVelocity;
            pickedRb.angularVelocity = throwAngularVelocity;

            pickedRb = null;
        }
    }
}
