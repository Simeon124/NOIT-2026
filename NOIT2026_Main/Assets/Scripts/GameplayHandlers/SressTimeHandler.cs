using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SressTimeHandler : MonoBehaviour
{
    [SerializeField] int minutes;
    [SerializeField] int seconds;

    [Header("UI")] [SerializeField] bool UIMode = false;
    [SerializeField] TextMeshProUGUI minutesText;
    [SerializeField] TextMeshProUGUI secondsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Timer());
    }


    void Update()
    {
        if (UIMode)
        {
            minutesText.text = $"{minutes.ToString()}m";
            secondsText.text = $"{seconds.ToString()}s";
        }
    }

    public IEnumerator Timer()
    {
        while (minutes > 0 || seconds > 0)
        {
            yield return new WaitForSeconds(1);
            if (seconds > 0)
            {
                seconds--;
            }
            else if (minutes > 0)
            {
                seconds = 60;
                minutes--;
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}