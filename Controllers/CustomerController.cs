using accounting.Models;
using accounting.Repositories;
using accounting.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace accounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            try
            {
                await _customerRepository.Create(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the customer: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetById(id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the customer: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetById(id);
                if (existingCustomer == null)
                    return NotFound();

                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                // Update other properties as needed

                await _customerRepository.Update(existingCustomer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the customer: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
            

                await _customerRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the customer: {ex.Message}");
            }
        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using restAPI.Models;

//// For more information on enabling Web API for empty projects, visit 

//namespace restAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]      // this command made me crazy
//    public class CustomerController : ControllerBase
//    {
//        public readonly IConfiguration _configuration;
//        public readonly IWebHostEnvironment _env; // dependency injection for photos

//        public CustomerController(IConfiguration configuration, IWebHostEnvironment env)
//        {
//            _configuration = configuration;
//            _env = env;

//        }
//        // GET: api/values
//        [HttpGet]
//        public JsonResult Get()
//        {
//            string query = @"
//                            select EmployeeId,EmployeeName,Department,
//                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFileName
//                            from
//                            dbo.Employee
//                            ";
//            DataTable table = new DataTable();
//            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
//            SqlDataReader myReader;
//            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
//            {
//                myCon.Open();
//                using (SqlCommand myCommand = new SqlCommand(query, myCon))
//                {
//                    myReader = myCommand.ExecuteReader();
//                    table.Load(myReader);
//                    myReader.Close();
//                    myCon.Close();
//                }
//            }
//            return new JsonResult(table);
//        }

//        [HttpPost]
//        public JsonResult Post(Employee emp)
//        {
//            string query = @"
//                            insert into dbo.Employee
//                            (EmployeeName,Department,DateOfJoining,PhotoFileName)
//                            values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)
//                            ";
//            DataTable table = new DataTable();
//            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
//            SqlDataReader myReader;
//            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
//            {
//                myCon.Open();
//                using (SqlCommand myCommand = new SqlCommand(query, myCon))
//                {
//                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
//                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
//                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
//                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
//                    myReader = myCommand.ExecuteReader();
//                    table.Load(myReader);
//                    myReader.Close();
//                    myCon.Close();
//                }
//            }
//            return new JsonResult("Added successfully");
//        }

//        [HttpPut]
//        public JsonResult Put(Employee emp)
//        {
//            string query = @"
//                            update dbo.Employee
//                            set EmployeeName = @EmployeeName,
//                                Department = @Department,
//                                DateOfJoining = @DateOfJoining,
//                                PhotoFileName = @PhotoFileName
//                            where EmployeeId = @EmployeeId
//                            ";
//            DataTable table = new DataTable();
//            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
//            SqlDataReader myReader;
//            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
//            {
//                myCon.Open();
//                using (SqlCommand myCommand = new SqlCommand(query, myCon))
//                {
//                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
//                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
//                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
//                    myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
//                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
//                    myReader = myCommand.ExecuteReader();
//                    table.Load(myReader);
//                    myReader.Close();
//                    myCon.Close();
//                }
//            }
//            return new JsonResult("Updated successfully");
//        }

//        [HttpDelete("{id}")]
//        public ObjectResult Delete(int id)
//        {
//            //user access
//            string query = @"
//                            delete dbo.Employee
//                            where EmployeeId = @EmployeeId
//                            ";
//            DataTable table = new DataTable();
//            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
//            SqlDataReader myReader;
//            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
//            {
//                myCon.Open();
//                using (SqlCommand myCommand = new SqlCommand(query, myCon))
//                {
//                    myCommand.Parameters.AddWithValue("@EmployeeId", id);
//                    myReader = myCommand.ExecuteReader();
//                    table.Load(myReader);
//                    myReader.Close();
//                    myCon.Close();
//                }
//            }
//            return Ok(new JsonResult("Deleted successfully"));
//        }

//        [Route("SaveFile")]
//        [HttpPost]
//        public JsonResult SaveFile()
//        {
//            try
//            {
//                var httpRequest = Request.Form;
//                var postedFile = httpRequest.Files[0];
//                string Filename = postedFile.FileName;
//                var physicalPath = _env.ContentRootPath + "/Photos/" + Filename;
//                using (var stream = new FileStream(physicalPath, FileMode.Create))
//                {
//                    postedFile.CopyTo(stream);
//                }
//                return new JsonResult(Filename);
//            }
//            catch(Exception)
//            {
//                return new JsonResult("Anonymous.png");
//            }
//        }

//    }
//}


