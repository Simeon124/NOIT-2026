using UnityEngine;
using UnityEngine.UI;

public class ButtonPick1 : MonoBehaviour
{
    public Button button1, button2, button3;
    public GameObject GameOver;
   // public bool answer;
    public bool ready = false;
    int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WrongAns()
    {
        loselife();
        button3.interactable = false;
        button1.interactable = false;
        button2.interactable = false;
        ready = true;
    }
    public void CorrectAns()
    {
        button3.interactable = false;
        button1.interactable = false;
        button2.interactable = false;
        ready = true;
    }
    public void loselife()
    {
        if (lives <=0) { GameOver.SetActive(true); Time.timeScale = 0; }
        if (lives > 0) { lives--; }
    }
}
