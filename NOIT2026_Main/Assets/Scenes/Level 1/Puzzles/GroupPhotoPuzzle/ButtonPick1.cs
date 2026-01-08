using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPick1 : MonoBehaviour
{
    public Button button1, button2, button3, button4;
    public TextMeshProUGUI[] textbox;
    public Animator anim;
    public ReloadScene reloader;
    public GameObject GameOver;
    public GameObject puzzle;
   // public bool answer;
    public bool ready = false;
    public int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //the button holding the wrong answer calls this method
    public void WrongAns()
    {
        loselife();
        button3.interactable = false;
        button4.interactable = false;
        button1.interactable = false;
        button2.interactable = false;
        ready = true;
        StartCoroutine(DisablePuzzle());
    }
    //the button holding the correct answer calls this method
    public void CorrectAns()
    {
        button3.interactable = false;
        button4.interactable = false;
        button1.interactable = false;
        button2.interactable = false;
        ready = true;
        StartCoroutine(DisablePuzzle());
    }
    public void loselife()
    {
        anim.SetBool("Loss", true);
        reloader.ReloadCurrentScene(9);
        if (lives <=0) { GameOver.SetActive(true); Time.timeScale = 0; }
        if (lives > 0) { lives--; }
    }
    IEnumerator DisablePuzzle()
    {
        Debug.Log("100ping");
        yield return new WaitForSeconds(0.5f);
        puzzle.SetActive(false);
    }
}
