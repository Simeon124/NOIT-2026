using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoopButton : MonoBehaviour
{
    public Button button;
    bool buttonstate;
    public float score;
    public RandomPosition PosScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.gameObject.SetActive(false);
        buttonstate = false;
      // StartCoroutine(Countdown(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //starter se callva kogato iskame otnovo da pusnem loopa s butona
    public void starter(int a)
    {
        StartCoroutine(Countdown(a));
    }

    IEnumerator Countdown(int seconds)
    {
        //skriva i pokazva butona
        buttonstate = !buttonstate;
        button.interactable = buttonstate;
        Debug.Log(buttonstate);
        yield return new WaitForSeconds(seconds);
        button.gameObject.SetActive(buttonstate);
        StopAllCoroutines();
       // PosScript.AfterStarter(2);
    }
}
