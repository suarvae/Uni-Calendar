# Uni Calendar

`Uni Calendar` is a lightweight Unity monthly calendar UI built with `uGUI`, `TextMeshPro`, and a simple month-navigation workflow. It is meant as a reusable in-project component you can drop into a canvas, style, and extend for your own scheduling, productivity, or date-picking features.

Current release: `0.1.0`

## What It Does

- Renders a full month view as a fixed 6-row calendar grid
- Shows days from the previous and next month to keep the layout consistent
- Highlights the current day with an underline
- Lets the user move backward and forward between months
- Exposes day clicks so you can hook the calendar into your own systems

## How It Works

The main behavior lives in `Assets/Uni Calendar/Scripts/CalendarMonthView.cs`.

When the component refreshes:

1. It picks the visible month, defaulting to the current month.
2. It calculates the first day of that month.
3. It finds the start date for the 42-cell grid by backing up to the first visible weekday.
4. It fills each cell with a date in sequence.
5. It fades dates that are outside the active month.
6. It underlines today's date if it appears in the visible month.

Each day cell is handled by `Assets/Uni Calendar/Scripts/CalendarDayCell.cs`, which:

- updates the label text
- applies the chosen font and cell color
- changes alpha for in-month vs out-of-month dates
- forwards button clicks back to the month view

The custom inspector in `Assets/Uni Calendar/Scripts/Editor/CalendarMonthViewEditor.cs` keeps the component setup cleaner inside the Unity Editor.

## Included Assets

- Demo scene: `Assets/Uni Calendar/Scenes/Demo.unity`
- Reusable prefab: `Assets/Uni Calendar/Prefab/Calender View.prefab`
- Calendar scripts: `Assets/Uni Calendar/Scripts/`

## Unity Version

Built in `Unity 6` with editor version `6000.3.9f1`.

## Setup

1. Open the project in Unity.
2. Open `Assets/Uni Calendar/Scenes/Demo.unity` to see the calendar in use.
3. Drag `Assets/Uni Calendar/Prefab/Calender View.prefab` into your own canvas if you want to reuse it.
4. Assign your own fonts, colors, and click behavior in the inspector if needed.

## Extending It

Good next additions for a real production use case:

- selected date state
- event markers or badges
- localization / alternate week-start support
- disabled dates
- external callbacks or data binding for appointments

## Current Notes

- The current click handler logs the selected date to the Unity console.
- The grid assumes a 42-cell month layout.
- This project currently focuses on the calendar UI rather than persistence or event management.

## License

This repository includes a `LICENSE` file. Use that as the source of truth for reuse terms.
