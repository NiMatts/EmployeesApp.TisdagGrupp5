using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using EmployeesApp.Web.Views.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static EmployeesApp.Web.Views.Employees.IndexVM;

namespace EmployeesApp.Web.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void Test_Index_ReturnsView()
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
        public void Test_Create_CheckReturnType()
        {
            var employeeService = new Mock<IEmployeeService>();
            var controller = new EmployeesController(employeeService.Object);
            CreateVM viewModel = new CreateVM() { Name = "testCreate", Email = "test@test.se", BotCheck = 4 };
            var result = controller.Create(viewModel);
           
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            
        }

        [Fact]
        public void Test_Details_IsValidType()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository
                .Setup(o => o.GetById(1))
                .Returns(new Employee { Id = 1, Name = "Acme", Email = "London" });

            var employeeService = new Mock<EmployeeService>(employeeRepository.Object);
            var controller = new EmployeesController(employeeService.Object);

            var result = controller.Details(1);

            Assert.IsType<ViewResult>(result);
        }
    }
}
