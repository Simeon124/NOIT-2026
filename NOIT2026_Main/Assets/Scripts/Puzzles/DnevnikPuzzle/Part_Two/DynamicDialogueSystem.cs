using TMPro;
using UnityEngine;

public class DynamicDialogueSystem : MonoBehaviour
{
    [SerializeField] private string optionA_Trigger, optionB_Trigger, optionC_Trigger;
    [SerializeField] TextMeshProUGUI optionA_Text, optionB_Text, optionC_Text;
    Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        animator = GetComponent<Animator>();
    }

    public void ChangeOptionAText(string text)
    {
        optionA_Text.text = text;
    }
    
    public void ChangeOptionBText(string text)
    {
        optionB_Text.text = text;
    }
    
    public void ChangeOptionCText(string text)
    {
        optionC_Text.text = text;
    }

    public void ChooseOptionA()
    {
        animator.SetTrigger(optionA_Trigger);
    }

    public void ChooseOptionB()
    {
        animator.SetTrigger(optionB_Trigger);
    }
    
    public void ChooseOptionC()
    {
        animator.SetTrigger(optionC_Trigger);
    }
}