using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class InformaticsPuzzleManager : MonoBehaviour
{
    //This script manages all actions and handles all things connected to the Informatics Puzzle.
    //Here are all hard coded questions and they are picked on random
    
    [SerializeField] QuestionsDB questionDatabase;
    List<Question> questionCollection;
    List<Question> currentQuestions = new List<Question>();
    List<Question> compensatingQuestions = new List<Question>(); //Questions that the player guessed wrong and need to try again
    [SerializeField] private int questionQuantity = 10; //Numbers of question to take from the question collection
    private int currentQuestionIndex = 0;
    [SerializeField] TextMeshProUGUI questionTextArea;
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    private bool initializedCollection = false;

    [SerializeField] private int transitionScene;
    [SerializeField] private int score = 0;

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
        
        DisplayQuestion();
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

            if (currentQuestions[currentQuestionIndex].Answers[randomIndex].IsCorrect)
            {
                buttonAnswerComponent.IsCorrect = true;
            }
            
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
            score++;
            ChangeToNextQuestion();
        }
        else
        {
            compensatingQuestions.Add(currentQuestions[currentQuestionIndex]);
        }
    }
    
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}