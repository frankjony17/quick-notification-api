using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using Quick.Notification.Api.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Notification.Tests.Api
{
    class StartupTests
    {
        private Mock<IHostEnvironment> _hostEnvironment;
        private Mock<IApplicationBuilder> _applicationBuilder;
        private Mock<IWebHostEnvironment> _environment;
        private Mock<IApiVersionDescriptionProvider> _provider;

        [SetUp]
        public void SetUp()
        {
            _hostEnvironment = new Mock<IHostEnvironment>();
            _hostEnvironment.Setup(e => e.ContentRootPath).Returns(Environment.CurrentDirectory);
            _applicationBuilder = new Mock<IApplicationBuilder>();
            _environment = new Mock<IWebHostEnvironment>();
            _provider = new Mock<IApiVersionDescriptionProvider>();
        }

        [Test]
        public void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            var target = new Startup(_hostEnvironment.Object);
            Action action = () => target.ConfigureServices(services);
            action.Should().NotThrow();
        }
    }
}
