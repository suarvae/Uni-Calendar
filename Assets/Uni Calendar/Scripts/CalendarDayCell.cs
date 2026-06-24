using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Suarvae.UniCalendar
{
    public class CalendarDayCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text dayNumberText;
        [SerializeField] private Button button;

        private const float InactiveMonthAlpha = 0.5f;
        private const float CurrentMonthAlpha = 1f;

        private DateTime date;
        private Action<DateTime> onClicked;
        private TMP_FontAsset defaultFont;
        private bool hasDefaultFont;
        private TMPro.FontStyles defaultFontStyle;
        private bool hasDefaultFontStyle;
        private Color defaultTextColor;
        private bool hasDefaultTextColor;

        public bool IsBoundToDate(DateTime targetDate)
        {
            return date.Date == targetDate.Date;
        }

        public bool DisplaysDayNumber(int day)
        {
            return dayNumberText != null && dayNumberText.text == day.ToString();
        }

        public void ApplyAppearance(TMP_FontAsset dateFont, Color cellColor)
        {
            CaptureDefaults();

            if (dayNumberText != null)
            {
                dayNumberText.font = dateFont != null ? dateFont : defaultFont;
                dayNumberText.color = hasDefaultTextColor ? defaultTextColor : dayNumberText.color;
            }

            if (button != null && button.image != null)
            {
                button.image.color = cellColor;
            }
        }

        public void Bind(DateTime newDate, bool isCurrentMonth, bool isToday, Action<DateTime> clickCallback)
        {
            date = newDate;
            onClicked = clickCallback;

            CaptureDefaults();

            if (dayNumberText != null)
            {
                dayNumberText.text = newDate.Day.ToString();
            }

            SetAlpha(isCurrentMonth ? CurrentMonthAlpha : InactiveMonthAlpha);
            SetUnderline(isToday);

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HandleClick);
            }
        }

        private void CaptureDefaults()
        {
            if (dayNumberText == null)
            {
                return;
            }

            if (!hasDefaultFont)
            {
                defaultFont = dayNumberText.font;
                hasDefaultFont = true;
            }

            if (!hasDefaultFontStyle)
            {
                defaultFontStyle = dayNumberText.fontStyle;
                hasDefaultFontStyle = true;
            }

            if (!hasDefaultTextColor)
            {
                defaultTextColor = dayNumberText.color;
                hasDefaultTextColor = true;
            }
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
}
