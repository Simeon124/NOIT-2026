using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookControl : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    //[SerializeField] private Transform spine;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;
    [SerializeField] private float sensitivity;
    float bodyRotation = 0;
    float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bodyRotation = playerBody.localEulerAngles.y;
    }

    void LateUpdate()
    {
        var mouseSenProperty = PlayerPrefs.GetFloat(GlobalConfig.mouseSensitivitySavePropertyName);

        if (mouseSenProperty != 0)
        {
            sensitivity = mouseSenProperty;
        }
        
        var mousAxisX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity * 10;
        var mousAxisY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity * 10;

        xRotation -= mousAxisY;
        xRotation = Mathf.Clamp(xRotation, minYRotation, maxYRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        bodyRotation += mousAxisX;
        playerBody.localEulerAngles = new Vector3(0, bodyRotation, 0);

    }
}
