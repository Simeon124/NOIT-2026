using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class Interactable : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private bool isSceneChangeTrigger;
    [SerializeField] int transitionScene;
    [SerializeField] private DnevnikMehanikaSetup dnevnikMehanikaSetup;
    bool FadeInStarted;
    public bool LOSChecker;
    Color c;
    int counter = 0;
    public float range = 2.2f; //the distance between the player and the object needed to activate the script
    public LayerMask obst; //wall mask
    public Image image;
    void Start()
    {
        image.enabled = false;
        c = image.color;
        c.a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirTo = this.transform.position - Player.transform.position;
        float distanceSQR = dirTo.sqrMagnitude;
        image.enabled = false;
        //   image.color = c;
        LOSChecker = false;
        if (distanceSQR < range * range)
        {
            LOSChecker = true;
            if (isSceneChangeTrigger)
            {
                if (HasLineOfSight(Player.transform.position, this.transform.position) && Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(transitionScene);
                }
            }
            
            
            if (HasLineOfSight(Player.transform.position, this.transform.position))
            {
                if (!FadeInStarted) { StopAllCoroutines(); StartCoroutine(FadeIn()); }
                image.enabled = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(FadeOut());
               // image.color = c;
            }

        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }
    public bool HasLineOfSight(Vector3 pos, Vector3 dir)
    {
        Vector3 Dir = dir - pos;
        float distance = Dir.magnitude;
        Debug.DrawRay(pos, Dir);
        //Debug.DrawRay(pos, Player.forward * range);
        if (Physics.Raycast(pos, Dir, distance, obst))// || !Physics.Raycast(pos, Player.forward*range, range, this.gameObject.layer))
        {
            
            return false;
        }
     //   else if (Physics.Raycast(pos, Player.forward * range, range, this.gameObject.layer)) {return true;}
        return true;
        
    }
    IEnumerator FadeIn()
    {
        c = image.color;
        float initial = c.a;
        FadeInStarted = true;

        for (float t = 0; t < 0.4f; t += Time.deltaTime)
        {
            c = image.color;

            c.a = Mathf.Lerp(0, 0.9f, t * 2);
            image.color = c;
            yield return null;
        }
        counter = 0;
    }
    IEnumerator FadeOut()
    {
        float initial = c.a;
        FadeInStarted = false;
        for (float t = 0; t < 0.01; t += Time.deltaTime)
        {
            c = image.color;

            c.a = Mathf.Lerp(0.9f, 0f, t*100);
            image.color = c;
            yield return null;
        }


    }
}
