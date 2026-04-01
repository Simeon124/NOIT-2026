using UnityEngine;

public class KeyPuzzleMechanic : MonoBehaviour
{
    [SerializeField] private Transform rayStartPos;
    [SerializeField] private int rayLength = 10;
    [SerializeField] LayerMask hiddenWallLayer;

    Collider wallCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new RaycastHit();

        if (Physics.Raycast(rayStartPos.transform.position, rayStartPos.forward, out ray, rayLength, hiddenWallLayer))
        {
            wallCollider.enabled = true;
        }
        else
        {
            wallCollider.enabled = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayStartPos.transform.position, rayStartPos.forward * rayLength, Color.green);
    }
}
