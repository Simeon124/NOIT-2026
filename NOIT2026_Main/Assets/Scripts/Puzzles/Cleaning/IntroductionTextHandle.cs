using Unity.VisualScripting;
using UnityEngine;

public class IntroductionTextHandle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}


