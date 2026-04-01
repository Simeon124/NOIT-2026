using System;
using UnityEngine;

public class AnomalyDetector : MonoBehaviour
{
    public bool hasSightOfAnomaly;
    [SerializeField] private Transform rayStartPos;
    [SerializeField] private int rayLength = 10;
    [SerializeField] LayerMask environmentalTriggerLayer;
    void Update()
    {
        var ray = new RaycastHit();

        if (Physics.Raycast(rayStartPos.transform.position, rayStartPos.forward, out ray, rayLength, environmentalTriggerLayer))
        {
            hasSightOfAnomaly = true;
        }
        else
        {
            hasSightOfAnomaly = false;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPos.transform.position, rayStartPos.forward * rayLength, Color.red);
    }
}
