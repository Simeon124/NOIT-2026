using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Sanity : MonoBehaviour
{
    [SerializeField] public float maxSanity;
    [SerializeField] public float currentSanity;
    [SerializeField] private float sanityDropMultiplier;
    [SerializeField] private float sanityRiseMultiplier;
    [SerializeField] private string insanityZoneTag;
    [SerializeField] private string safeZoneTag;

    [SerializeField] Volume insanityVolume;

    [SerializeField] bool isInInsanityZone = false;

    [SerializeField] List<string> currentZones = new List<string>();

    private void Update()
    {
        if (currentZones.Contains(insanityZoneTag) && !currentZones.Contains("SafeZone"))
        {
            isInInsanityZone = true;
        }
        else
        {
            isInInsanityZone = false;
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
                insanityVolume.weight += Time.deltaTime * 0.01f * sanityDropMultiplier;
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
}
