﻿using FluentAssertions;
using NUnit.Framework;
using Quick.Notify.Domain.Abstractions.Helpers;
using System;

namespace Quick.Notify.Tests.Domain.Abstractions
{
    public class TraceMessageHelperTests
    {
        private Guid guid;

        [SetUp]
        public void SetUp()
        {
            guid = Guid.NewGuid();
        }

        [Test]
        public void GetGeneratedByCorrelationIdMessage_ReturnGeneratedBymessage()
        {
            // Arrange
            string expectedMessage = $"Generated by correlation id: {guid}.";

            // Action
            var message = TraceMessageHelper.GetGeneratedByCorrelationIdMessage(guid);

            // Assert
            message.Should().Be(expectedMessage);
        }

        [Test]
        public void GetCorrelationIdChangeMessage_ReturnChangeMessage()
        {
            // Arrange
            string expectedMessage = $"New correlation id generated: {guid}. Derived from: {guid}.";

            // Action
            var message = TraceMessageHelper.GetCorrelationIdChangeMessage(guid, guid);

            // Assert
            message.Should().Be(expectedMessage);
        }
    }
}
