using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

public class InformaticsPuzzleManager : MonoBehaviour
{
    //This script manages all actions and handles all things connected to the Informatics Puzzle.
    //Here are all hard coded questions and they are picked on random

    [SerializeField] private SressTimeHandler sressTimeHandler;
    [SerializeField] private int secondsReward;
    [SerializeField] StressBarManager stressBarManager;
    [SerializeField] QuestionsDB questionDatabase;
    List<Question> questionCollection;
    List<Question> currentQuestions = new List<Question>();

    [SerializeField] private int questionQuantity = 10; //Numbers of question to take from the question collection
    private int currentQuestionIndex = 0;
    [SerializeField] TextMeshProUGUI questionTextArea;
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    private bool initializedCollection = false;

    [SerializeField] private int transitionScene;
    [SerializeField] private int score = 0;

    [Header("Animations")] [SerializeField]
    private float animationDuration = 2f;

    [SerializeField] Animator pageFlipAnimator;

    [Header("EarthquakeEffect")] [SerializeField]
    float effectDurationSpeed;

    [SerializeField] private int effectMultiplier;
    [SerializeField] float effectDuration;

    [SerializeField] private GameObject indicator;
    [SerializeField] private Sprite wrongIndicatorIcon;
    [SerializeField] private Sprite correctIndicatorIcon;

    bool effectFinished = false;

    bool isStressed = false;

    //Coroutine that are specific to stop
    List<Coroutine> coroutineList = new List<Coroutine>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        questionCollection = questionDatabase.QuestionCollection;

        for (int i = 0; i < questionQuantity; i++)
        {
            int randomIndex = Random.Range(0, questionCollection.Count);

            while (currentQuestions.Contains(questionCollection[randomIndex]))
            {
                randomIndex = Random.Range(0, questionCollection.Count);
            }

            currentQuestions.Add(questionCollection[randomIndex]);
        }

        Debug.Log(String.Join(' ', currentQuestions));

        DisplayQuestion();
    }

    private void Update()
    {
        if (stressBarManager.StressBar.value > stressBarManager.HighStressValue && isStressed == false)
        {
            effectFinished = false;
            foreach (var button in buttons)
            {
                var coroutine =
                    StartCoroutine(Earthquake(button.transform, effectMultiplier, effectDurationSpeed,
                        new Vector2(button.transform.position.x, button.transform.position.y)));
                coroutineList.Add(coroutine);
            }

            isStressed = true;
        }

        if (stressBarManager.StressBar.value < stressBarManager.HighStressValue)
        {
            isStressed = false;
            effectFinished = true;
        }
    }


    private void DisplayQuestion()
    {
        questionTextArea.text = currentQuestions[currentQuestionIndex].QuestionText;
        var buttonsTexts = buttons.Select(x => x.GetComponentInChildren<TextMeshProUGUI>());
        foreach (var buttonText in buttonsTexts)
        {
            buttonText.text = "";
        }

        AssignAnswerValues();
    }

    private void AssignAnswerValues()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            bool itIsNotDublicate = false;

            var button = buttons[i];

            var buttonAnswerComponent = button.GetComponent<ButtonAnswerModel>();
            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            int randomIndex = Random.Range(0, currentQuestions[currentQuestionIndex].Answers.Count);

            while (!itIsNotDublicate)
            {
                string[] buttonsTexts = buttons.Select(x => x.GetComponentInChildren<TextMeshProUGUI>().text).ToArray();

                if (buttonsTexts.Contains(currentQuestions[currentQuestionIndex].Answers[randomIndex].AnswerText))
                {
                    randomIndex = Random.Range(0, currentQuestions[currentQuestionIndex].Answers.Count);
                }
                else
                {
                    itIsNotDublicate = true;
                }
            }

            buttonAnswerComponent.IsCorrect = currentQuestions[currentQuestionIndex].Answers[randomIndex].IsCorrect;

            buttonText.text = currentQuestions[currentQuestionIndex].Answers[randomIndex].AnswerText;
        }
    }

    public void ChangeToNextQuestion()
    {
        if (score >= currentQuestions.Count)
        {
            SceneManager.LoadScene(transitionScene);
        }

        DisplayQuestion();
    }

    public void Answer(ButtonAnswerModel answer)
    {
        if (answer.IsCorrect)
        {
            StartCoroutine(Indicate(true));
            stressBarManager.CorrectAns();
            sressTimeHandler.AddSeconds(secondsReward);
            score++;
            currentQuestionIndex++;
            StartCoroutine(WaitForAnimationEnd());
            ChangeToNextQuestion();
        }
        else
        {
            StartCoroutine(Indicate(false));
            currentQuestions.Add(currentQuestions[currentQuestionIndex]);
            score++;
            currentQuestionIndex++;
            StartCoroutine(WaitForAnimationEnd());
            ChangeToNextQuestion();
            stressBarManager.WrongAns();
            //StartCoroutine(Timer(effectDuration));
            //StartCoroutine(Earthquake(Camera.main.transform, effectDuration, effectCameraMultiplier));
        }
    }

    public IEnumerator WaitForAnimationEnd()
    {
        pageFlipAnimator.SetTrigger("pageFlip");
        yield return new WaitForSeconds(animationDuration);
    }

    public IEnumerator Timer(float deadline)
    {
        float timer = 0f;
        while (timer < deadline)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        effectFinished = true;
    }

    public IEnumerator Earthquake(Transform transform, float effectMultiplier, float effectDurationSpeed,
        Vector2 originalPostition)
    {
        while (effectFinished == false)
        {
            var randomPosX = originalPostition.x + Random.Range(-1 * effectMultiplier, 1 * effectMultiplier);
            var randomPosY = originalPostition.y + Random.Range(-1 * effectMultiplier, 1 * effectMultiplier);
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(randomPosX, randomPosY), 0.2f);
            yield return new WaitForSeconds(effectDurationSpeed);
        }

        transform.position = new Vector3(originalPostition.x, originalPostition.y);
        effectFinished = false;
    }

    public IEnumerator Indicate(bool isCorrect)
    {
        var indicatorImage = indicator.GetComponent<Image>();
        if (isCorrect)
        {
            indicatorImage.sprite = correctIndicatorIcon;
        }
        else
        {
            indicatorImage.sprite = wrongIndicatorIcon;
        }

        indicator.SetActive(true);
        yield return new WaitForSeconds(1f);
        indicator.SetActive(false);
    }
}


public enum Difficulty
{
    Easy,
    Medium,
    Hard
}