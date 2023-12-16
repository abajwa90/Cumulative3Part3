using MiniviableProject.Models;
using Microsoft.AspNetCore.Cors;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MiniviableProject.Controllers
{
    public class TeachersDataController : ApiController
    {
        
        private SchoolDbContext School = new SchoolDbContext();

        
        [HttpGet]
        [Route("api/GetTeachers/{searchKey?}")]
        public IEnumerable<Teacher> GetTeachers(string searchKey)
        {
            List<Teacher> teachers = new List<Teacher> { };

            
            MySqlConnection Conn = School.AccessDatabase();

            try
            {
                
                Conn.Open();

                
                MySqlCommand cmd = Conn.CreateCommand();

             
                cmd.CommandText = "SELECT * FROM teachers WHERE lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

                cmd.Parameters.AddWithValue("@key", "%" + searchKey + "%");
                cmd.Prepare();

                MySqlDataReader results = cmd.ExecuteReader();

                while (results.Read())
                {
                    
                    int teacherId = Convert.ToInt32(results["teacherid"]);
                    string teacherFirstname = results["teacherfname"].ToString();
                    string teacherLastname = results["teacherlname"].ToString();

                    Teacher newTeacher = new Teacher();
                    newTeacher.id = teacherId;
                    newTeacher.firstName = teacherFirstname;
                    newTeacher.lastName = teacherLastname;

                    teachers.Add(newTeacher);
                }

            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Server Error", ex);
            }
            catch (Exception ex)
            {
 
                Conn.Close();
            }
            
            return teachers;
        }

        [HttpGet]
        [Route("api/GetTeacherDetails/{id}")]
        public Teacher GetTeacherDetails(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Teacher teacher = new Teacher();

            try
            {
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                
                cmd.CommandText = "SELECT * FROM teachers where teacherid = @id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

               
                MySqlDataReader resultSet = cmd.ExecuteReader();

                while (resultSet.Read())
                {
                    int teacherId = Convert.ToInt32(resultSet["teacherid"]);
                    string teacherFirstname = resultSet["teacherfname"].ToString();
                    string teacherLastname = resultSet["teacherlname"].ToString();
                    string employeeNumber = resultSet["employeenumber"].ToString();
                    string hireDate = Convert.ToDateTime(resultSet["hiredate"]).ToString("dd/MM/yyyy");
                    decimal salary = Convert.ToDecimal(resultSet["salary"]);

                    teacher.id = teacherId;
                    teacher.firstName = teacherFirstname;
                    teacher.lastName = teacherLastname;
                    teacher.employeeNumber = employeeNumber;
                    teacher.hireDate = hireDate;
                    teacher.salary = salary;
                }
            }
            catch (MySqlException ex)
            {
             
                Debug.Write(ex);
                throw new ApplicationException("Server Error", ex);
            }
            catch (Exception ex)
            {
                
                Conn.Close();
            }

            return teacher;
        }


        
        [HttpPost]
        [Route("api/addTeacher")]
        public void AddTeacher([FromBody] Teacher newTeacher)
        {

            MySqlConnection Conn = School.AccessDatabase();

            try
            {
                if (!newTeacher.IsValid())
                {
                    throw new ApplicationException("Please provide the TeacherID to update the Teacher.");
                }

                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                cmd.CommandText = "INSERT INTO teachers(teacherFName,teacherLName,salary,employeeNumber,hireDate) Values(@firstName, @lastName, @salary, @employeeNumber, @hireDate);";

                cmd.Parameters.AddWithValue("@firstName", newTeacher.firstName);
                cmd.Parameters.AddWithValue("@lastName", newTeacher.lastName);
                cmd.Parameters.AddWithValue("@employeeNumber", newTeacher.employeeNumber);
                cmd.Parameters.AddWithValue("@salary", newTeacher.salary);
                cmd.Parameters.AddWithValue("@hireDate", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                cmd.Prepare();

               
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                throw new ApplicationException("Server Error", ex);
            }
            catch (Exception ex)
            {
                Conn.Close(); 
            }


        }

        
        [HttpPost]
        [Route("api/deleteTeacher/{teacherId}")]
        public void DeleteTeacher(string teacherId)
        { 
            MySqlConnection Conn = School.AccessDatabase();

            try
            {
                if (teacherId == "")
                {
                    throw new ApplicationException("Please provide the TeacherID to delete the Teacher.");
                }
                
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                cmd.CommandText = "Delete from teachers where teacherid = @teacherId";
                cmd.Parameters.AddWithValue("@teacherId", teacherId);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Server Error", ex);
            }
            catch (Exception ex)
            {
                Conn.Close();
            }

        }

        
        [HttpPost]
        [Route("api/updateTeacher/{teacherId}")]
        public void UpdateTeacher(string teacherId, [FromBody] Teacher newTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();

            try
            {
                if (teacherId == "")
                {
                    throw new ApplicationException("Please provide the TeacherID to update the Teacher.");
                }
                if (!newTeacher.IsValid())
                {
                    throw new ApplicationException("Please fill in all the fields.");
                }

                Conn.Open();


                MySqlCommand cmd = Conn.CreateCommand();


                cmd.CommandText = "UPDATE teachers SET teacherfname=@firstName, teacherlname=@lastName, employeenumber=@employeeNumber, salary=@salary WHERE teacherId=@teacherId";


                cmd.Parameters.AddWithValue("@firstName", newTeacher.firstName);
                cmd.Parameters.AddWithValue("@lastName", newTeacher.lastName);
                cmd.Parameters.AddWithValue("@employeeNumber", newTeacher.employeeNumber);
                cmd.Parameters.AddWithValue("@salary", newTeacher.salary);
                cmd.Parameters.AddWithValue("@teacherId", teacherId);

                cmd.Prepare();

                cmd.ExecuteNonQuery();


                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                throw new ApplicationException("Server Error", ex);
            }
            catch (Exception ex)
            {
                Conn.Close();
            }

        }
    }
}
