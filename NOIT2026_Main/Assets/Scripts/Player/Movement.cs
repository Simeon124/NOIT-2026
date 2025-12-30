using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    //Temporary bools used in other scripts
    public bool isInHouse = false;

    public Sanity sanityScr;
    
    KeyboardDatabaseDTO keyProfile;

    [Header("Movement")] 
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    private float normalSpeed;
    bool sprinting;
    bool isPlaying = false; //Check for SFX
    [SerializeField] private float walkingAudoDilay;
    [SerializeField] private float runningAudoDilay;
    [SerializeField] AudioSource walkingSFXSource;
    [SerializeField] AudioSource runningSFXSource;
    [SerializeField] private AudioClip[] walkingSFXCollection;
    [SerializeField] private AudioClip[] runningSFXCollection;

    [SerializeField] private AudioClip[] indoorWalkingSFXCollection;
    [SerializeField] private AudioClip[] indoorRunningSFXCollection;

    [SerializeField] private float jumpForce;

    //[SerializeField] private Transform hips;
    Rigidbody rb;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] bool isGrounded;

    [Header("Stamina")] public float stamina;
    [SerializeField] float staminaResetMultiplier;
    float maxStamina;
    bool hasStamina;
    bool isNotMoving;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        keyProfile = JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        sanityScr = GetComponent<Sanity>();

        normalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        maxStamina = stamina;
        hasStamina = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Jump).Value) && isGrounded == true && hasStamina == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
        
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            isNotMoving = false;
        }
        else
        {
            isNotMoving = true;
        }

        HandleSFX();
        
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

        Vector3 movementDir = xAxis * gameObject.transform.right + yAxis * gameObject.transform.forward;
        rb.MovePosition(rb.position + movementDir * speed * Time.fixedDeltaTime);
    }

    private void HandleSFX()
    {
        if (!isPlaying)
        {
            if (sanityScr != null)
            {
                if (sanityScr.currentZones.Contains("InsanityZone") || sanityScr.currentZones.Contains("SafeZone"))
                {
                    if (!isNotMoving && !sprinting)
                    {
                        walkingSFXSource.clip =
                            indoorWalkingSFXCollection[Random.Range(0, indoorWalkingSFXCollection.Length)];
                        StartCoroutine(PlayAudioWithDelay(walkingSFXSource, walkingAudoDilay));
                    }

                    if (sprinting == true)
                    {
                        runningSFXSource.clip =
                            indoorRunningSFXCollection[Random.Range(0, indoorRunningSFXCollection.Length)];
                        StartCoroutine(PlayAudioWithDelay(runningSFXSource, runningAudoDilay));
                    }
                }
                else
                {
                    if (!isNotMoving && !sprinting)
                    {
                        walkingSFXSource.clip = walkingSFXCollection[Random.Range(0, walkingSFXCollection.Length)];
                        StartCoroutine(PlayAudioWithDelay(walkingSFXSource, walkingAudoDilay));
                    }

                    if (sprinting == true)
                    {
                        runningSFXSource.clip = runningSFXCollection[Random.Range(0, runningSFXCollection.Length)];
                        StartCoroutine(PlayAudioWithDelay(runningSFXSource, runningAudoDilay));
                    }
                }
            }

            if (isInHouse)
            {
                if (!isNotMoving && !sprinting)
                {
                    walkingSFXSource.clip =
                        indoorWalkingSFXCollection[Random.Range(0, indoorWalkingSFXCollection.Length)];
                    StartCoroutine(PlayAudioWithDelay(walkingSFXSource, walkingAudoDilay));
                }

                if (sprinting == true)
                {
                    runningSFXSource.clip =
                        indoorRunningSFXCollection[Random.Range(0, indoorRunningSFXCollection.Length)];
                    StartCoroutine(PlayAudioWithDelay(runningSFXSource, runningAudoDilay));
                }
            }
        }
    }

    public void Sprinting(float sprintSpeed)
    {
        var sprintKey = keyProfile.Actions.First(x => x.Key == Action.Sprint).Value;
        if (Input.GetKeyDown(sprintKey) && hasStamina)
        {
            sprinting = true;
        }
        else if (Input.GetKeyUp(sprintKey))
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
            speed += (Time.timeScale * 7) * Time.deltaTime;
            if (isNotMoving == true)
            {
                sprinting = false;
            }
        }
        else if (sprinting == false || hasStamina == false)
        {
            speed = normalSpeed;
        }
    }


    public IEnumerator PlayAudioWithDelay(AudioSource source, float delay = 0)
    {
        isPlaying = true;
        source.Play();
        if (delay == 0)
        {
            yield return new WaitForSeconds(source.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }

        isPlaying = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MainHouse" || other.gameObject.tag == "InsanityZone")
        {
            isInHouse = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MainHouse")
        {
            isInHouse = false;
        }
    }
}