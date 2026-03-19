using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarDayCell : MonoBehaviour
{
    [SerializeField] private TMP_Text dayNumberText;
    [SerializeField] private Button button;

    private const float InactiveMonthAlpha = 0.5f;
    private const float CurrentMonthAlpha = 1f;

    private DateTime date;
    private Action<DateTime> onClicked;
    private TMPro.FontStyles defaultFontStyle;
    private bool hasDefaultFontStyle;

    public bool IsBoundToDate(DateTime targetDate)
    {
        return date.Date == targetDate.Date;
    }

    public bool DisplaysDayNumber(int day)
    {
        return dayNumberText != null && dayNumberText.text == day.ToString();
    }

    public void Bind(DateTime newDate, bool isCurrentMonth, bool isToday, Action<DateTime> clickCallback)
    {
        date = newDate;
        onClicked = clickCallback;

        CaptureDefaultFontStyle();

        dayNumberText.text = newDate.Day.ToString();
        SetAlpha(isCurrentMonth ? CurrentMonthAlpha : InactiveMonthAlpha);
        SetUnderline(isToday);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleClick);
    }

    private void CaptureDefaultFontStyle()
    {
        if (hasDefaultFontStyle || dayNumberText == null)
        {
            return;
        }

        defaultFontStyle = dayNumberText.fontStyle;
        hasDefaultFontStyle = true;
    }

    private void SetUnderline(bool isToday)
    {
        if (dayNumberText == null)
        {
            return;
        }

        dayNumberText.fontStyle = isToday
            ? defaultFontStyle | FontStyles.Underline
            : defaultFontStyle;
    }

    private void SetAlpha(float alpha)
    {
        if (dayNumberText != null)
        {
            Color textColor = dayNumberText.color;
            textColor.a = alpha;
            dayNumberText.color = textColor;
        }

        if (button != null && button.image != null)
        {
            Color imageColor = button.image.color;
            imageColor.a = alpha;
            button.image.color = imageColor;
        }
    }

    private void HandleClick()
    {
        onClicked?.Invoke(date);
    }
}
