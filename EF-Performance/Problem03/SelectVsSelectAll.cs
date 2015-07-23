/*
    Write a program to compare the execution speed between these two scenarios:
    •	Select everything from the Ads table and print only the ad title.
    •	Select the ad title from Ads table and print it.
    
    Run the two queries 10 times and write down the average time. Follow all the steps you did for Problem 2.
	    Run 1	Run 2	…	Run 10	Average time
    Non-optimized					
    Optimized					

    For this problem you will have to submit the two versions of the program:
    the slow version and the optimized version, and the table above.
*/


namespace Problem03
{
    using Problem01;
    using System;
    using System.Diagnostics;
    using System.Linq;

    class SelectVsSelectAll
    {
        /// <summary>
        ///     Return query performance result
        /// </summary>
        /// <param name="queryType">("selectCertain") OR ("selectAll")</param>
        /// <param name="queryCount">int (1..n)</param>
        private static void RunQuery(string queryType, int queryCount)
        {
            using (var db = new AdsEntities())
            {
                if (queryType != null && queryCount > 0 &&
                    (queryType.ToLower() == "certain" || queryType.ToLower() == "all"))
                {
                    // CLEAN BUFFERS
                    db.Database.ExecuteSqlCommand("CHECKPOINT;");
                    db.Database.ExecuteSqlCommand(" DBCC DROPCLEANBUFFERS;");

                    // STOPWATCH
                    var sw = new Stopwatch();
                    sw.Start();
                    var totalTime = new TimeSpan();

                    Console.WriteLine("\r\n{0} mode.\r\n",
                        queryType.ToLower() == "certain" ? "Select certain" : "Select all");

                    for (int i = 0; i < queryCount; i++)
                    {
                        // CERTAIN COLUMN
                        if (queryType.ToLower() == "certain")
                        {
                            foreach (var ad in db.Ads.Select(a => a.Title))
                            {
                                var currentAdTitle = ad;
                            }
                        }
                        else
                        // ALL COLUMNS
                        {
                            foreach (var ad in db.Ads)
                            {
                                var currentAdTitle = ad.Title;
                            }
                        }

                        // ADD CURRENT ELAPSED TIME TO TOTAL TIME
                        totalTime = totalTime.Add(sw.Elapsed);

                        // SHOW CURRENT ELAPSED TIME
                        Console.WriteLine(sw.Elapsed);

                        // STOPWATCH RESET
                        sw.Restart();
                    }

                    // AVERAGE ELAPSED TIME
                    Console.WriteLine("\r\nAverage time: {0}", totalTime.TotalSeconds / queryCount);
                    Console.WriteLine("-------------------------------");
                }
            }
        }

        static void Main()
        {
            RunQuery("all", 10);

            RunQuery("certain", 10);
        }
    }
}
