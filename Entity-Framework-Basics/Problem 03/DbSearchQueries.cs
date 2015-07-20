namespace Problem_03
{
    using System;
    using System.Linq;

    class DbSearchQueries
    {
        static void Main()
        {
            /*
                Writer the following queries in LINQ:

                1.
                    Find all employees who have projects started in the time period 2001 - 2003 (inclusive).
                    Select each employee's first name, last name and manager name
                    and each of their projects' name, start date, end date.
            
                2.
                    Find all addresses, ordered by the number of employees who live there (descending),
                    then by town name (ascending).
                    Take only the first 10 addresses and select their address text, town name and employee count. 
            
                3.
                    Get an employee by id (e.g. 147). 
                    Select only his/her first name, last name, job title and projects (only their names). 
                    The projects should be ordered by name (ascending).

                4.
                    Find all departments with more than 5 employees. 
                    Order them by employee count (ascending). 
                    Select the department name, manager name and first name, last name, hire date
                    and job title of every employee.
            */

            // 1
            using (var db = new SoftUniEntities())
            {
                var employeesInRange = db.Employees
                    .Where(e => e.Projects
                        .Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                        Projects = e.Projects.Select(p => new
                        {
                            Name = p.Name,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate
                        })
                    });

                //// UNCOMMENT TO USE (RESULT)
                //foreach (var employee in employeesInRange)
                //{
                //    Console.WriteLine("Employee: {0} {1} -> Manager: {2}",
                //        employee.FirstName, employee.LastName, employee.ManagerName);
                //    foreach (var project in employee.Projects)
                //    {
                //        Console.WriteLine("Project Name: {0}\r\nStart date: {1}\r\nEnd date: {2}\r\n",
                //            project.Name, project.StartDate, project.EndDate);
                //    }
                //}

                // 2
                var orderedAddresses = db.Addresses
                    .OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name)
                    .Select(a => new
                    {
                        AddressText = a.AddressText,
                        TownName = a.Town.Name,
                        EmployeesCount = a.Employees.Count
                    }).Take(10);

                //// UNCOMMENT TO USE (RESULT)
                //foreach (var address in orderedAddresses)
                //{
                //    Console.WriteLine("{0}, {1} - {2}", 
                //        address.AddressText, address.TownName, address.EmployeesCount);
                //}

                // 3
                var employeesById = db.Employees
                    .Where(e => e.EmployeeID == 147)
                    .Select(e => new
                    {
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        JobTitle = e.JobTitle,
                        Projects = e.Projects.Select(p => new
                        {
                            ProjectName = p.Name
                        })
                    });

                //// UNCOMMENT TO USE (RESULT)
                //foreach (var employee in employeesById)
                //{
                //    Console.WriteLine("Name: {0} {1}, Job Title: {2}", 
                //        employee.FirstName, employee.LastName, employee.JobTitle);
                //    foreach (var project in employee.Projects)
                //    {
                //        Console.WriteLine("Project name: {0}", project.ProjectName);
                //    }
                //}

                // 4
                var departments = db.Departments
                    .Where(d => d.Employees.Count > 5)
                    .OrderBy(d => d.Employees.Count)
                    .Select(d => new
                    {
                        DepartmentName = d.Name,
                        ManagerName = d.Manager.LastName,
                        Employees = d.Employees.Select(e => new
                        {
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            HireDate = e.HireDate,
                            JobTitle = e.JobTitle
                        })
                    });

                //// UNCOMMENT TO USE (RESULT)
                //foreach (var department in departments)
                //{
                //    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
                //        department.DepartmentName,
                //        department.ManagerName,
                //        department.Employees.Count());
                //}
            }
        }
    }
}
