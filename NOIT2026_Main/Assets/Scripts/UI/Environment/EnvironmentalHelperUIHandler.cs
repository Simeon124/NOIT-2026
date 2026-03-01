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

    private void Update()
    {
        RaycastHit hit;
        
        bool canInteract = Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, rayLength,
            environmentalTriggerLayer);
        
        
        targetUIElement.SetActive(canInteract);

        if (canInteract)
        {
            targetUIElement.transform.position = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + xOffset, hit.transform.position.y + yOffset, hit.transform.position.z + zOffset));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayStartPos.position, rayStartPos.forward * rayLength);
    }
}