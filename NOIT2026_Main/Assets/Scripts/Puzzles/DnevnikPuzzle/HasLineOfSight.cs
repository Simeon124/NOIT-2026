using UnityEngine;

public class HasLineOfSight : MonoBehaviour
{
    public bool InLineOfSight;
    public Interactable[] inter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inter.Length; i++) {
            if (inter[i].LOSChecker) 
            {
                InLineOfSight = inter[i].HasLineOfSight(this.transform.position, inter[i].transform.position);

                if (InLineOfSight) 
                { 
                    break; 
                }
            }
            else { InLineOfSight = false; }
            
        
        }
    }
}
