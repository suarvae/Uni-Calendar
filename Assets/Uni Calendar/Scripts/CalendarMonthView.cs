using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Suarvae.UniCalendar
{
    public class CalendarMonthView : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] private TMP_Text monthLabel;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        [Header("Grid")]
        [SerializeField] private CalendarDayCell[] cells; // must be 42 cells

        [Header("Setup")]
        [SerializeField] private Color cellImageColor = Color.white;
        [SerializeField] private TMP_FontAsset dateFont;

        [Header("Editor")]
        [SerializeField] private bool updateToCurrentDayInEditor = true;

        private DateTime visibleMonth;
        private DateTime today;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

#if UNITY_EDITOR
            // OnValidate must not mutate TMP/Button state directly; defer it.
            UnityEditor.EditorApplication.delayCall -= EditorRefresh;
            UnityEditor.EditorApplication.delayCall += EditorRefresh;
#endif
        }

#if UNITY_EDITOR
        private void EditorRefresh()
        {
            if (this == null || Application.isPlaying)
            {
                return;
            }

            today = DateTime.Today;

            if (updateToCurrentDayInEditor || visibleMonth.Year < 2)
            {
                visibleMonth = new DateTime(today.Year, today.Month, 1);
            }

            Refresh();
        }
#endif

        private void Awake()
        {
            EnsureStateInitialized(true);

            if (previousButton != null)
            {
                previousButton.onClick.RemoveListener(ShowPreviousMonth);
                previousButton.onClick.AddListener(ShowPreviousMonth);
            }

            if (nextButton != null)
            {
                nextButton.onClick.RemoveListener(ShowNextMonth);
                nextButton.onClick.AddListener(ShowNextMonth);
            }

            Refresh();
        }

        private void ShowPreviousMonth()
        {
            EnsureStateInitialized(false);
            visibleMonth = visibleMonth.AddMonths(-1);
            Refresh();
        }

        private void ShowNextMonth()
        {
            EnsureStateInitialized(false);
            visibleMonth = visibleMonth.AddMonths(1);
            Refresh();
        }

        private void EnsureStateInitialized(bool resetToCurrentMonth)
        {
            today = DateTime.Today;

            if (resetToCurrentMonth || visibleMonth.Year < 2)
            {
                visibleMonth = new DateTime(today.Year, today.Month, 1);
            }
        }

        private void Refresh()
        {
            if (monthLabel == null || cells == null)
            {
                return;
            }

            EnsureStateInitialized(false);

            monthLabel.text = visibleMonth.ToString("MMMM yyyy");

            DateTime firstOfMonth = new DateTime(visibleMonth.Year, visibleMonth.Month, 1);
            int startOffset = (int)firstOfMonth.DayOfWeek;
            DateTime gridStartDate = firstOfMonth.AddDays(-startOffset);

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == null)
                {
                    continue;
                }

                DateTime cellDate = gridStartDate.AddDays(i);

                bool isCurrentMonth = cellDate.Month == visibleMonth.Month &&
                                      cellDate.Year == visibleMonth.Year;

                cells[i].ApplyAppearance(dateFont, cellImageColor);

                bool isToday = isCurrentMonth && cellDate.Date == today;

                cells[i].Bind(cellDate, isCurrentMonth, isToday, OnDateClicked);
            }
        }

        private void OnDateClicked(DateTime date)
        {
            Debug.Log("Clicked: " + date.ToShortDateString());
        }
    }
}
