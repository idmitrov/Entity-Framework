using System.Collections.Generic;
using System.Data.Entity;

namespace StudentSystem.ConsoleClient
{
    using System;
    using Data;
    using Models;
    using System.Linq;

    class Client
    {
        static void Main()
        {
            using (var db = new StudentSystemDb())
            {
                //// INIT DB IF IS NOT CREATED
                //var studentsCount = db.Students.Count();
                //Console.WriteLine(studentsCount);

                //// CREATE NEW STUDENT
                //var peshoStudent = new Student()
                //{
                //    Name = "Pesho",
                //    RegistredOn = DateTime.Now
                //};

                //// ADD STUDENT TO DATABASE
                //db.Students.Add(peshoStudent);

                //// SAVE DATABASE CHANGES
                //db.SaveChanges();

                /* 
                    PROBLEM 03
                        1. Lists all students and their homework submissions. 
                        Select only their names and for each homework - content and content-type.
                */

                ///// UNCOMMENT TO USE (RESULT)
                //var studentsHomeworks = db.Homeworks.Select(h => new
                //{
                //    h.Student.Name,
                //    h.Content,
                //    h.Type
                //});

                //foreach (var studentsHomework in studentsHomeworks)
                //{
                //    Console.WriteLine("Name: {0}\r\nContent: {1}\r\nType: {2}", 
                //        studentsHomework.Name, studentsHomework.Content, studentsHomework.Type);
                //}

                /*
                    2.	List all courses with their corresponding resources. 
                    Select the course name and description and everything for each resource. 
                    Order the courses by start date (ascending), then by end date (descending).
                */

                //// UNCOMENT TO USE (RESULT)
                //    var coursesWithResources = db.Courses.Select(c => new
                //    {
                //        CourseName = c.Name,
                //        CourseDescription = c.Description,
                //        CourseResources = c.Resources.Select(r => new
                //        {
                //            ResourceName = r.Name,
                //            ResourceType = r.Type,
                //            ResourceUrl = r.Url
                //        }),
                //        StartDate = c.StartDate,
                //        EndDate = c.EndDate
                //    }).OrderBy(c => c.StartDate).ThenByDescending(c => c.EndDate);

                //    foreach (var coursesWithResource in coursesWithResources)
                //    {
                //        Console.WriteLine("Course: {0}\r\nDescription: {1}\r\nResources:",
                //            coursesWithResource.CourseName,
                //            coursesWithResource.CourseDescription);
                //        foreach (var courseResource in coursesWithResource.CourseResources)
                //        {
                //            Console.WriteLine("Resource: {0}\r\nType: {1}\r\nURL: {2}",
                //                courseResource.ResourceName, courseResource.ResourceType, courseResource.ResourceUrl);
                //        }
                //        Console.WriteLine();
                //    }

                /*
                    3.	List all courses with more than 5 resources. Order them by resources count (descending),
                    then by start date (descending). Select only the course name and the resource count.
                */

                //// UNCOMENT TO USE (RESULT)
                //var courses = db.Courses
                //    .Where(c => c.Resources.Count > 5)
                //    .Select(c => new
                //    {
                //        Name = c.Name,
                //        ResourceCount = c.Resources.Count,
                //        StartDate = c.StartDate
                //    }).OrderByDescending(c => c.ResourceCount).ThenByDescending(c => c.StartDate);

                //foreach (var course in courses)
                //{
                //    Console.WriteLine("Course: {0} -> Resources Count: {1}", course.Name, course.ResourceCount);
                //}

                /*
                    4.	List all courses which were active on a given date 
                    (choose the date depending on the data seeded to ensure there are results), 
                    and for each course count the number of students enrolled. 
                    Select the course name, start and end date, course duration 
                    (difference between end and start date) and number of students enrolled. 
                    Order the results by the number of students enrolled (in descending order), 
                    then by duration (descending).
                */

                //// UNCOMENT TO USE (RESULT)
                //var coursesInfo = db.Courses
                //    .Where(c => c.StartDate.Day == DateTime.Now.Day
                //                && c.StartDate.Year == DateTime.Now.Year
                //                && c.StartDate.Month == DateTime.Now.Month)
                //    .Select(c => new
                //    {
                //        CourseName = c.Name,
                //        StartDate = c.StartDate,
                //        EndDate = c.EndDate,
                //        Duration = DbFunctions.DiffHours(c.StartDate, c.EndDate) / 24,
                //        NumberOfStudents = c.Students.Count
                //    }).OrderByDescending(c => c.NumberOfStudents).ThenByDescending(c => c.Duration);

                //foreach (var course in coursesInfo)
                //{
                //    Console.WriteLine("Course: {0}" +
                //                      "\r\nStart Date: {1}" +
                //                      "\r\nEnd Date: {2}" +
                //                      "\r\nDuration: {3}" +
                //                      "\r\nNumber of students: {4}",
                //        course.CourseName, course.StartDate, course.EndDate, course.Duration, course.NumberOfStudents);
                //}

                /*
                    5.	For each student, calculate the number of courses he/she has enrolled in, 
                        the total price of these courses and the average price per course for the student.
                        Select the student name, number of courses, total price and average price. 
                        Order the results by total price (descending), then by number of courses (descending) 
                        and then by the student's name (ascending).
                */

                //// UNCOMENT TO USE (RESULT)
                //var students = db.Students.Select(s => new
                //{
                //    StudentName = s.Name,
                //    NumberOfCourses = s.Courses.Count,
                //    TotalCoursesPrice = s.Courses.Sum(c => c.Price) == null ? 0.0M : s.Courses.Sum(c => c.Price),
                //    AverageCoursesPrice = s.Courses.Average(c => c.Price) == null ? 0.0M : s.Courses.Average(c => c.Price)
                //}).OrderByDescending(c => c.TotalCoursesPrice)
                //    .ThenByDescending(c => c.NumberOfCourses)
                //    .ThenBy(c => c.StudentName);

                //foreach (var student in students)
                //{
                //    Console.WriteLine("Name: {0}\r\nCourses: {1}\r\nTotal Price: {2}\r\nAVG Price: {3}",
                //        student.StudentName, student.NumberOfCourses,
                //        student.TotalCoursesPrice, student.AverageCoursesPrice);
                //}
            }
        }
    }
}
