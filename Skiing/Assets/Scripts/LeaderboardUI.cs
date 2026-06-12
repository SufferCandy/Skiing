using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardText;
    [SerializeField] private string bestTimeKey = "LVL1_BEST_TIME";
    [SerializeField] private string header = "TOP TIMES";

    private void OnEnable()
    {
        GameManager.OnLeaderboardChanged += Refresh;
    }

    private void OnDisable()
    {
        GameManager.OnLeaderboardChanged -= Refresh;
    }

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (leaderboardText == null)
            return;

        List<float> times = Leaderboard.GetTimes(bestTimeKey);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(header);

        if (times.Count == 0)
        {
            sb.AppendLine("-");
        }
        else
        {
            for (int i = 0; i < times.Count; i++)
                sb.AppendLine((i + 1) + ".  " + times[i].ToString("F2") + "s");
        }

        leaderboardText.text = sb.ToString();
    }
}