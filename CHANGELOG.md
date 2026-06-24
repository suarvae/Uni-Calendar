# Changelog

All notable changes to Uni Calendar are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.2.0] - 2026-06-24

Foundation & hygiene pass. No new end-user features yet — this release makes the
asset safe to drop into any project and sets up the structure for everything that
follows.

### Added
- `Suarvae.UniCalendar` assembly definition for runtime code and
  `Suarvae.UniCalendar.Editor` for editor code, isolating the calendar from
  `Assembly-CSharp`.
- `Suarvae.UniCalendar` namespace on all runtime types (and
  `Suarvae.UniCalendar.Editor` on editor types) to prevent name collisions with
  consumer code.
- This changelog.

### Changed
- Renamed the prefab `Calender View.prefab` -> `Calendar View.prefab` (typo fix).
  The asset GUID is preserved, so existing scene references keep working.

### Fixed
- `CalendarDayCell.Bind` no longer throws a `NullReferenceException` when a cell
  is missing its `dayNumberText` or `button` reference; it now guards them like
  the rest of the class already did.
- `CalendarMonthView.OnValidate` no longer mutates scene objects directly during
  validation/import (a source of "SendMessage cannot be called during
  OnValidate" warnings and spurious scene dirtying). The refresh is now deferred
  via `EditorApplication.delayCall`.

### Removed
- Deleted the unused `CalendarState` class. Month tracking lives entirely in
  `CalendarMonthView`; `CalendarState` was dead code and a second, conflicting
  source of truth.
- Removed the write-only `lastValidatedDate` serialized field, which was never
  read.

## [0.1.0]

### Added
- Initial monthly calendar UI built with uGUI and TextMeshPro.
- Fixed 6-row / 42-cell month grid with leading and trailing days.
- Previous / next month navigation.
- Underline highlight for the current day.
- Demo scene and reusable prefab.
