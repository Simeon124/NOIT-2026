using UnityEngine;
using UnityEngine.InputSystem;

public class LockpickToolPosition : MonoBehaviour
{
    public bool DidItHit = false;
    [SerializeField] private float xOffset;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        
        // Convert screen space to world space
        mousePos.z = 5f; // distance from camera

        var cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 target = new Vector3(cursorPos.x, cursorPos.y, cursorPos.z);


        // Smoothly move toward the mouse
        transform.position = new Vector3(target.x + xOffset, target.y, transform.position.z);
        target.x -= 0.6f;
        // i < number of rays
        for (int i = 0; i < 2; i++)
        {
            Ray ray = new Ray(target, Vector3.back);
            DidItHit = Physics.Raycast(ray, out RaycastHit hit, 7f,LayerMask.GetMask("Pin"));
            if (DidItHit) hit.transform.position = new Vector3(hit.transform.position.x, transform.position.y+0.17f, hit.transform.position.z);
            Debug.DrawRay(target, Vector3.back, Color.red);
            target.x += 0.1f;
        }
        
    }
}
