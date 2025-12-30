using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class InterludeNote : MonoBehaviour
{
    GlobalIngameTimeHandler globalIngameTimeHandler;

    Material material;

    KeyboardDatabaseDTO keyProfile;

    [SerializeField] private GameObject noteUIElement;
    [SerializeField] private string text;
    TextMeshProUGUI noteUIText;

    [SerializeField] float maxGlowIntensity;
    [SerializeField] float timeMultiplier;
    float currentIntensity;

    bool inRange;

    void Start()
    {
        globalIngameTimeHandler = GameObject.FindAnyObjectByType<GlobalIngameTimeHandler>();

        keyProfile =
            JsonUtility.FromJson<KeyboardDatabaseDTO>(PlayerPrefs.GetString(GlobalConfig.keybindSavePropertyName));
        material = GetComponent<MeshRenderer>().material;
        

        StartCoroutine(AnimateIntensity());
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(keyProfile.Actions.First(x => x.Key == Action.Interact).Value))
        {
            noteUIText = noteUIElement.GetComponentInChildren<TextMeshProUGUI>();
            noteUIText.text = text;
            globalIngameTimeHandler.gameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            noteUIElement.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void SetEmissionIntensity(Color color, float intensity)
    {
        material.SetColor("_EmissionColor", color * intensity);
    }

    public IEnumerator AnimateIntensity()
    {
        while (true)
        {
            currentIntensity = Mathf.PingPong(Time.time * timeMultiplier, maxGlowIntensity);
            SetEmissionIntensity(material.color, currentIntensity);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}