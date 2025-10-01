using UnityEngine;

public class CleanTheMrusno : MonoBehaviour
{
    public GameObject mother;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mother.transform.childCount < 300) //kogato puzela e minat
        {
        Debug.Log("nigger");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "MRUSOTIQ")
        {
            Destroy(other.gameObject);
        }
    }
}
