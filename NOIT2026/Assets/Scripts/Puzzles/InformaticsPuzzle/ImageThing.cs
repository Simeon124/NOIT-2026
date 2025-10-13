using UnityEngine;
using UnityEngine.UI;

public class ImageThing : MonoBehaviour
{
    //na poziciq 0 se slaga purviq greshen otgovor, a na poziciq 1 se slaga vtoriq. posle na poziciq 2
    //se slaga purvi greshen otgovor na vtori vupros, a na 3 - vtori greshen otgovor i tn. tn.
    public Image[] WrongAns;
    //sushtoto samo che na 0 se slaga vuprosa, a na 1 se veren otgovor
    public Image[] CorrectAns;
    public Image correct;
    public Image wrong;
    public Image wrong2;
    public Image question;
    int QuestionNumber = 0;

    public void imagechange()
    {
        question = CorrectAns[QuestionNumber];
        correct = CorrectAns[QuestionNumber + 1];
        wrong = WrongAns[QuestionNumber];
        wrong2 = WrongAns[QuestionNumber + 1];
        QuestionNumber += 2;

    }
}