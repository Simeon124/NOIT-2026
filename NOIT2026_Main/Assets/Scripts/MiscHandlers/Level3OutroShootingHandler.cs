using UnityEngine;

public class Level3OutroShootingHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("Shot");
        }
    }
}
