using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    private float normalSpeed;
    bool sprinting;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform hips;
    Rigidbody rb;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] bool isGrounded;

    [Header("Stamina")]
    public float stamina;
    [SerializeField] float staminaResetMultiplier;
    float maxStamina;
    bool hasStamina;
    bool isNotMoving;

    [Header("SFX")]
    [SerializeField]
    AudioSource footstepsSFXAudioSource;

    [SerializeField]
    AudioSource jumpSFXAudioSource;

    [SerializeField]
    AudioSource landingSFXAudioSource;

    [SerializeField]
    AudioSource punchingSFXAudioSource;

    [SerializeField]
    AudioClip[] jumpsSFX;

    [SerializeField]
    AudioClip[] footstepsSFX;

    [SerializeField]
    AudioClip[] landingsSFX;

    [SerializeField]
    AudioClip[] punchesSFX;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        /*/
        anim = GetComponent<Animator>();
        /*/
        maxStamina = stamina;
        hasStamina = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && hasStamina == true)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            /*/
            anim.SetBool("isGrounded", false);
            /*/
            stamina -= 20;
        }
        Sprinting(sprintSpeed);



        //Stamina
        if (stamina < maxStamina && stamina > 0 && sprinting == false && isGrounded == true)
        {
            StartCoroutine(StaminaResetter());

        }
        else if (stamina <= 0)
        {
            stamina = 0;
            hasStamina = false;
            StartCoroutine(StaminaResetter(3));
        }
        if (stamina > 1)
        {
            hasStamina = true;
        }




    }
    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckPos.position, 0.5f, 7);

        /*/
            if (isGrounded == true)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }/*/

        var xAxis = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        var yAxis = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        if (xAxis != 0 || yAxis != 0)
        {
            /*/
            anim.SetBool("isWalking", true);
            /*/
            isNotMoving = false;
        }
        else
        {
            /*/
            anim.SetBool("isWalking", false);
            /*/
            isNotMoving = true;
        }



        //Stamina
        if (stamina < maxStamina && stamina > 0 && sprinting == false && isGrounded == true)
        {
            StartCoroutine(StaminaResetter());
        }
        else if (stamina <= 0)
        {
            stamina = 0;
            hasStamina = false;
            StartCoroutine(StaminaResetter(3));
        }
        if (stamina > 1)
        {
            hasStamina = true;
        }

        Vector3 movementDir = xAxis * hips.right + yAxis * hips.forward;
        rb.MovePosition(rb.position + movementDir * speed * Time.deltaTime);


    }

    public void Sprinting(float sprintSpeed)
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasStamina)
        {
            sprinting = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }

        if (isNotMoving == true)
        {
            sprinting = false;
        }

        if (sprinting == true && hasStamina == true && isNotMoving == false)
        {
            stamina -= Time.deltaTime * speed * 0.1f;
        }

        if (sprinting == true && speed <= sprintSpeed && hasStamina == true)
        {
            /*/
            anim.SetBool("isRunning", true);
            /*/
            speed += (Time.timeScale * 7) * Time.deltaTime;
            if (isNotMoving == true)
            {
                sprinting = false;
            }

        }
        else if (sprinting == false || hasStamina == false)
        {
            speed = normalSpeed;

            /*/
            anim.SetBool("isRunning", false);
            /*/
        }
    }


    public IEnumerator StaminaResetter(float delay = 2)
    {

        yield return new WaitForSeconds(delay);

        if (sprinting == false && isGrounded == true)
        {


            if (stamina <= maxStamina && sprinting == false && isGrounded == true)
            {
                stamina += Time.deltaTime * 1.3f;

            }

        }
        else
        {
            StopCoroutine(StaminaResetter());
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheckPos.position, 0.2f);
    }

    public void PlayFootstepsSound()
    {
        footstepsSFXAudioSource.clip = footstepsSFX[Random.Range(0, footstepsSFX.Length)];
        footstepsSFXAudioSource.PlayOneShot(footstepsSFXAudioSource.clip);
    }

    public void PlayJumpSound()
    {
        jumpSFXAudioSource.clip = jumpsSFX[Random.Range(0, jumpsSFX.Length)];
        jumpSFXAudioSource.PlayOneShot(jumpSFXAudioSource.clip);
    }

    public void PlayLandingSound()
    {
        landingSFXAudioSource.clip = landingsSFX[Random.Range(0, landingsSFX.Length)];
        landingSFXAudioSource.PlayOneShot(landingSFXAudioSource.clip);
    }

    public void PlayPunchSound()
    {
        punchingSFXAudioSource.clip = punchesSFX[Random.Range(0, punchesSFX.Length)];
        punchingSFXAudioSource.PlayOneShot(punchingSFXAudioSource.clip);
    }
}

