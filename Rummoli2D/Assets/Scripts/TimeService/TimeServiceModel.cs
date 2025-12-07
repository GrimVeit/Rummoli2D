using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeServiceModel
{
    private readonly DateTime eventDateTime = new DateTime(2025, 10, 27, 7, 0, 0);

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public  void CheckDateTime()
    {
        TimeZoneInfo mscTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        DateTime mscDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, mscTimeZone);

        Debug.Log(mscDateTime);

        if (mscDateTime >= eventDateTime)
        {
            Debug.Log("Время наступило");

            OnEventReached?.Invoke();
        }
        else
        {
            Debug.Log("Время не наступило");

            OnEventNotYet?.Invoke();
        }
    }

    public event Action OnEventReached;
    public event Action OnEventNotYet;
}
