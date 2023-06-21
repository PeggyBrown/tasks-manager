# Tasks Manager

A simple service for reading tasks from an `input.json` file and managing them in Trello. This project was created for testing purposes to demonstrate the process of refactoring and introducing various tests.

## Prerequisites

Before running the program or tests, ensure that you have the following:

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- IDE (preferably [Rider](https://www.jetbrains.com/rider/) or [ReSharper](https://www.jetbrains.com/resharper/)) for running the program.
- [Trello API credentials](https://trello.com/power-ups/admin) (apiKey and apiToken) set up in the Atlassian Developer system. They should be placed in the correct place in `settings.json`.

## Getting Started

To get started with the Tasks Manager, follow these steps:

1. Clone the repository:

```bash
git clone https://github.com/your-username/tasks-manager.git
```

2. Open the project in your preferred IDE (Rider or ReSharper).

3. Configure Trello API credentials:
   - Go to [Atlassian Developer](https://trello.com/power-ups/admin) and obtain your `apiKey` and `apiToken`.
   - Update the corresponding values in the project configuration.

4. Run the Program:
   - Open the `Program.cs` file.
   - Configure the input file path and Trello board ID if necessary.
   - Run the program in your IDE to read tasks from the input file and manage them in Trello.

5. Run the Tests:
   - Open the test project in your IDE.
   - Ensure that the necessary NuGet packages are restored.
   - Run the tests using the testing framework provided by your IDE or using the `dotnet test` command.

## Project Structure

The project is organized as follows:

- `TaskManager`: Contains the main implementation of the tasks management service.
- `TaskManagerTests`: Contains some unit tests in NUnit.
- `xTaskManagerTests`: Contains unit and integration tests for the `TaskManager` in xUnit as well as Calculator examples.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgements

This project was created for educational and testing purposes, inspired by the need to demonstrate refactoring and test introduction techniques. We would like to acknowledge the creators of the tools and frameworks used in this project for their contributions to the software development community.

- [Rider](https://www.jetbrains.com/rider/) - An intelligent IDE for .NET development.
- [ReSharper](https://www.jetbrains.com/resharper/) - A productivity tool for .NET developers.
- [xUnit](https://xunit.net/) - A unit testing framework for .NET.
