using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QnA/QuestionDatabase")]
public class QuestionsDB : ScriptableObject
{
    public List<Question> QuestionCollection;
}