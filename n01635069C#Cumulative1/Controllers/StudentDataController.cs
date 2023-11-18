using MySql.Data.MySqlClient;
using n01635069C_Cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace n01635069C_Cumulative1.Controllers
{
    public class StudentDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        //This controller will access the students table of the school database
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>
        /// GET api/StudentData/ListStudents
        /// </example>
        /// <returns>
        /// A list of students
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/ListStudent/{StudentSearchKey}")]
        public IEnumerable<Student> ListStudents(string StudentSearchKey = null)
        {
            //Create instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open connection between database and web server
            Conn.Open();

            //Create a new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or  lower(concat(studentfname, ' ', studentlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + StudentSearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student Names
            List<Student> Students = new List<Student>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                
                string StudentFName = ResultSet["studentfname"].ToString();
                string StudentLName = ResultSet["studentlname"].ToString();
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                DateTime EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                Student NewStudent = new Student();
                NewStudent.studentid = StudentId;
                NewStudent.studentfname = StudentFName;
                NewStudent.studentlname = StudentLName;
                NewStudent.enroldate = EnrolDate;

                Students.Add(NewStudent);
            }

            //close connection between database and web server
            Conn.Close();

            //return final list of students names
            return Students;

        }

        [HttpGet]
        [Route("api/StudentData/FindStudent/{StudentId}")]

        public Student FindStudent(int StudentId)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();
            //open connection
            Conn.Open();
            //create new query
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL query & command
            cmd.CommandText = "Select * from students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", StudentId);
            cmd.Prepare();
            //gather result set 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Student SelectedStudent = new Student();
            while (ResultSet.Read())
            {
                SelectedStudent.studentid = Convert.ToInt32(ResultSet["studentid"]);
                SelectedStudent.studentfname = ResultSet["studentfname"].ToString();
                SelectedStudent.studentlname = ResultSet["studentlname"].ToString();
                SelectedStudent.enroldate = Convert.ToDateTime(ResultSet["enroldate"]);
            }

            //close connection
            Conn.Close();

            return SelectedStudent;

        }

    }
}
