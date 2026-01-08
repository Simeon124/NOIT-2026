using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroFixButton : MonoBehaviour
{
    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.gameObject.SetActive(false);
        StartCoroutine(IntroFix());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   IEnumerator IntroFix()
    {
        yield return new WaitForSeconds(53);
        button.gameObject.SetActive(true);
    }
}
