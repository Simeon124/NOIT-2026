using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
    [SerializeField] private float maxSanity;
    [SerializeField] private float currentSanity;
    [SerializeField] private float sanityDropMultiplier;
    [SerializeField] private float sanityRiseMultiplier;
    [SerializeField] private string insanityZoneTag;

    bool isInInsanityZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == insanityZoneTag)
        {
            isInInsanityZone = true;
            StopAllCoroutines();
            StartCoroutine(SanityDrop());
        }
    }
    private void OnTriggerExit(Collider other)
    {
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
                currentSanity -= Time.deltaTime * sanityDropMultiplier;
            }
            yield return null;
        }

    }

    public IEnumerator SanityRegen()
    {
        while (currentSanity <= 100)
        {

            currentSanity += Time.deltaTime * sanityRiseMultiplier;

            yield return null;
        }

        if (currentSanity > 100)
        {
            currentSanity = 100;
        }

    }
}
