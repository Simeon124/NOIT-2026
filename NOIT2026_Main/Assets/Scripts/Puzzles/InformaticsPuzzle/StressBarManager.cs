using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StressBarManager : MonoBehaviour
{
    public Slider StressBar;
    public Image FillImage;
    public float ReturnTime = 0;
    public float ReturnValue = 0;
    public float CooldownTime = 2;

    [SerializeField] Sprite lowStressSprite;
    [SerializeField] Sprite mediumStressSprite;
    [SerializeField] Sprite highStressSprite;

    [SerializeField] private Image BarIcon;
    [SerializeField] private float lowStressValue;
    [SerializeField] private float mediumStressValue;
    [SerializeField] public float HighStressValue;

    //Color only works for green now
    [SerializeField] Color beginningColor;
    [SerializeField] Color StressColor;

    void Start()
    {
        StartCoroutine(Fill());
    }

    // Update is called once per frame
    void Update()
    {
        if (StressBar.value > lowStressValue && StressBar.value < mediumStressValue)
        {
            BarIcon.sprite = lowStressSprite;
        }
        else if (StressBar.value > mediumStressValue && StressBar.value < HighStressValue)
        {
            BarIcon.sprite = mediumStressSprite;
        }
        else if (StressBar.value > HighStressValue)
        {
            BarIcon.sprite = highStressSprite;
        }
    }

    public IEnumerator Fill()
    {
        if (FillImage.color.r >= beginningColor.r || FillImage.color.g <= beginningColor.r)
        {
            FillImage.color = new Color(FillImage.color.r - Time.deltaTime * 0.5f, FillImage.color.g, FillImage.color.b);
            FillImage.color = new Color(FillImage.color.r, FillImage.color.g + Time.deltaTime * 0.5f, FillImage.color.b);
        }
        

        StressBar.value -= ReturnValue;
        yield return new WaitForSeconds(ReturnTime);
        StartCoroutine(Fill());
    }

    public void WrongAns()
    {
        SetColor();
        StopAllCoroutines();
        StressBar.value += 0.35f;
        StartCoroutine(Cooldown());
    }

    public void CorrectAns()
    {
        SetColor();
        StopAllCoroutines();
        StressBar.value += 0.15f;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        StartCoroutine(Fill());
    }

    public void SetColor()
    {
        FillImage.color = new Color(FillImage.color.r + StressBar.value * 1.5f, FillImage.color.g - StressBar.value * 1.5f, FillImage.color.b);
    }
}