/*
    Using Entity Framework select all ads from the database, then invoke ToList(), 
    then filter the categories whose status is Published; 
    then select the ad title, category and town, then invoke ToList() 
    again and finally order the ads by publish date. 
    Rewrite the same query in a more optimized way and compare the performance.

    Compare the execution time of the two programs. 
    Hint: use the System.Diagnostics.Stopwatch class. 
    You can see a tutorial on how to use it here. 
    Run each program 10 times and write the average performance time in the following table:

                    Run 1	Run 2	…	Run 10	Average time
    Non-optimized					
    Optimized					

    For this problem you will have to submit the two versions of the program: the slow version and the optimized version.
    WARNING: You may see that the second, third, etc.
    runs are much faster than the first one. 
    This is because SQL Server caches the executed queries. 
    To make tests valid, in the beginning of your program, send the following native SQL query:
    CHECKPOINT; DBCC DROPCLEANBUFFERS;
    For this problem you will have to submit the two versions of the program: 
    the slow version and the optimized version, and the table above.
*/


namespace Problem02
{
    using Problem01;
    using System.Linq;
    using System;
    using System.Diagnostics;

    class PlayWithToList
    {
        /// <summary>
        ///     Run SQL Query and print runtime stats 
        /// </summary>
        /// <param name="queryMode">("Optimized") or ("NotOptimized")</param>
        /// <param name="queryCount">int (how much times to run the query)</param>
        private static void RunQuery(string queryMode, int queryCount)
        {
            using (var db = new AdsEntities())
            {
                // CLEAN BUFFERS
                db.Database.ExecuteSqlCommand("CHECKPOINT;");
                db.Database.ExecuteSqlCommand(" DBCC DROPCLEANBUFFERS;");

                // INIT STOPWATCH
                var sw = new Stopwatch();
                sw.Start();

                // INIT TOTAL TIME
                var totalTime = new TimeSpan();


                // RUN QUERY NOT OPTIMIZED MODE
                if (queryMode == null || queryMode.ToLower() == "notoptimized")
                {
                    Console.WriteLine("Not optimized mode:\r\n");

                    for (int i = 0; i < queryCount; i++)
                    {
                        db.Ads.ToList().Where(a => a.AdStatus.Status == "Published").Select(a => new
                        {
                            a.Title,
                            Category = a.CategoryId == null ? null : a.Category.Name,
                            Town = a.TownId == null ? null : a.Town.Name,
                            a.Date
                        }).ToList().OrderBy(a => a.Date);

                        // PRINT CURRENT STOPWATCH ELAPSED TIME
                        Console.WriteLine("Elapsed: {0}", sw.Elapsed);

                        // ADD CURRENT STOPWATCH ELAPSED TIME TO TOTAL TIME
                        totalTime = totalTime.Add(sw.Elapsed);

                        // RESET STOP WATCH
                        sw.Restart();
                    }
                }
                // RUN QUERY OPTIMIZED MODE
                else if (queryMode.ToLower() == "optimized")
                {
                    Console.WriteLine("Optimized mode:\r\n");

                    for (int i = 0; i < queryCount; i++)
                    {
                        db.Ads.Where(a => a.AdStatus.Status == "Published").Select(a => new
                        {
                            a.Title,
                            Category = a.Category.Name,
                            Town = a.Town.Name,
                            a.Date,
                        }).OrderBy(a => a.Date);

                        // PRINT CURRENT STOPWATCH ELAPSED TIME
                        Console.WriteLine("Elapsed: {0}", sw.Elapsed);

                        // ADD CURRENT STOPWATCH ELAPSED TIME TO TOTAL TIME
                        totalTime = totalTime.Add(sw.Elapsed);

                        // RESET STOP WATCH
                        sw.Restart();
                    }
                }
                // ELSE THROW EXCEPTION
                else
                {
                    throw new ArgumentException(String.Format("Unknown parameter {0}.", queryMode));
                }

                // AVERAGE ELAPSED TIME
                Console.WriteLine("\r\nAverage time: {0}\r\n", totalTime.TotalSeconds / queryCount);
                Console.WriteLine("-------------------------------");
                sw.Stop();
            }
        }

        static void Main()
        {
            RunQuery("Optimized", 10);
            RunQuery("NotOptimized", 10);
        }
    }
}
