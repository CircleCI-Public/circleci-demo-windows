# circleci-demo-windows

This repo contains multiple examples for using CircleCI Windows executor.

- [HelloWorld](./HelloWorld/README.md) - Simple Console Application
- [NotepadTest](./NotepadTest/README.md) - Native Windows Application UI test
- [WebBrowserTestsSample](./WebBrowserTestsSample/README.md) - Web Browser integration test

The [CI pipeline](.circleci/config.yml) contains two distinct workflows:

- `hello-world` - This is a simple Hello World demo application that uses the CircleCI Windows executor.
- `integration-tests` - This contains two different test strategies that can be used with CircleCI Windows executor:
  - `browser-tests` -  This is a simple integration test using a Web Browser to navigate to a page and assert its contents.
  - `ui-tests`- This is a simple test for a native Windows App (Notepad) using `WinAppDriver`.
