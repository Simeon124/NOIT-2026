using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRandomizer : MonoBehaviour
{
    public Button[] buttons;
    public Transform[] positions;
    public ImageThing imageManager;
    // public ImageThing ImageThing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        RandomizePos();
   //   ImageThing.imagechange();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public void RandomizePos()
    {   int[] number = new int[positions.Length];
        //imageManager.ImageChange();
        for (int i = positions.Length - 1; i > 0; i--)
        { 
            number[i] = i; 
        }

        for (int i = positions.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = number[i];
            number[i] = number[rand];
            number[rand] = temp;
        }
        for (int i = 0; i < positions.Length; i++)
        {
            buttons[i].transform.position = positions[number[i]].position;
        }
     //   ImageThing.imagechange();
    }
    public void NumerChange()
    {
        StartCoroutine(CHANGE());
    }
    public IEnumerator CHANGE()
    {
        yield return new WaitForSeconds(1);
        RandomizePos();
        for (int i = 0;i < buttons.Length;i++)
        {
            buttons[i].interactable = true;
            buttons[i].animator.Play("Normal");
        }
    }
}

