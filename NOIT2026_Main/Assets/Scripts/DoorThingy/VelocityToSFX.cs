using System.Collections;
using UnityEngine;

public class VelocityToSFX : MonoBehaviour
{
    private AudioSource kreek;
    [SerializeField] private float volumeMultiplier;
    public float audioDuration;
    public float speed;
    private Vector3 lastPos;
    bool readyToPlay = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kreek = gameObject.GetComponent<AudioSource>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        speed = ((currentPos - lastPos).magnitude / Time.deltaTime)*5;
        lastPos = currentPos;
        if (speed > 0.75f) 
        {
            kreek.volume = speed/10 - 0.15f * volumeMultiplier;
            if(kreek.volume > 0.93) 
            {
                kreek.volume = 0.93f;
            }
            if (speed > 4)
            {
                kreek.pitch = Mathf.Sqrt(speed) / 3.5f;
                if (kreek.pitch >= 1.1f) 
                {
                    kreek.pitch = 1.1f; 
                }
                else if (kreek.pitch <= 0.85f)
                {
                    kreek.pitch = 0.85f;
                }
            }
            if (readyToPlay)
            {
                readyToPlay = false;
                StartCoroutine(AudioPlayer());
            }
           
        }
    }
    IEnumerator AudioPlayer()
    {
        Debug.Log(speed);
        kreek.Play();
        yield return new WaitForSeconds(audioDuration); //lenght of audio clip
        readyToPlay = true;
    }
}
