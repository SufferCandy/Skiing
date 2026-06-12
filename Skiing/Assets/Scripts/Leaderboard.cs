using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class Leaderboard
{
    public const int MaxEntries = 5;

    private static string StorageKey(string baseKey) => baseKey + "_LEADERBOARD";

    public static List<float> GetTimes(string baseKey)
    {
        var result = new List<float>();
        string raw = PlayerPrefs.GetString(StorageKey(baseKey), "");
        if (string.IsNullOrEmpty(raw))
            return result;

        foreach (string part in raw.Split(','))
        {
            if (float.TryParse(part, NumberStyles.Float, CultureInfo.InvariantCulture, out float t))
                result.Add(t);
        }

        result.Sort();
        return result;
    }

    public static void AddTime(string baseKey, float time)
    {
        var times = GetTimes(baseKey);
        times.Add(time);
        times.Sort();

        if (times.Count > MaxEntries)
            times.RemoveRange(MaxEntries, times.Count - MaxEntries);

        var parts = new List<string>();
        foreach (float t in times)
            parts.Add(t.ToString("F2", CultureInfo.InvariantCulture));

        PlayerPrefs.SetString(StorageKey(baseKey), string.Join(",", parts));
        PlayerPrefs.Save();
    }

    public static float GetBestTime(string baseKey, float fallback = 99.99f)
    {
        var times = GetTimes(baseKey);
        return times.Count > 0 ? times[0] : fallback;
    }

    public static void Clear(string baseKey)
    {
        PlayerPrefs.DeleteKey(StorageKey(baseKey));
        PlayerPrefs.Save();
    }
}
