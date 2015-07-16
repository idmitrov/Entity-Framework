/*
    Problem 1.	DbContext for the SoftUni Database

    Your task is to create a DbContext for the SoftUni database (provided in the course page)
    using the Visual Studio Entity Data Model Wizard for Database First. 
    Map all database tables. Exclude any stored procedures.
*/

namespace Problem01
{
    class DbContext
    {
        static void Main(string[] args)
        {
            var softUniDb = new SoftUniEntities();
        }
    }
}
