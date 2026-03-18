using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarMonthView : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private TMP_Text monthLabel;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Grid")]
    [SerializeField] private CalendarDayCell[] cells; // must be 42 cells

    private DateTime visibleMonth;
    private DateTime today;

    private void Awake()
    {
        today = DateTime.Today;
        visibleMonth = new DateTime(today.Year, today.Month, 1);

        previousButton.onClick.AddListener(ShowPreviousMonth);
        nextButton.onClick.AddListener(ShowNextMonth);

        Refresh();
    }

    private void ShowPreviousMonth()
    {
        visibleMonth = visibleMonth.AddMonths(-1);
        Refresh();
    }

    private void ShowNextMonth()
    {
        visibleMonth = visibleMonth.AddMonths(1);
        Refresh();
    }

    private void Refresh()
    {
        monthLabel.text = visibleMonth.ToString("MMMM yyyy");

        DateTime firstOfMonth = new DateTime(visibleMonth.Year, visibleMonth.Month, 1);
        int startOffset = (int)firstOfMonth.DayOfWeek;
        DateTime gridStartDate = firstOfMonth.AddDays(-startOffset);

        for (int i = 0; i < cells.Length; i++)
        {
            DateTime cellDate = gridStartDate.AddDays(i);

            bool isCurrentMonth = cellDate.Month == visibleMonth.Month &&
                                  cellDate.Year == visibleMonth.Year;

            bool isToday = cellDate.Date == today;

            cells[i].Bind(cellDate, isCurrentMonth, isToday, OnDateClicked);
        }
    }

    private void OnDateClicked(DateTime date)
    {
        Debug.Log("Clicked: " + date.ToShortDateString());
    }
}