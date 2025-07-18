using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float hour => _hour;
    public float minute => _minute;
    public string timeString => _hour.ToString("00") + "h" + _minute.ToString("00");
    public bool timeIsTicking = false;
    public Action<float, float> TimeChanged;
    public Action<float> MinutesAdded;          
    
    [SerializeField] private const float RealSecondsPerInGameDay = 60f * 24f;

    private const float HourPerDay = 24f;
    private const float MinutePerHour = 60f;
    
    private float _day;
    private float _hour;
    private float _minute;

    private float dayNormalized => _day % 1f;
    
    void Update()
    {
        if (!timeIsTicking) return;
        
        _day += Time.deltaTime / RealSecondsPerInGameDay;
        var dayNorm = dayNormalized;
        
        var newHour = Mathf.Floor(dayNorm * HourPerDay);
        if (newHour >= _hour) TimeChanged?.Invoke(_hour, _minute);
        _hour = newHour;
        
        var newMinute = Mathf.Floor(((dayNorm * HourPerDay) % 1f) * MinutePerHour);
        if (newMinute >= _minute) TimeChanged?.Invoke(_hour, _minute);
        _minute = newMinute;
    }

    public void SetTimeOfDay(float hours, float minutes)
    {
        _day = (hours + (minutes / MinutePerHour)) / HourPerDay;
        _hour = Mathf.Floor((dayNormalized) * HourPerDay);
        _minute = Mathf.Floor((((dayNormalized) * HourPerDay) % 1f) * MinutePerHour);
        TimeChanged?.Invoke(_hour, _minute);
    }

    public void AddHours(float hours)
    {
        if (hours <= 0) return;
        
        _day += (hours / HourPerDay);
        _hour = Mathf.Floor((dayNormalized) * HourPerDay);
        TimeChanged?.Invoke(_hour, _minute);
        //MinutesAdded?.Invoke(hours * MinutePerHour);
    }
    
    public void AddMinutes(float minutes)
    {
        if (minutes <= 0) return;
        
        _day += ((minutes / MinutePerHour) / HourPerDay);
        _minute = Mathf.Floor((((dayNormalized) * HourPerDay) % 1f) * MinutePerHour);
        TimeChanged?.Invoke(_hour, _minute);
        MinutesAdded?.Invoke(minutes);
    }
}
