using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyProject3.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject3.Controllers
{
    // GET: /<controller>/
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : Controller
    {


        
            // GET: /<controller>/
            private readonly IConfiguration _configuration;

            public EmployeeController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpGet]

            public JsonResult Get()
            {
                string query = @"
                       select EmployeeId, EmployeeName, Department,
                       convert(varchar(10),DateOfJoining,120) as DataOfJoining,PhotoFileName from dbo.Employee";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }

                }

                return new JsonResult(table);
            }


            [HttpPost]

            public JsonResult Post(Employee emp)
            {
                string query = @"insert into dbo.Employee
                        (EmployeeName,Department,DateOfJoining,PhotoFileName)
                        values
                        ('" + emp.EmployeeName + @"',
                        '"" + emp.Department + @""',
                        '"" + emp.DateOfJoining + @""'
                        '"" + emp.PhotoFileName + @""')";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }

                }

                return new JsonResult("Added Sucessfully");
            }


            [HttpPut]

            public JsonResult Put(Employee emp)
            {
                string query = @"update dbo.Employee set
                            EmployeeName =  '" + emp.EmployeeName + @"',
                            Department =  '"" + emp.Department + @""',
                            DateOfJoining =  '"" + emp.DateOfJoining + @""'
                            where EmployeeId = " + emp.EmployeeId + @"";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }

                }

                return new JsonResult("Updated Sucessfully");
            }


            [HttpDelete("{id}")]

            public JsonResult Delete(int id)
            {
                string query = @"delete from  dbo.Employee 
                            where EmployeeId = " + id + @"";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }

                }

                return new JsonResult("Delete Sucessfully");
            }



    }

    
}

