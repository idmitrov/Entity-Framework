namespace Problem02
{
    using System;
    using System.Globalization;
    using System.Linq;

    public static class Dao
    {

        // ADD
        public static void Add(Employee employee)
        {
            using (var db = new SoftUniEntities())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                Console.WriteLine("Employee added successiful.");
            }
        }

        // FIND BY KEY
        public static Employee FindByKey(object key)
        {
            using (var db = new SoftUniEntities())
            {
                var employee = db.Employees.Find(key);

                if (employee == null)
                {
                    throw new ArgumentException(
                        String.Format("\r\n\r\nEmployee with ID: {0}, was not found.\r\n\r\n", key));
                }

                return employee;
            }
        }

        // MODIFY
        public static void Modify(int id, string propertyToUpdate, string updateValue)
        {
            using (var db = new SoftUniEntities())
            {
                var employee = db.Employees.Find(id);

                try
                {
                    employee.GetType().GetProperty(propertyToUpdate).SetValue(employee, updateValue);
                    db.SaveChanges();
                    Console.WriteLine("Employee with ID: {0} -> {1} has changed to: {2}",
                        id, propertyToUpdate, updateValue);
                }
                catch (NullReferenceException)
                {
                    Console.Error.WriteLine("Unknown property/id parameter.");
                }
            }
        }

        // DELETE
        public static void Delete(int id)
        {
            using (var db = new SoftUniEntities())
            {
                var employeeToDelete = db.Employees.Find(id);

                if (employeeToDelete != null)
                {
                    db.Employees.Remove(employeeToDelete);
                    db.SaveChanges();
                    Console.WriteLine("Employee with ID: {0} removed.", id);
                }
                else
                {
                    Console.WriteLine("Employee with ID {0} was not found.", id);
                }
            }
        }
    }
}
