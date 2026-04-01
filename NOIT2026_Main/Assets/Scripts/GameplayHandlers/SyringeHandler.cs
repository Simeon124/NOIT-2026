using System;
using System.Collections.Generic;
using UnityEngine;

public class SyringeHandler : MonoBehaviour
{
    private Sanity playerSanity;
    private List<Anomaly> anomalies = new List<Anomaly>();

    private void Start()
    {
        playerSanity = FindFirstObjectByType<Sanity>();
    }

    public void ConsumeSyringe()
    {
        if (anomalies.Count > 0)
        {
            foreach (var anomaly in anomalies)
            {
                anomaly.ResetAnomaly();
            }
        }
        
        playerSanity.currentSanity = playerSanity.maxSanity;
        playerSanity.insanityVolume.weight = 0;
    }
}
