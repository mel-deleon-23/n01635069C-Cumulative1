using MySql.Data.MySqlClient;
using n01635069C_Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01635069C_Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will access the teachers table of the school database
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>
        /// GET api/TeacherData/ListTeachers
        /// </example>
        /// <returns>
        /// A list of teachers (first and last names)
        /// </returns>

        //[HttpGet]
        //public IEnumerable<Teacher> ListTeachers()
        //{
            //Create instance of a connection
            //MySqlConnection Conn = School.AccessDatabase();

            //Open connection between database and web server
            //Conn.Open();

            //Create a new query for database
            //MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            //cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            //MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            //List<Teacher> Teachers = new List<Teacher>();

            //Loop Through Each Row the Result Set
            //while (ResultSet.Read())
            //{
                //Access Column info by the DB column name as an index
                //string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                //Add Teacher Name to the list
                //TeacherNames.Add(TeacherName);

                //string TeacherFName = ResultSet["teacherfname"].ToString();
                //string TeacherLName = ResultSet["teacherlname"].ToString();
                //int TeacherId = (int)ResultSet["teacherid"];
                ///int EmployeeNumber = (int)ResultSet["employeenumber"];
                //DateTime HireDate = (DateTime)ResultSet["hiredate"];
                //decimal Salary = (decimal)ResultSet["salary"];
                //int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                //Teacher NewTeacher = new Teacher();
                //NewTeacher.teacherid = TeacherId;
                //NewTeacher.teacherfname = TeacherFName;
                //NewTeacher.teacherlname = TeacherLName;
                ///NewTeacher.employeenumber = EmployeeNumber;
                //NewTeacher.hiredate = HireDate;
                //NewTeacher.salary = Salary;


                //Teachers.Add(NewTeacher);

            //}

            //close connection between database and web server
            //Conn.Close();

            //return final list of teachers names
            //return Teachers;
        //}

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey}")]
        public IEnumerable<Teacher> ListTeachers(string TeacherSearchKey = null)
        {
            //Debug.WriteLine("trying to do an api search for " + TeacherSearchKey);

            //Create instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open connection between database and web server
            Conn.Open();

            //Create a new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or  lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                ///int EmployeeNumber = (int)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];
                //int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFName;
                NewTeacher.teacherlname = TeacherLName;
                ///NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;


                Teachers.Add(NewTeacher);

            }

            //close connection between database and web server
            Conn.Close();

            //return final list of teachers names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int TeacherId)
        {
            Teacher NewTeacher = new Teacher();

            //create connection
            MySqlConnection Conn = School.AccessDatabase();
            //open connection
            Conn.Open();
            //create new query
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL query & command
            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();
            //gather result set 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Teacher SelectedTeacher = new Teacher();
            while (ResultSet.Read())
            {

                int teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherfname = ResultSet["teacherfname"].ToString();
                string teacherlname = ResultSet["teacherlname"].ToString();
                DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.teacherid = TeacherId;

                NewTeacher.teacherfname = ResultSet["teacherfname"].ToString();

                NewTeacher.teacherlname = ResultSet["teacherlname"].ToString();

                NewTeacher.hiredate = Convert.ToDateTime(ResultSet["hiredate"]);

                NewTeacher.salary = Convert.ToDecimal(ResultSet["salary"]);
            }

            //close connection
            //Conn.Close();

            return NewTeacher;

        }

        /// <summary>
        /// Find teacher by input teacher id
        /// </summary>
        /// <param name="TeacherId">teacherid primary key in the database</param>
        /// <returns>
        /// a teacher object
        /// </returns>
        /// <example>
        /// GET api/TeacherData/FindTeacher/5
        /// </example>
        //[HttpGet]
        //[Route("api/TeacherData/FindTeacher/{TeacherId}")]
        //public Teacher FindTeacherName(int TeacherId)
        //{
            //create connection
            //MySqlConnection Conn = School.AccessDatabase();
            //open connection
            //Conn.Open();
            //create new query
            //MySqlCommand cmd = Conn.CreateCommand();
            //SQL query & command
            //cmd.CommandText = "Select * from teachers where teacherid = " + TeacherId;
            //gather result set 
            //MySqlDataReader ResultSet = cmd.ExecuteReader();


            //Teacher SelectedTeacher = new Teacher();
            //while (ResultSet.Read())
            //{
            //    SelectedTeacher.teacherid = Convert.ToInt32(ResultSet["teacherid"]);

            //    SelectedTeacher.teacherfname = ResultSet["teacherfname"].ToString();

            //    SelectedTeacher.teacherlname = ResultSet["teacherlname"].ToString();

            //    SelectedTeacher.hiredate = Convert.ToDateTime(ResultSet["hiredate"]);

            //    SelectedTeacher.salary = Convert.ToDecimal(ResultSet["salary"]);
            //}

            //close connection
            //Conn.Close();

            //return SelectedTeacher;

        //}

    }
}
