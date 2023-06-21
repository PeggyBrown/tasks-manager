using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using Moq;
using TaskManager;
using Xunit;

namespace xTaskManagerTests
{
    public class TasksManagerTests
    {
        [Fact]
        public void PrepareTasksFromFile_WhenJsonParsingFails_ShouldReturnFalse()
        {
            // Arrange
            var apiHelperMock = new Mock<IApiHelper>();
            var filesHelperMock = new Mock<IFilesHelper>();
            filesHelperMock.Setup(fh => fh.GetTasksInput()).Returns("invalid json");

            var tasksManager = new TasksManager(apiHelperMock.Object, filesHelperMock.Object);

            // Act
            bool result = tasksManager.PrepareTasksFromFile();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PrepareTasksFromFile_WhenJsonParsingSucceeds_ShouldReturnTrue()
        {
            // Arrange
            var apiHelperMock = new Mock<IApiHelper>();
            var filesHelperMock = new Mock<IFilesHelper>();
            apiHelperMock.Setup(x => x.ReadAllTasksFromBoard(It.IsAny<string>())).Returns(new List<TrelloTask>());
            apiHelperMock.Setup(x => x.ReadAllListsFromBoard(It.IsAny<string>())).Returns(new List<TrelloList>());

            filesHelperMock.Setup(fh => fh.GetTasksInput()).Returns(
                "{\"Name\": \"Kanban Board\",\"Lists\": []}");

            var tasksManager = new TasksManager(apiHelperMock.Object, filesHelperMock.Object);

            // Act
            bool result = tasksManager.PrepareTasksFromFile();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AddTask_ShouldSendPostRequestWithCorrectUrlAndData()
        {
            // Arrange
            string listId = "list123";
            string taskName = "Task 1";
            string desc = "Description of Task 1";
            string expectedUrl = "https://api.trello.com/1/cards?key=apiKey123&token=apiToken123";

            Mock<IRequestSender> requestSenderMock = new Mock<IRequestSender>();
            Mock<IFilesHelper> filesHelperMock = new Mock<IFilesHelper>();
            filesHelperMock.Setup(fh => fh.GetApiKey()).Returns("apiKey123");
            filesHelperMock.Setup(fh => fh.GetApiToken()).Returns("apiToken123");

            ApiHelper apiHelper = new ApiHelper(requestSenderMock.Object, filesHelperMock.Object);

            // Act
            apiHelper.AddTask(listId, taskName, desc);

            // Assert
            requestSenderMock.Verify(rs => rs.SendPostRequest(
                It.Is<string>(url => url == expectedUrl),
                It.IsAny<NameValueCollection>()
            ), Times.Once);
        }


        [Fact]
        public void ReadAllTasksFromBoard_ShouldSendGetAllTasksRequestWithCorrectUrl()
        {
            // Arrange
            string boardId = "board123";
            string expectedUrl = "https://api.trello.com/1/boards/board123/cards?key=apiKey123&token=apiToken123";

            Mock<IRequestSender> requestSenderMock = new Mock<IRequestSender>();
            Mock<IFilesHelper> filesHelperMock = new Mock<IFilesHelper>();
            filesHelperMock.Setup(fh => fh.GetApiKey()).Returns("apiKey123");
            filesHelperMock.Setup(fh => fh.GetApiToken()).Returns("apiToken123");

            ApiHelper apiHelper = new ApiHelper(requestSenderMock.Object, filesHelperMock.Object);

            // Act
            apiHelper.ReadAllTasksFromBoard(boardId);

            // Assert
            requestSenderMock.Verify(rs => rs.SendGetAllTasksRequest(
                It.Is<string>(url => url == expectedUrl)
            ).Result, Times.Once);
        }


        [Fact]
        public void ReadAllListsFromBoard_ShouldSendGetAllListsRequestWithCorrectUrl()
        {
            // Arrange
            string boardId = "board123";
            string expectedUrl = "https://api.trello.com/1/boards/board123/Lists?key=apiKey123&token=apiToken123";

            Mock<IRequestSender> requestSenderMock = new Mock<IRequestSender>();
            Mock<IFilesHelper> filesHelperMock = new Mock<IFilesHelper>();
            filesHelperMock.Setup(fh => fh.GetApiKey()).Returns("apiKey123");
            filesHelperMock.Setup(fh => fh.GetApiToken()).Returns("apiToken123");

            ApiHelper apiHelper = new ApiHelper(requestSenderMock.Object, filesHelperMock.Object);

            // Act
            apiHelper.ReadAllListsFromBoard(boardId);

            // Assert
            requestSenderMock.Verify(rs => rs.SendGetAllListsRequest(
                It.Is<string>(url => url == expectedUrl)
            ).Result, Times.Once);
        }

        [Fact]
        public void IntegTest()
        {
            // Arrange
            var filesHelper = new FilesHelper();
            TasksManager tasksManager = new TasksManager(new ApiHelper(new RequestSender(), filesHelper), filesHelper);

            // Act
            bool result = tasksManager.PrepareTasksFromFile();

            // Assert
            result.Should().BeTrue();
        }
    }
}