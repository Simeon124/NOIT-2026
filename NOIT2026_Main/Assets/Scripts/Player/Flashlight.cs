using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    bool isOn = false;
    [SerializeField] KeyCode flashlightKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(flashlightKey))
        {
            isOn = !isOn;
        }

        FlashlightControl();

    }

    void FlashlightControl()
    {
        if (isOn)
        {
            flashlight.SetActive(true);
        }
        else
        {
            flashlight.SetActive(false);
        }
    }
}
