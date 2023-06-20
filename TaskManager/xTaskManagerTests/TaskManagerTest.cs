using FluentAssertions;
using Moq;
using Xunit;

namespace TaskManager
{
    public class TasksManagerTests
    {
        [Fact]
        public void PrepareTasksFromFile_WhenJsonParsingFails_ShouldReturnFalse()
        {
            // Arrange
            var apiHelperMock = new Mock<ApiHelper>();
            var filesHelperMock = new Mock<FilesHelper>();
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
            var apiHelperMock = new Mock<ApiHelper>();
            var filesHelperMock = new Mock<FilesHelper>();
            filesHelperMock.Setup(fh => fh.GetTasksInput()).Returns("valid json");

            var tasksManager = new TasksManager(apiHelperMock.Object, filesHelperMock.Object);

            // Act
            bool result = tasksManager.PrepareTasksFromFile();

            // Assert
            result.Should().BeTrue();
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