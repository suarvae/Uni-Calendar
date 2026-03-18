using System;
using UnityEngine;

public class CalendarState
{
    public DateTime VisibleMonth { get; private set; }

    public CalendarState(DateTime startDate)
    {
        VisibleMonth = new DateTime(startDate.Year, startDate.Month, 1);
    }

    public void NextMonth()
    {
        VisibleMonth = VisibleMonth.AddMonths(1);
    }

    public void PreviousMonth()
    {
        VisibleMonth = VisibleMonth.AddMonths(-1);
    }
}