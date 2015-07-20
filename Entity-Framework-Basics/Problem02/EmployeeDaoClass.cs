﻿/*
    Problem 2.	Employee DAO Class

    Your task is to create a Data Access Object (DAO) class with static methods, 
    which provide functionality for inserting, finding by key, modifying and deleting employees.

        Write a testing class which:
    1.	Inserts an employee
    2.	Prints his/her primary key generated by the DB
    3.	Changes the employee first name and saves it to the database
    4.	Deletes an employee
*/

using System;

namespace Problem02
{
    class EmployeeDaoClass
    {
        static void Main()
        {
            // CREATE NEW EMPLOYEE
            var straxilEmployee = new Employee()
            {
                FirstName = "Straxil",
                LastName = "Straxilev",
                JobTitle = "Engineer",
                DepartmentID = 1,
                HireDate = DateTime.Now,
                Salary = 1000M
            };

            //// UNCOMMENT TO USE
            //// ADD EMPLOYEE TO SoftUni DB
            //Dao.Add(straxilEmployee);

            //// UNCOMMENT TO USE
            //// FIND EMPLOYEE BY KEY
            //var foundEmployee = Dao.FindByKey(300);
            //Console.WriteLine("Employee found: {0} {1}", foundEmployee.FirstName, foundEmployee.LastName);

            //// MODIFY
            //Dao.Modify(303, "FirstName", "Straxil-Updated");

            //// uncomment to use
            //// delete existing employee
            //Dao.Delete(302); //execute with employee id which you want to remove
        }
    }
}
