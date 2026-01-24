using UnityEngine;

public class HasLineOfSight : MonoBehaviour
{
    public bool InLineOfSight;
    public Interactable[] inter;
    public GameObject interactedObject;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inter.Length; i++)
        {
            if (inter[i].LOSChecker)
            {
                InLineOfSight = inter[i].HasLineOfSight(this.transform.position, inter[i].transform.position);

                if (InLineOfSight)
                {
                    interactedObject = inter[i].gameObject;
                    break;
                }
            }
            else
            {
                InLineOfSight = false;
            }
        }
    }
}