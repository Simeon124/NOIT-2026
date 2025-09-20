using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomPosition : MonoBehaviour
{
    public Button button;
    public ButtonRandomizer randomizer;
    public GameObject Options;
    public LoopButton loop;
    public ButtonPick1 pick1;
    public bool InLoop;
    public int reps, clicked;
    public int repssave, score=0;
    public Vector3 positionTopLeft;
    public Vector3 positionBotRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Options.SetActive(false);
        repssave = reps;
        ButtonPosChange();
        randomizer.RandomizePos();
    }

    // Update is called once per frame
    void Update()
    {
        if (pick1.ready)
        {
            StopCoroutine(OptionsTime());
            pick1.ready = false;
            Options.SetActive(false);
            StartCoroutine(CooldownButton(2f));
            pick1.button3.interactable = true;
            pick1.button1.interactable = true;
            pick1.button2.interactable = true;
        }
    }
    public void NumCall()
    {
        //kogato butona e natisnat bez da e v loopa
        if (!InLoop)
        {
            StartCoroutine(PositionChange(2, reps));
            InLoop = true;
            score++;
        }
        //kogato butona e natisnat po vreme na loopa
        else { score++; StopAllCoroutines(); StartCoroutine(PositionChange(2, reps));}
    }
    public IEnumerator PositionChange(float n, int repeats)
    {
        //puska loopa s butona i proverqva dali e gotov
        repeats--;
        reps = repeats;
        if (repeats == 0) { 
            loop.score =(100*score) / repssave; 
            loop.starter(1);  InLoop = false; 
            score = 0; StopAllCoroutines();
            ShowOptions();
            repssave++; 
            reps = repssave;
        }
        ButtonPosChange();
        yield return new WaitForSeconds(n);
        if (repeats != 0) { StartCoroutine(PositionChange(n, repeats));  }
    }
    IEnumerator CooldownButton(float time)
    {
        //puska loopa s butona sled opredeleno vreme
        yield return new WaitForSeconds(time);
        ButtonPosChange();
        loop.starter(0);
    }

    void ButtonPosChange()
    {
        float randomX = Random.Range(positionTopLeft.x, positionBotRight.x);
        float randomY = Random.Range(positionTopLeft.y, positionBotRight.y);
        button.transform.position = new Vector3(randomX, randomY, 0);
    }
    public void AfterStarter(float WaitTime)
    {
        //unused v momenta
        //reps++;
        StartCoroutine(PositionChange(WaitTime, reps));
        InLoop = true;
    }
    public void ShowOptions()
    {
        if (loop.score >= 60)
        { StartCoroutine(OptionsTime()); 
        }
        else { pick1.loselife();StartCoroutine(CooldownButton(3f));  }
    }
    IEnumerator OptionsTime()
    {
        yield return new WaitForSeconds(1.5f);
        Options.SetActive(true);
        yield return new WaitForSeconds(5);
        Options.SetActive(false);
        randomizer.RandomizePos();
        StartCoroutine(CooldownButton(2f));
    }
}
