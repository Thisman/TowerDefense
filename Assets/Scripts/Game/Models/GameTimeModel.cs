using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct GameTimeData
{
    public int Seconds;
    public int Minutes;
    public int Hours;
    public int Days;
}

public class GameTimeModel
{
    [SerializeField]
    private float _startTimestamp = 36000; // 10 AM

    [SerializeField]
    private float _timestamp; // Seconds after start game ( exclude main menu )

    [SerializeField]
    private float _realTimestamp; // Real seconds after start game. Use Time.DeltaTime ( exclude main menu )

    private List<int[]> _nightHoursPeriods = new List<int[]>();
    private List<int[]> _dayHoursPeriods = new List<int[]>();

    public GameTimeModel()
    {
        // Move to editor settings later...
        int[] nightPeriodAM = new int[2];
        nightPeriodAM[0] = 0;
        nightPeriodAM[1] = 5;

        int[] nightPeriodPM = new int[2];
        nightPeriodPM[0] = 19;
        nightPeriodPM[1] = 23;

        int[] dayPeriod = new int[2];
        dayPeriod[0] = 6;
        dayPeriod[1] = 18;

        _dayHoursPeriods.Add(dayPeriod);
        _nightHoursPeriods.Add(nightPeriodAM);
        _nightHoursPeriods.Add(nightPeriodPM);
    }

    public float StartTimeStamp { get { return _startTimestamp; } }

    public float Timestamp
    {
        get { return _timestamp; }
        set { _timestamp = value; }
    }
    
    public float RealTimestamp
    {
        get { return _realTimestamp; }
        set { _realTimestamp = value; }
    }

    public bool IsDay()
    {
        GameTimeData timeData = GetTimeData();
        foreach (var period in _dayHoursPeriods)
        {
            if (timeData.Hours >= period[0] && timeData.Hours <= period[1]) return true;
        }

        return false;
    }

    public bool IsNight()
    {
        GameTimeData timeData = GetTimeData();
        foreach (var period in _nightHoursPeriods)
        {
            if (timeData.Hours >= period[0] && timeData.Hours <= period[1]) return true;
        }

        return false;
    }

    public GameTimeData GetTimeData()
    {
        // Constants
        const int SecondsInMinute = 60;
        const int SecondsInHour = 3600;
        const int SecondsInDay = 86400;

        // Calculate the day
        int day = (int)(_timestamp / SecondsInDay) + 1;

        // Remaining seconds in the current day
        float remainingSeconds = _timestamp % SecondsInDay;

        // Calculate hours, minutes, and seconds
        int hour = (int)(remainingSeconds / SecondsInHour);
        int minutes = (int)((remainingSeconds % SecondsInHour) / SecondsInMinute);
        int seconds = (int)(remainingSeconds % SecondsInMinute);

        // Return the result
        return new GameTimeData
        {
            Days = day,
            Hours = hour,
            Minutes = minutes,
            Seconds = seconds
        };
    }

    public long GetSecondsSinceStartDay(GameTimeData timeData)
    {
        // Calculate total seconds
        int totalSeconds = (timeData.Hours * 3600) + (timeData.Minutes * 60) + timeData.Seconds;
        return totalSeconds;
    }
}
