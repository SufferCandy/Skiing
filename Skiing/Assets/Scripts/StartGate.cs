using UnityEngine;
using static GameManager;

public class StartGate : MonoBehaviour
{
    public static event GameManager.TimerEvent TimerStart;
    {
        private void OnTriggerEnter(Collider other)
        {
            TimerStart.Invoke();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
