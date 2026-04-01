using UnityEngine;

public class L2_To_L3_TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject animationGameObject;
    [SerializeField] private GameObject terrain;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.ambientIntensity = 0;
            terrain.SetActive(false);
            animationGameObject.SetActive(true);
        }
    }
}
