using FluentAssertions;
using NUnit.Framework;
using Quick.Notification.Domain.Abstractions.Helpers;
using System;
using System.Reflection;

namespace Quick.Notification.Tests.Domain.Abstractions
{
    class CreatorHelperTests
    {
        [Test]
        public void NotifyCommandHandler_ShouldBeSuccess()
        {
            // Arrange
            string applicationIdentityFake = Assembly.GetEntryAssembly().GetName().Name;
            string systemUserFake = Environment.UserName;
            string hostnameFake = Environment.MachineName;
            string expectedResult = $"{systemUserFake}@{hostnameFake} ({applicationIdentityFake})";

            // Action
            var result = CreatorHelper.GetEntityCreatorIdentity();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
