using System;
using System.Linq;
using System.Threading;
using UnityEngine;

public class EnvironmentalHelperUIHandler : MonoBehaviour
{
    [SerializeField] GameObject targetUIElement;
    [SerializeField] private Transform rayStartPos;
    [SerializeField] private int rayLength = 10;
    [SerializeField] int environmentalTriggerLayer;
    [SerializeField] private bool inRange;
    [SerializeField] private int yOffset;

    private void Update()
    {
        RaycastHit hit;
        
        bool canInteract = Physics.Raycast(rayStartPos.position, rayStartPos.forward, out hit, rayLength,
            environmentalTriggerLayer);

        if (canInteract)
        {
            Debug.Log("Interacting");
        }
        else
        {
            Debug.Log("Not interacting");
        }
        
        targetUIElement.SetActive(canInteract);

        if (canInteract)
        {
            targetUIElement.transform.position = Camera.main.WorldToScreenPoint( new Vector2(hit.transform.position.x, hit.transform.position.y + yOffset));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayStartPos.position, rayStartPos.forward);
    }
}