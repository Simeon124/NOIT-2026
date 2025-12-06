using UnityEngine;

public class DynamicDialogueSystem : MonoBehaviour
{
    [SerializeField] private string optionA_Trigger, optionB_Trigger, optionC_Trigger;
    Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        animator = GetComponent<Animator>();
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