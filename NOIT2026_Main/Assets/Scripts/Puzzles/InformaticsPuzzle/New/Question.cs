using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public Difficulty Difficulty;
    public string QuestionText;
    public List<Answer> Answers;
}
