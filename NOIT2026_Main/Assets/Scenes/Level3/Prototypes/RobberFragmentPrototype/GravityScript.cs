using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public KeyCode Activation;
    public Transform origin;
    BoxCollider bc;
    public bool locker = false;

    [SerializeField] private AudioSource lockInSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(Activation)) locker = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!locker)
        {
            bc.enabled = true;
            if (transform.position.y >= origin.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            }
            else if (transform.position.y <= origin.position.y-0.03f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            }
        }
        else
        {
            bc.enabled = false;
            if (transform.position.y >= origin.position.y+1f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Locker" && transform.position.y > collision.transform.position.y+0.12f)
        {
            lockInSFX.Play();
            locker = true;
        }
    }
}
