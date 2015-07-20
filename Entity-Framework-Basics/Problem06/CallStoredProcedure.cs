/*
    Problem 6.	Call a Stored Procedure

    Your task is to create a stored procedure in the SoftUni database for finding all projects for given employee. 
    The procedure should receive first name and last name as arguments. 
    Using EF implement a C# method that calls the stored procedure 
    and returns the result projects' name, description and start date.
*/

namespace Problem06
{
    using System;
    using System.Linq;

    class CallStoredProcedure
    {
        static void Main()
        {
            using (var db = new SoftUniEntities())
            {
                var projectsByEmployee = db.GetProjectsByEmployee("Ruth", "Ellerbrock")
                    .Select(p => new
                    {
                        ProjectName = p.Name,
                        Description = p.Description,
                        StartDate = p.StartDate
                    });

                foreach (var project in projectsByEmployee)
                {
                    Console.WriteLine("{0} - {1}, {2}", 
                        project.ProjectName, project.Description, project.StartDate);
                }
            }
        }
    }
}
