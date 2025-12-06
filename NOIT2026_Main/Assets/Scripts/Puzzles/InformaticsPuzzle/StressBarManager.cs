using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StressBarManager : MonoBehaviour
{
    public Slider StressBar;
    public float ReturnTime = 0;
    public float ReturnValue = 0;
    public float CooldownTime = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Fill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Fill() 
    {
        StressBar.value -= ReturnValue;
        yield return new WaitForSeconds(ReturnTime);
        StartCoroutine(Fill());
    }
    public void WrongAns()
    {
        StopAllCoroutines();
        StressBar.value += 0.35f;
        StartCoroutine(Cooldown());
    }
    public void CorrectAns()
    {
        StopAllCoroutines();
        StressBar.value += 0.15f;
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(CooldownTime);
        StartCoroutine(Fill());
    }
}
