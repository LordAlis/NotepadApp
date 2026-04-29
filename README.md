# Notepad Application - C# OOP

> **Demonstration video is here:** https://www.youtube.com/watch?v=PLACEHOLDER_REPLACE_ME

A Windows Forms text editor built with .NET 8, demonstrating OOP, File I/O, and JSON serialization.

## Project Structure

- **Notepad.Common** - Shared models, interfaces, helpers, and the abstract service base (Class Library)
- **Notepad.App** - Windows Forms application: editor UI, find dialog, and concrete services

## OOP Concepts

| Concept | Usage |
|---------|-------|
| Interface | `IFileService`, `ISettingsService` |
| Abstract Class | `FileServiceBase` |
| Inheritance | `TextFileService : FileServiceBase` |
| Encapsulation | `EditorSettings` private fields with validated/clamped setters |
| Polymorphism | `MainForm` holds an `IFileService` reference (interface-typed) |
| Composition | `MainForm` owns `IFileService`, `ISettingsService`, `RecentFilesService` |
| Events/Delegates | `LogMessage` event on `FileServiceBase` |
| Enum | `EditorState` (Idle, Loading, Saving, Modified) |
| IDisposable | `FileServiceBase.Dispose()` |
| Static Helper | `JsonHelper.Serialize<T>` / `Deserialize<T>` (generic) |
| Custom Exception | `FileServiceException` |

## File I/O & Serialization

- `StreamReader` / `StreamWriter` with `using` blocks and `async/await`
- All file errors are wrapped in `FileServiceException`
- JSON-serialized state (via `System.Text.Json`):
  - `%AppData%\NotepadApp\settings.json` - font, font size, word wrap
  - `%AppData%\NotepadApp\recent.json` - last 5 opened files

## Prerequisites

- .NET 8 SDK
- Windows OS

## Build

```
dotnet build NotepadApp.sln
```

## Usage

1. **Run the app:** `dotnet run --project Notepad.App`
2. **New / Open:** File menu (or `Ctrl+N` / `Ctrl+O`) - the title shows `*` when there are unsaved changes
3. **Edit:** type in the editor; the status bar shows the current character count and file path
4. **Save / Save As:** File menu (or `Ctrl+S` / `Ctrl+Shift+S`)
5. **Find:** Edit > Find (or `Ctrl+F`) - supports case-sensitive search and wraps around to the start
6. **Recent Files:** File > Recent Files - reopen one of the last five files
7. **Font / Word Wrap:** Edit menu - changes are persisted to `settings.json`

## Running with Visual Studio

1. Open `NotepadApp.sln` in Visual Studio
2. Right-click `Notepad.App` > "Set as Startup Project"
3. Press F5 to run
