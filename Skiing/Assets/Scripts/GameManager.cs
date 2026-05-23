using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();
    public delegate void TimerPenaltyEvent(float penaltyTime);
    private bool isRacing= false;
    private float raceTime = 0;
    [SerializeField] private TMP_Text timeText, bestTimeText;
    private float bestTime= 99.99f;
    [SerializeField] private string bestTimeKey ="LVL1_BEST_TIME";
    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat(bestTimeKey,99.99f);
        bestTimeText.text = "BEST TIME: " + bestTime.ToString("F2");
    }
    private void OnEnable()
    {
        StartGate.TimerStart += StartTimer;
        FinishGate.TimerEnd += StopTimer;
        SlalomFlag.Penalty += AddPenalty;
    }
    private void OnDisable()
    {
        StartGate.TimerStart -= StartTimer;
        FinishGate.TimerEnd -= StopTimer;
    }
    private void AddPenalty(float penalty)
    {
        raceTime += penalty;
        Debug.Log("Recieved penalty");
    }
    private void StartTimer()
    {
        Debug.Log("started timer");
        isRacing = true;
    }
    private void StopTimer()
    {
        Debug.Log("stopped timer. Race time: " + raceTime);
        isRacing = false; 
        if (raceTime < bestTime)
        {
            bestTime = raceTime;
            bestTimeText.text = "BEST TIME: " + bestTime.ToString("F2");
            PlayerPrefs.SetFloat(bestTimeKey, bestTime);
            PlayerPrefs.Save();
            bestTimeText.color = Color.yellow;
        }
        Invoke("RestartScene", 3);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Update()
    {
        if(isRacing)
        {
            raceTime += Time.deltaTime;            
        }
        timeText.text = "TIME: " + raceTime.ToString("F2");
    }
}

