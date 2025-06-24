using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookControl : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform spine;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;
    [SerializeField] private int sensitivity;
    public bool isInMenu;
    float bodyRotation = 0;
    float xRotation = 0;
    // Start is called before the first frame update
    // Update is called once per frame
    void LateUpdate()
    {
        if (isInMenu == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        var mousAxisX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity * 10;
        var mousAxisY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity * 10;




        xRotation -= mousAxisY;
        xRotation = Mathf.Clamp(xRotation, minYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        spine.localRotation = Quaternion.Euler(xRotation, 0, 0);

        bodyRotation += mousAxisX;
        playerBody.localEulerAngles = new Vector3(0, bodyRotation, 0);

    }
}
