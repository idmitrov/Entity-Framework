/*
    Problem 4.	Native SQL Query

    Find all employees who have projects with start date in the year 2002. 
    Select only their first name. 
    Solve this task by using both LINQ query and native SQL query through the context. 
    Measure the difference in performance in both cases with a Stopwatch.
    Comment out any printing so the measurements can be most accurate.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Problem04
{
    class NativeSqlQuery
    {
        static void Main()
        {
            using (var db = new SoftUniEntities())
            {
                // LINQ WAY QUERY
                var employeesLinq = db.Employees
                    .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                    .Select(e => new
                    {
                        FirstName = e.FirstName
                    });

                // NATIVE WAY QUERY
                string nativeQuery = 
                    "SELECT e.FirstName FROM Employees e " +
                    "WHERE EXISTS(SELECT p.ProjectID FROM Projects p " +
                    "LEFT JOIN EmployeesProjects ep " +
                    "ON p.ProjectID = ep.ProjectID " +
                    "LEFT JOIN Employees em " +
                    "ON ep.EmployeeID = em.EmployeeID " +
                    "WHERE em.EmployeeID = e.EmployeeID " +
                    "AND YEAR(p.StartDate) = 2002)";

                // MEASUREMENT
                var sw = new Stopwatch();
                sw.Start();

                // LINQ WAY STATS
                foreach (var employee in employeesLinq)
                {
                    Console.WriteLine(employee.FirstName);
                }

                Console.WriteLine("LINQ WAY STATS: ELPASED : {0}", sw.Elapsed);

                // NATIVE WAY STATS
                sw.Reset();
                sw.Start();

                var employeesNative = db.Database.SqlQuery<string>(nativeQuery).ToList();
                foreach (var employee in employeesNative)
                {
                    Console.WriteLine(employee);
                }

                Console.WriteLine("NATIVE WAY STATS: ELPASED : {0}", sw.Elapsed);
            }
        }
    }
}
