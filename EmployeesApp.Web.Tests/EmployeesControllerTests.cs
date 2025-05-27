using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using EmployeesApp.Web.Views.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeesApp.Web.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void Index_ReturnsView()
        {
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(o => o.GetAll())
                .Returns([new Employee { Id = 1, Name = "AcmeTest1", Email = "LondonTest1" },
                      new Employee { Id = 2, Name = "AcmeTest2", Email = "LondonTest2" }]);

            var controller = new EmployeesController(employeeService.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Creates()
        {
            var employeeService = new Mock<IEmployeeService>();
            var controller = new EmployeesController(employeeService.Object);
            CreateVM viewModel = new CreateVM() { Name = "testCreate", Email = "test@test.se", BotCheck = 4 };
            var result = controller.Create(viewModel);
           
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.IsType<ViewResult>(result);
        }
    }
}
