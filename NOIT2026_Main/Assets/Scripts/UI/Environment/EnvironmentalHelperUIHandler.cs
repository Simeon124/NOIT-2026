using System;
using System.Linq;
using System.Threading;
using UnityEngine;

public class EnvironmentalHelperUIHandler : MonoBehaviour
{
    [SerializeField] GameObject targetUIElement;
    [SerializeField] private Transform rayStartPos;
    [SerializeField] private int rayLength = 10;
    [SerializeField] LayerMask environmentalTriggerLayer;
    [SerializeField] private bool inRange;
    [SerializeField] private int yOffset;
    [SerializeField] private int xOffset;
    [SerializeField] private int zOffset;

    [SerializeField] private GameObject physicalTargetUIElement;
    private void Update()
    {
        RaycastHit hit;
        
        bool canInteract = Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, rayLength,
            environmentalTriggerLayer);

        if (physicalTargetUIElement != null)
        {
            physicalTargetUIElement.SetActive(canInteract);
        }
        else
        {
            targetUIElement.SetActive(canInteract);
        }

        if (canInteract)
        {
            
            
            float targetX = hit.transform.position.x + xOffset;
            float targetY = hit.transform.position.y + yOffset;
            float targetZ = hit.transform.position.z + zOffset;
            
            if (physicalTargetUIElement != null)
            {
                physicalTargetUIElement.transform.position = new Vector3(targetX, targetY, targetZ);
                physicalTargetUIElement.transform.LookAt(Camera.main.transform, Vector3.up);
            }
            else
            {
                targetUIElement.transform.position = Camera.main.WorldToScreenPoint(new Vector3(targetX, targetY, targetZ));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayStartPos.position, rayStartPos.forward * rayLength);
    }
}