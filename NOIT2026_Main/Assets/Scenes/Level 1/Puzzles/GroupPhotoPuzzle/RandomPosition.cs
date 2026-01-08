using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomPosition : MonoBehaviour
{
    [SerializeField] private int cutsceneIndex;
    public int WinCounter;
    [SerializeField] private Canvas canvas;
    public Button button;
    public string[] replyOptions;
    public Animator animator;
    int level = 1;
    int replyCounter = 4;
    public int[] cutsceneLengths;
    float WaitTime = 2f;
    bool cutscene;
    Color mainColor;
    Color color;
    public ButtonRandomizer randomizer;
    public GameObject Options;
    public LoopButton loop;
    public GameObject Puzzle;
    public ButtonPick1 pick1;
    public bool InLoop;
    public int reps, clicked;
    public int repssave, score=0;
    public Vector3 positionTopLeft;
    public Vector3 positionBotRight;
    float CurrentTime = 0;
    bool StartColorChange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color = button.image.color;
        mainColor = button.image.color;
        button.gameObject.SetActive(false);
        StartCoroutine(IntroFix(cutsceneLengths[level-1]));
        Cursor.lockState = CursorLockMode.None;
        for (int i = replyCounter - 4; i < replyCounter; i++)
        {
            pick1.textbox[i].text = replyOptions[i];
        }
        replyCounter += 4;

        var CanvasRect = canvas.GetComponent<RectTransform>();
        
        //positionTopLeft = new Vector3(0, CanvasRect.rect.height, 0);
        //positionBotRight = new Vector3(CanvasRect.rect.width, 0, 0);
        
        Options.SetActive(false);
        repssave = reps;
        ButtonPosChange();
        randomizer.RandomizePos();
    }

    // Update is called once per frame
    void Update()
    {
        //kogato izberesh nqkoq dialogova opciq
        if (pick1.ready)
        {
            WinCounter++;
            if (WinCounter == 3 && pick1.lives == 3)
            {
                //Transition to cutscene
                SceneManager.LoadScene(cutsceneIndex);
            }
            
            //StopCoroutine(OptionsTime());
            StopAllCoroutines();
            pick1.ready = false;
            Options.SetActive(false);
            StartCoroutine(CooldownButton(2f));
            pick1.button3.interactable = true;
            pick1.button4.interactable = true;
            pick1.button1.interactable = true;
            pick1.button2.interactable = true;

        }
        if (StartColorChange)
        {
            CurrentTime += Time.deltaTime;
                float t = Mathf.Clamp01(CurrentTime / 2);

                button.image.color = Color.Lerp(Color.green, Color.red, t);
        }
    }
    public void NumCall()
    {
        //kogato butona e natisnat bez da e v loopa
        if (!InLoop)
        {
            StartCoroutine(PositionChange(WaitTime, reps));
            InLoop = true;
            score++;
        }
        //kogato butona e natisnat po vreme na loopa
        else { score++; StopAllCoroutines(); StartCoroutine(PositionChange(WaitTime, reps));}
    }
    public IEnumerator PositionChange(float n, int repeats)
    {
        //puska loopa s butona i proverqva dali e gotov
        CurrentTime = 0;
        button.image.color = mainColor;
        color = mainColor;
        StartColorChange = true;
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
        StartColorChange = false;
        if (repeats != 0) { StartCoroutine(PositionChange(n, repeats));  }
    }
    IEnumerator CooldownButton(float time)   //MNOGO VAJNO TOVA TRQA DA MU SE DOBAVI CUTSCENE TRIGGER
    {
        //puska loopa s butona sled opredeleno vreme
        CurrentTime = 0;
        StartColorChange = false;
        button.image.color = mainColor;
        color = mainColor;
        yield return new WaitForSeconds(time);
        level++;
        animator.SetInteger("Level",level);
        StartCoroutine(IntroFix(cutsceneLengths[level-1]));
        Puzzle.SetActive(false);
        //smenq texta v butonite. vseki chetvurti element ot masiva v pick1 e verniq otgovor
        int j = 0;
        for (int i = replyCounter-4; i < replyCounter; i++) {
            pick1.textbox[j].text = replyOptions[i];
            j++;
        }
        if (replyCounter < 12)
        {
        replyCounter += 4;
        }
        
        ButtonPosChange();
       // loop.starter(0);
    }

    void ButtonPosChange()
    {
        float randomX = Random.Range(positionTopLeft.x, positionBotRight.x);
        float randomY = Random.Range(positionTopLeft.y, positionBotRight.y);
        button.transform.position = new Vector3(randomX, randomY, canvas.transform.localScale.z);
        button.gameObject.SetActive(true);
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
        button.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Options.SetActive(true);
        yield return new WaitForSeconds(5);
        pick1.loselife();
        Options.SetActive(false);
        if (cutscene)
        {
            yield return new WaitForSeconds(5); //tova se promenq v zavisimost kolko dulug e cutscenea sled opciite
            randomizer.RandomizePos();
            StartCoroutine(CooldownButton(2f));
        }
        else 
        {
        randomizer.RandomizePos();
        StartCoroutine(CooldownButton(2f));
        }
        
    }
    IEnumerator IntroFix(int CutsceneLength)
    {
        yield return new WaitForSeconds(2);
        Puzzle.SetActive(true);
        button.gameObject.SetActive(false);
        yield return new WaitForSeconds(CutsceneLength-2);
        loop.starter(1);
    }
}
