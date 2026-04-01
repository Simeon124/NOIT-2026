using UnityEngine;

public class Anomaly : MonoBehaviour
{
    [Header("Anomaly General Settings")] [SerializeField]
    private GameObject anomalyDefaultState;

    [SerializeField] private GameObject anomalyTriggeredState;
    [SerializeField] private AudioSource anomalyTriggerSfx;
    AnomalyDetector anomalyDetector;

    [Header("Randomized Time Settings")] [SerializeField]
    private float minTime;

    [SerializeField] private float maxTime;

    [Header("Debug Settings")] [SerializeField]
    private float timeToTrigger = 0;

    [SerializeField] public float inGameTimer = 0;
    bool anomalyTriggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToTrigger = Random.Range(minTime, maxTime);
        anomalyDetector = GameObject.FindAnyObjectByType<AnomalyDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anomalyDetector.hasSightOfAnomaly)
        {
            if (inGameTimer >= timeToTrigger)
            {
                TriggerAnomaly();
            }
            else
            {
                inGameTimer += Time.deltaTime;
            }
        }
    }

    public void ResetAnomaly()
    {
        inGameTimer = 0;
        timeToTrigger = Random.Range(minTime, maxTime);
        anomalyTriggeredState.SetActive(false);
        anomalyDefaultState.SetActive(true);
        anomalyTriggered = false;
    }

    public void TriggerAnomaly()
    {
        if (anomalyTriggered == false)
        {
            if (anomalyTriggerSfx != null)
            {
                anomalyTriggerSfx.panStereo = Random.Range(-1, 2);
                anomalyTriggerSfx.Play();
            }

            anomalyTriggeredState.SetActive(true);
            anomalyDefaultState.SetActive(false);

            anomalyTriggered = true;
        }
    }
}