using System;
using System.Collections.Generic;
using System.Linq;
using EmployeesWebNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeesWebNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = configuration["filepath"];
            
            
            if (string.IsNullOrEmpty(_filePath))
            {
                throw new Exception("The data file cannot be found, please provide a data file.");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new List<Employee>();
            var fileString = System.IO.File.ReadAllLinesAsync(_filePath).Result;
            foreach (var s in fileString)
            {
                var value = s.Split(";");
                result.Add(
                    new Employee{
                        Name = value[0].Split("=")[1],
                        Surname = value[1].Split("=")[1]
                    });
            }
            return Ok(result);
        }
        
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var result = new List<Employee>();
            var fileString = System.IO.File.ReadAllLinesAsync(_filePath).Result;
            foreach (var s in fileString)
            {
                var value = s.Split(";");
                result.Add(
                    new Employee{
                        Name = value[0].Split("=")[1],
                        Surname = value[1].Split("=")[1]
                    });
            }

            var employee = result
                .FirstOrDefault(i=> string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            System.IO.File.AppendAllText(_filePath, $"{Environment.NewLine}Name={employee.Name};Surname={employee.Surname};");
            return CreatedAtAction("Get", new {name = employee.Name}, employee);
        }
    }
}