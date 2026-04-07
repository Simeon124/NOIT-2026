using UnityEngine;

public class L2_To_L3_TriggerScript : MonoBehaviour
{
    [SerializeField] private Light sunLight;
    [SerializeField] private GameObject animationGameObject;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject playerWalkingSFX;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.ambientIntensity = 0;
            terrain.SetActive(false);
            animationGameObject.SetActive(true);
            playerWalkingSFX.SetActive(false);
            sunLight.intensity = 0;
        }
    }
}
