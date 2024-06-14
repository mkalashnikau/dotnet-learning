using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Api;
using BrainstormSessions.Controllers;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using Xunit;

namespace BrainstormSessions.Test.UnitTests
{
    public class LoggingTests : IDisposable
    {
        public LoggingTests()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestCorrelator()
                .CreateLogger();
        }

        public void Dispose()
        {
            // Cleanup
            Log.CloseAndFlush();
        }

        [Fact]
        public async Task HomeController_Index_LogInfoMessages()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());
            var controller = new HomeController(mockRepo.Object);

            using (TestCorrelator.CreateContext())
            {
                // Act
                var result = await controller.Index();

                // Assert
                var logEvent = TestCorrelator.GetLogEventsFromCurrentContext().SingleOrDefault();
                Assert.NotNull(logEvent);
                Assert.Equal("Getting list of sessions", logEvent.MessageTemplate.Text);
            }
        }

        [Fact]
        public async Task HomeController_IndexPost_LogWarningMessage_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            var controller = new HomeController(mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new HomeController.NewSessionModel();

            using (TestCorrelator.CreateContext())
            {
                // Act
                var result = await controller.Index(newSession);

                // Assert
                var logEvent = TestCorrelator.GetLogEventsFromCurrentContext().SingleOrDefault();
                Assert.NotNull(logEvent);
                Assert.Equal("An error occurred while creating a new session", logEvent.MessageTemplate.Text);
            }
        }

        [Fact]
        public async Task IdeasController_CreateActionResult_LogErrorMessage_WhenModelStateIsInvalid()
        {
            // Arrange & Act
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            var controller = new IdeasController(mockRepo.Object, Log.Logger);
            controller.ModelState.AddModelError("error", "some error");

            using (TestCorrelator.CreateContext())
            {
                // Act
                var result = await controller.CreateActionResult(model: null);

                // Assert
                var logEvent = TestCorrelator.GetLogEventsFromCurrentContext().SingleOrDefault();
                Assert.NotNull(logEvent);
                Assert.Equal("Model state is invalid", logEvent.MessageTemplate.Text);
            }
        }

        [Fact]
        public async Task SessionController_Index_LogDebugMessages()
        {
            // Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo
                .Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(GetTestSessions().FirstOrDefault(s => s.Id == testSessionId));
            var controller = new SessionController(mockRepo.Object);

            using (TestCorrelator.CreateContext())
            {
                // Act
                var result = await controller.Index(testSessionId);

                // Assert
                var logEvent = TestCorrelator.GetLogEventsFromCurrentContext().Single();
                Assert.NotNull(logEvent);
                Assert.Equal("Session loaded", logEvent.MessageTemplate.Text);
            }
        }

        private static List<BrainstormSession> GetTestSessions() =>
            new List<BrainstormSession>
            {
                new BrainstormSession
                {
                    DateCreated = new DateTime(2016, 7, 2),
                    Id = 1,
                    Name = "Test One",
                },
                new BrainstormSession
                {
                    DateCreated = new DateTime(2016, 7, 1),
                    Id = 2,
                    Name = "Test Two",
                },
            };
    }
}