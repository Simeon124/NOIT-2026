using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResistPullingScript : MonoBehaviour
{
    [SerializeField] private int transitionSceneIndex;
    [SerializeField] private int repeatsToChangeSceneCount;
    [SerializeField] private int repeatsCount;
    Slider slider;
    public GameObject[] Arrows;
    public GameObject[] Hands;
    public MouseDirection mD;
    public int state; // 0 - durpa na tvoe lqvo  1 - durpa na tvoe dqsno
    int tempState,counter;
    public float BarRegenSpeed;
    public int ResistanceDampener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider  = GetComponent<Slider>();
        StartCoroutine(StateChange());
        Arrows[0].SetActive(false);
        Arrows[1].SetActive(false);
        
        Hands[0].SetActive(false);
        Hands[1].SetActive(false);
    }
    
    private void FixedUpdate()
    {
        slider.value += BarRegenSpeed/100;
        if (state == 0 && mD.stateX == "Right" && mD.stateY == "Down")
        {
                slider.value -= (mD.strengthX + mD.strengthY) / ResistanceDampener;
        }

        else if (state == 1 && mD.stateX == "Left" && mD.stateY == "Down")
        {
                slider.value -= (mD.strengthX + mD.strengthY) / ResistanceDampener;
        }
    }
    IEnumerator StateChange()
    {
        tempState = state;
        state = Mathf.RoundToInt(Random.Range(0, 2));
        counter++;
        if (counter >= 2 && state == 1) {
            state = 0;
            counter = 0;
        }
        else if (counter >= 2 && state == 0)
        {
            state = 1;
            counter = 0;
        }
        if (tempState != state) {
            counter = 0;
            Arrows[tempState].SetActive(false);
            Arrows[state].SetActive(true);
            
            Hands[tempState].SetActive(false);
            Hands[state].SetActive(true);
            
            repeatsCount++;
            if (repeatsCount >= repeatsToChangeSceneCount)
            {
                SceneManager.LoadScene(transitionSceneIndex);
            }
        }
        yield return new WaitForSeconds(0.5f);
        Arrows[state].SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(StateChange());
    }
}
