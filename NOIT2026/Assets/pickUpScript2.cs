using UnityEngine;

public class pickUpScript2 : MonoBehaviour
{
    public float spring = 1500f;      // try 1500 -> snappy but tweak
    public float damper = 50f;        // higher -> less wobble
    public float maxForce = 10000f;   // finite so collisions can stop the object
    public float linearLimit = 0.5f;  // how far it can stretch (meters)

    Camera cam;
    Rigidbody pickedRb;
    ConfigurableJoint joint;
    GameObject anchorGO;
    Rigidbody anchorRb;

    Vector3 localHitOnPicked;   // anchor point in pickedRb local space
    Vector3 prevAnchorPos;
    Vector3 anchorVelocity;
    public Transform CenterPos;
    Quaternion prevPickedRotation;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryPick();

        if (Input.GetMouseButtonUp(0))
            Drop();
    }

    void FixedUpdate()
    {
        
        if (anchorGO != null)
        {
            // keep the anchor at the mouse depth where we picked the object
            Vector3 mouse = Input.mousePosition;
            mouse.z = 3.6f;//Vector3.Lerp(pickedRb.transform.position, CenterPos.position, 2f).z;
            Vector3 worldPos = cam.ScreenToWorldPoint(mouse);

            // compute anchor velocity (for throwing)
            anchorVelocity = (worldPos - prevAnchorPos) / Time.fixedDeltaTime;
            prevAnchorPos = worldPos;

            // move the kinematic anchor
            anchorRb.MovePosition(worldPos);

            // keep track of the picked object's rotation for angular velocity estimation
            if (pickedRb != null)
                prevPickedRotation = pickedRb.rotation;
        }
    }

    void TryPick()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(r, out RaycastHit hit, 100f)) return;
        if (hit.rigidbody == null) return;
        if (hit.rigidbody.isKinematic) return; // only pick dynamic rigidbodies

        pickedRb = hit.rigidbody;

        // increase collision fidelity while dragging
        pickedRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        pickedRb.interpolation = RigidbodyInterpolation.Interpolate;

        // record depth & local hit point (so it doesn't snap to center)
        localHitOnPicked = pickedRb.transform.InverseTransformPoint(hit.point);

        // create a kinematic anchor at the hit point
        anchorGO = new GameObject("PickupAnchor");
        anchorGO.transform.position = hit.point;
        anchorRb = anchorGO.AddComponent<Rigidbody>();
        anchorRb.isKinematic = true;
        prevAnchorPos = anchorGO.transform.position;
        anchorVelocity = Vector3.zero;

        // add ConfigurableJoint to the picked object and connect it to the kinematic anchor
        joint = pickedRb.gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = anchorRb;

        // set anchor on the picked object to the local hit point (prevents snapping)
        joint.anchor = localHitOnPicked;
        joint.connectedAnchor = Vector3.zero; // anchor's local point

        // allow limited translation (driven by x/y/z drives)
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;

        // allow full free rotation so object can tumble
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        // small-ish limit so object stays near the anchor but not rigidly pinned
        SoftJointLimit sjl = new SoftJointLimit { limit = linearLimit };
        joint.linearLimit = sjl;

        // position drives (spring-like)
        JointDrive drive = new JointDrive
        {
            positionSpring = spring,
            positionDamper = damper,
            maximumForce = maxForce
        };
        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;

        // projection helps correct large errors
        joint.projectionMode = JointProjectionMode.PositionAndRotation;
        joint.enablePreprocessing = false;

        // init prevPickedRotation for angular velocity calc
        prevPickedRotation = pickedRb.rotation;
    }

    void Drop()
    {
        if (pickedRb == null) { CleanAnchor(); return; }

        // estimate linear velocity from anchor movement (gives you "throw")
        Vector3 throwVel = anchorVelocity;

        // estimate angular velocity from rotation delta (rough)
        Quaternion currentRot = pickedRb.rotation;
        Quaternion delta = currentRot * Quaternion.Inverse(prevPickedRotation);
        delta.ToAngleAxis(out float angleDeg, out Vector3 axis);
        Vector3 throwAngVel = Vector3.zero;
        if (!float.IsNaN(axis.x) && axis.sqrMagnitude > 0.0001f)
        {
            if (angleDeg > 180f) angleDeg -= 360f;
            throwAngVel = axis.normalized * (angleDeg * Mathf.Deg2Rad / Time.fixedDeltaTime);
        }

        // apply the velocities (classic Rigidbody API). If you're on DOTS/new physics, set linearVelocity/angularVelocity accordingly.
        pickedRb.linearVelocity = throwVel;
        pickedRb.angularVelocity = throwAngVel;

        // cleanup joint & anchor
        if (joint != null) Destroy(joint);
        CleanAnchor();

        // leave pickedRb as null so next pickup works
        pickedRb = null;
    }

    void CleanAnchor()
    {
        if (anchorGO != null) Destroy(anchorGO);
        anchorGO = null;
        anchorRb = null;
        anchorVelocity = Vector3.zero;
    }
}
