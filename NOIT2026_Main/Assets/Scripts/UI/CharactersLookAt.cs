using UnityEngine;

public class CharactersLookAt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform);
        
        Vector3 rot = transform.eulerAngles;
        rot.x = 0f;
        transform.eulerAngles = rot;
    }
}
