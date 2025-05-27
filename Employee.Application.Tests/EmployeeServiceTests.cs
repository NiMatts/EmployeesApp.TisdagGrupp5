using Moq;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Infrastructure.Persistance.Repositories;
namespace EmployeesApp.Application.Tests;

public class EmployeeServiceTests
{
    [Fact]
    public void GetById_ValidId_ReturnsEmployee()
    {
        // Arrage
        var employeeRepository = new Mock<IEmployeeRepository>();
        employeeRepository
            .Setup(o => o.GetById(1))
            .Returns(new Employee { Id = 1, Name = "Acme", Email = "London" });

        var employeeService = new EmployeeService(employeeRepository.Object);

        // Act
        var result = employeeService.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Employee>(result);
        employeeRepository.Verify(o => o.GetById(1), Times.Exactly(1));
        //Debug.Assert(true);
    }

    [Fact]
    public void Add_AddsEmployee()
    {
        //Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        var employeeService = new EmployeeService(employeeRepository.Object);
        var employee = new Employee { Id = 1, Name = "Acme", Email = "London" };
        //Act

        employeeService.Add(employee);
        //Assert

        Assert.NotNull(employee);
        Assert.True(employeeService.GetAll().Length > 0);
        Assert.Contains(employee, employeeService.GetAll());
    }

    [Fact]
    public void GetAll_ReturnsAllEmployees()
    {
        //Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        employeeRepository
            .Setup(o => o.GetAll())
            .Returns([new Employee { Id = 1, Name = "AcmeTest1", Email = "LondonTest1" }, 
                      new Employee { Id = 2, Name = "AcmeTest2", Email = "LondonTest2" }]);
                     

        var employeeService = new EmployeeService(employeeRepository.Object);
        //Act
        var result = employeeService.GetAll();
        //Assert
        Assert.NotNull(result);
        //[.. employeeRepository.GetAll().OrderBy(e => e.Name)];
    }

}
