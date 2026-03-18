using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarDayCell : MonoBehaviour
{
    [SerializeField] private TMP_Text dayNumberText;
    [SerializeField] private Button button;

    private DateTime date;
    private Action<DateTime> onClicked;

    public void Bind(DateTime newDate, bool isCurrentMonth, bool isToday, Action<DateTime> clickCallback)
    {
        date = newDate;
        onClicked = clickCallback;

        dayNumberText.text = newDate.Day.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        onClicked?.Invoke(date);
    }
}