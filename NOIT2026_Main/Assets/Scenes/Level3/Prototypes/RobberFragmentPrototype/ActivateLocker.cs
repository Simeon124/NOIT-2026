using TMPro;
using UnityEngine;

public class ActivateLocker : MonoBehaviour
{
    public KeyCode Activation;
    public TMP_Text text;
    public BoxCollider Bcol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bcol = GetComponent<BoxCollider>();
        Bcol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Activation)) {
            Bcol.enabled = true;
            text.enabled = false;
        }
        if (Input.GetKeyUp(Activation))
        {
            Bcol.enabled = false;
            text.enabled = true;
        }

    }
}
