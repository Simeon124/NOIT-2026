using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Sanity : MonoBehaviour
{
    [SerializeField] public float maxSanity;
    [SerializeField] public float currentSanity;
    [SerializeField] private float sanityDropMultiplier;
    [SerializeField] private float sanityRiseMultiplier;
    [SerializeField] private string insanityZoneTag;
    [SerializeField] private string safeZoneTag;
    [SerializeField] private ParticleSystem doorParticles;
    [SerializeField] AudioSource heartbeatAudio;
    [SerializeField] AudioSource criticalSanityAudio;
    private bool respawningStarted;

    public Volume insanityVolume;

    public bool isInInsanityZone = false;
    private bool particlesPlaying = false;

    public List<string> currentZones = new List<string>();

    private void Update()
    {
        if (respawningStarted == false && currentSanity <= 0)
        {
            StartCoroutine(Restart());
            respawningStarted = true;
        }
        if (currentZones.Contains(insanityZoneTag) && !currentZones.Contains("SafeZone"))
        {
            isInInsanityZone = true;
            if (!particlesPlaying && currentSanity <= 45)
            {
                heartbeatAudio.Play();
                if (doorParticles != null)
                {
                    doorParticles.gameObject.SetActive(true);
                    doorParticles.playbackSpeed = 0.1f;
                    doorParticles.Play();
                }
                particlesPlaying = true;
            }
        }
        else
        {
            heartbeatAudio.Stop();
            if (doorParticles != null)
            {
                doorParticles.gameObject.SetActive(false);
                doorParticles.playbackSpeed = 5f;
                doorParticles.Stop();
            }
            isInInsanityZone = false;
            particlesPlaying = false;
            StopAllCoroutines();
            StartCoroutine(SanityRegen());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentZones.Add(other.tag);
        if (other.gameObject.tag == insanityZoneTag)
        {
            isInInsanityZone = true;
            StopAllCoroutines();
            StartCoroutine(SanityDrop());
        }
        if (other.gameObject.tag == safeZoneTag)
        {
            isInInsanityZone = false;
            StopAllCoroutines();
            StartCoroutine(SanityRegen());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        currentZones.Remove(other.tag);

        if (other.gameObject.tag == safeZoneTag)
        {
            isInInsanityZone = true;
            StopAllCoroutines();
            StartCoroutine(SanityDrop());
        }

        if (other.gameObject.tag == insanityZoneTag)
        {
            isInInsanityZone = false;
            StopAllCoroutines();
            StartCoroutine(SanityRegen());
        }
    }

    public IEnumerator SanityDrop()
    {
        while (isInInsanityZone == true)
        {
            if (currentSanity > 0)
            {
                insanityVolume.weight += Time.deltaTime * 0.008f * sanityDropMultiplier;
                currentSanity -= Time.deltaTime * sanityDropMultiplier;
            }
            yield return null;
        }

    }

    public IEnumerator SanityRegen()
    {
        while (currentSanity <= 100 && isInInsanityZone == false)
        {
            insanityVolume.weight -= Time.deltaTime * 0.1f * sanityRiseMultiplier;

            currentSanity += Time.deltaTime * sanityRiseMultiplier;

            yield return null;
            
        }

        if (currentSanity > 100)
        {
            currentSanity = 100;
        }

    }

    public IEnumerator Restart()
    {
        heartbeatAudio.Stop();
        criticalSanityAudio.Play();
        yield return new WaitForSeconds(criticalSanityAudio.clip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
