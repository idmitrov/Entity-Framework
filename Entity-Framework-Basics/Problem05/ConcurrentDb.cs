/*
    Problem 5.	Concurrent Database Changes with EF

    Open two different data contexts and perform concurrent changes on the same records in some database table.

    1.	Open the first context and make changes to a column
    2.	Open the second context and make changes to a column
    3.	Consecutively call SaveChanges() on both contexts

    What will happen at SaveChanges()?

    Go to the .edmx diagram editor and set property [Concurrency Mode] of the column you are trying to modify to Fixed.
    Run the code again and see if there are any differences.
*/

namespace Problem05
{
    class ConcurrentDb
    {
        static void Main()
        {
            // FIRST DB
            var firstDb = new SoftUniEntities();
            // EMPLOYEE
            var firstDbEmployee = firstDb.Employees.Find(1);
            firstDbEmployee.FirstName = "Gosho";

            // SECOND DB
            var secondDb = new SoftUniEntities();
            // EMPLOYEE
            var secondDbEmployee = secondDb.Employees.Find(1);
            secondDbEmployee.FirstName = "Vanka";

            // SAVE BOTH
            firstDb.SaveChanges();
            secondDb.SaveChanges();
        }
    }
}
