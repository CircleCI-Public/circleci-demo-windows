# NotepadTest

This test uses [WinAppDriver](https://github.com/microsoft/WinAppDriver) to test Windows Apps UI. This was copied from [WinAppDriver Samples](https://github.com/microsoft/WinAppDriver/tree/master/Samples/C%23/NotepadTest)

NotepadTest is a sample test project that runs and validates basic UI scenarios on Windows 10 built-in **Notepad** application. This sample depicts a typical test project that is written for a classic windows application built using Win32, DirectUI, WPF, etc. You can use this as a template for writing your test project.

In addition to the **Notepad** application primary session, this sample uses **Windows Explorer** as a secondary session to verify file creation and help clean up file artifact.

This test project highlights some common interactions below that can be put together to perform and verify various UI scenario on a classic app.
- Creating a classic windows app session
- Interacting with menu item
- Sending keyboard input to a text box
- Sending keyboard shortcut
- Using secondary session
