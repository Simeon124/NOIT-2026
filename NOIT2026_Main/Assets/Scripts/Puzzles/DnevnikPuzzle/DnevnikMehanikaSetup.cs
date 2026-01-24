using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DnevnikMehanikaSetup : MonoBehaviour
{
    public Image panel;
    public HasLineOfSight PlayerLOS; // LOS == Line Of Sight
    Movement playerMovement;

    [Header("NPC Animations")] [SerializeField]
    private string introTrigger;
    Animator interactedObjectAnimator;

    [SerializeField] private string outroTrigger;

    public GameObject text;
    int a;
    [SerializeField] private int puzzleDuration = 5; //Limiter for how many keys to press before ending.
    [SerializeField] private int counter = 0; //Counter for how many keys to press before ending.
    public TextMeshProUGUI keyToPress;
    public KeyCode[] keys; // butonite po vreme na puzela
    Color c;

    bool Playing = false, FadeInStarted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = PlayerLOS.gameObject.GetComponent<Movement>();
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerLOS.InLineOfSight) //puzzle start condition
        {
            if (!FadeInStarted)
            {
                playerMovement.enabled = false;
                interactedObjectAnimator = PlayerLOS.interactedObject.GetComponent<Animator>();
                interactedObjectAnimator.SetTrigger(introTrigger);
                
                StopAllCoroutines();
                StartCoroutine(FadeIn());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut());
            }
        }

        if (Input.GetKeyDown(keys[a]) && Playing)
        {
            var animator = text.GetComponentInParent(typeof(Animator)) as Animator;
            animator.SetTrigger("pressed");
            counter++;
            if (counter >= puzzleDuration)
            {
                playerMovement.enabled = true;
                interactedObjectAnimator.SetTrigger(outroTrigger);
                counter = 0;
                StopAllCoroutines();
                StartCoroutine(FadeOut());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ButtonSeq());
            }
            //StopCoroutine(ButtonSeq());
        }
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        float initial = c.a;
        FadeInStarted = true;
        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            c = panel.color;

            c.a = Mathf.Lerp(initial, 0.9f, t);
            panel.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        Playing = true;
        StartCoroutine(ButtonSeq());
    }

    IEnumerator ButtonSeq()
    {
        text.SetActive(true);
        a = Random.Range(0, 5);
        keyToPress.text = keys[a].ToString();
        yield return new WaitForSeconds(2);
        StartCoroutine(ButtonSeq());
    }

    IEnumerator FadeOut()
    {
        text.SetActive(false);
        float initial = c.a;
        FadeInStarted = false;
        Playing = false;
        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            c = panel.color;

            c.a = Mathf.Lerp(initial, 0f, t);
            panel.color = c;
            yield return null;
        }
    }
}