using System.Collections;
using UnityEngine;

public class GlowingMaterial : MonoBehaviour
{
    Material material;
    
    [SerializeField] float maxGlowIntensity;
    [SerializeField] float timeMultiplier;
    float currentIntensity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        StartCoroutine(AnimateIntensity());
    }
    
    void SetEmissionIntensity(Color color, float intensity)
    {
        material.SetColor("_EmissionColor", color * intensity);
    }

    public IEnumerator AnimateIntensity()
    {
        while (true)
        {
            currentIntensity = Mathf.PingPong(Time.time * timeMultiplier, maxGlowIntensity);
            SetEmissionIntensity(material.color, currentIntensity);
            yield return null;
        }
    }
}
