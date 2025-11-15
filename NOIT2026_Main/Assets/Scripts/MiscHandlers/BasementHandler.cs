using UnityEngine;

public class BasementHandler : MonoBehaviour
{
    Sanity playerSanity;
    [SerializeField] GameObject basementGameObject;
    [SerializeField] GameObject terrain;
    [SerializeField] int sanityValueTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSanity = GameObject.FindWithTag("Player").GetComponent<Sanity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSanity.currentSanity < sanityValueTrigger)
        {
            basementGameObject.SetActive(true);
            terrain.SetActive(false);
        }
        else
        {
            basementGameObject.SetActive(false);
            terrain.SetActive(true);
        }
    }
}
