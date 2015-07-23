/*
    You are given a MS SQL Server database "Ads" holding advertisements, organized by categories, towns and users, available as SQL script.
    Using Entity Framework write a SQL query to select all ads from the database and later print their title, status, category, town and user. 
    Do not use Include(…) for the relationships of the Ads. 
    Check how many SQL commands are executed with the SQL ExpressProfiler (or a similar tool).
    Add Include(…) to select statuses, categories, towns and users along with all ads. 
    Compare the number of executed SQL statements and the performance before and after adding Include(…).
    For this problem you will have to submit the two versions of the program: 
    the one with Include(…) and the one without Include(…). You will also need to fill the following table and submit it as a .txt file:
	                            
                            No Include(…)	With Include(…)
    Number of SQL statements		

    You can optionally submit two screenshots of the SQL profiler tool – one for each case (with and without Include(…)).
    Here is a tool which can create text tables: http://ozh.github.io/ascii-tables/.
*/

namespace Problem01
{

    using System;
    using System.Data.Entity;
    using System.Linq;

    class RelatedTablesData
    {
        private static void RunQuery(string runParameter = null)
        {
            using (var db = new AdsEntities())
            {
                if (runParameter != null && runParameter.ToLower() == "include")
                {
                    // WITH INCLUDE
                    foreach (var ad in db.Ads
                        .Include(a => a.AdStatus).Include(a => a.Category).Include(a => a.AspNetUser).Include(a => a.Town))
                    {
                        Console.WriteLine("Title: {0}\r\nStatus: {1}\r\nCategory: {2}\r\nTown: {3}\r\nUser: {4}\r\n",
                            ad.Title,
                            ad.AdStatus.Status,
                            ad.CategoryId == null ? null : ad.Category.Name,
                            ad.TownId == null ? null : ad.Town.Name,
                            ad.AspNetUser.Name);
                    }
                }
                else if (runParameter == null || runParameter.ToLower() == "notInclude")
                {
                    // WITHOUT INCLUDE
                    foreach (var ad in db.Ads)
                    {
                        Console.WriteLine("Title: {0}\r\nStatus: {1}\r\nCategory: {2}\r\nTown: {3}\r\nUser: {4}\r\n",
                            ad.Title, ad.AdStatus.Status, ad.Category, ad.Town, ad.AspNetUser.Name);
                    }
                }
                else
                {
                    // INVALID RUN PARAMETER
                    throw new ArgumentException("Unknown run parameter.");
                }
            }
        }

        static void Main()
        {
            using (var db = new AdsEntities())
            {
                // QUERY WITHOUT INCLUDE
                RunQuery();

                // UNCOMENT TO USE (QUERY WITH INCLUDE)
                RunQuery("include");
            }
        }
    }
}
