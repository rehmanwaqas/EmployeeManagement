using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly MySQLServerDBContext _context;
        public EmployeeController(ILogger<EmployeeController> logger, MySQLServerDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllEmployees", Name = "GetAllEmployees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            return Ok(await _context.Employees.ToListAsync());
            //return Ok(Employees);
        }

        [HttpGet("GetEmployee/{id}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            //var emp = Employees.Find(x => x.Id == id) ;
            var emp = await _context.Employees.FindAsync(id) ;
            if (emp == null)
                return BadRequest("Employee not found");
            return Ok(emp) ;
        }

        [HttpPost("SaveEmployee", Name = "SaveEmployee")]
        public async Task<ActionResult<List<Employee>>> SaveEmployee([FromBody]Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync());
            //Employees.Add(emp);
            //return Ok(Employees);
        }

        [HttpPut("UpdateEmployee", Name = "UpdateEmployee")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee([FromBody] Employee employee)
        {
            Employee emp = await _context.Employees.FindAsync(employee.Id);

            //Employee emp = Employees.Find(x => x.Id == employee.Id);
            if (emp == null)
                return BadRequest("Employee not found");

            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.PhoneNumber = employee.PhoneNumber;
            emp.Email = employee.Email;
            emp.Country = employee.Country;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
            //return Ok(Employees.Find(x => x.Id == employee.Id));
        }

        [HttpDelete("DeleteEmployee/{id}", Name = "DeleteEmployee")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(long id)
        {
            Employee emp = await _context.Employees.FindAsync(id);
            //Employee emp = Employees.Find(x => x.Id == id);
            if (emp == null)
                return BadRequest("Employee not found");

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync());
            //return Ok(Employees);
        }
    }
}