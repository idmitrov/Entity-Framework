namespace FootballDb
{
    using System.Linq;
    using System;
    using System.Xml.Linq;
    using System.IO;
    using System.Web.Script.Serialization;
    using System.Xml.XPath;

    class Client
    {
        static void Main()
        {
            using (var db = new FootballEntities())
            {
                // Prboel 01
                foreach (var team in db.Teams)
                {
                    Console.WriteLine(team.TeamName);
                }

                // Poblem 02
                var leaguesQuery = db.Leagues.OrderBy(l => l.LeagueName).Select(l => new
                {
                    leagueName = l.LeagueName,
                    teams = l.Teams.OrderBy(t => t.TeamName).Select(t => t.TeamName)
                }).ToList();

                var serializer = new JavaScriptSerializer();
                var leaguesToJson = serializer.Serialize(leaguesQuery);

                File.WriteAllText("../../leagues-and-teams.json", leaguesToJson);

                // Problem 03
                var internationalMatches = db.InternationalMatches
                    .OrderBy(m => m.MatchDate)
                    .ThenBy(m => m.HomeCountry.CountryName)
                    .ThenBy(m => m.AwayCountry.CountryName)
                    .Select(m => new
                    {
                        countryCodeHomeTeam = m.HomeCountryCode,
                        countryCodeAwayTeam = m.AwayCountryCode,
                        countryNameHomeTeam = m.HomeCountry.CountryName,
                        countryNameAwayTeam = m.AwayCountry.CountryName,
                        matchPlayedOnDate = m.MatchDate,
                        matchLeagueName = m.League.LeagueName,
                        matchHomeTeamScore = m.HomeGoals,
                        matchAwayTeamScore = m.AwayGoals
                    });

                var allMatches = new XElement("matches");

                foreach (var internationalMatch in internationalMatches)
                {
                    // CURRENT MATCH
                    var currentMatch = new XElement
                    (
                        "match",
                        new XElement
                        (
                            "home-country",
                            new XAttribute("code", internationalMatch.countryCodeHomeTeam), internationalMatch.countryNameHomeTeam
                        ),
                        new XElement
                        (
                            "away-country",
                            new XAttribute("code", internationalMatch.countryCodeAwayTeam), internationalMatch.countryNameAwayTeam
                        )
                    );

                    // CURRENT MATCH LEAGUE NAME
                    if (internationalMatch.matchLeagueName != null)
                    {
                        currentMatch.Add(new XElement("league", internationalMatch.matchLeagueName));
                    }

                    // CURRENT MATCH SCORES
                    if (internationalMatch.matchHomeTeamScore != null && internationalMatch.matchAwayTeamScore != null)
                    {
                        currentMatch.Add
                        (
                            new XElement
                            (
                                "score",
                                string.Format("{0}-{1}", internationalMatch.matchHomeTeamScore, internationalMatch.matchAwayTeamScore)
                            )
                        );
                    }

                    // CURRENT MATCH PLAYED DATE
                    if (internationalMatch.matchPlayedOnDate != null)
                    {
                        DateTime dateTime;
                        DateTime.TryParse(internationalMatch.matchPlayedOnDate.ToString(), out dateTime);

                        var dateFormat = dateTime.TimeOfDay.TotalSeconds == 0.0D ? "date" : "date-time";
                        var matchDate = dateFormat == "date-time"
                            ? string.Format("{0:dd-MMM-yyyy hh:mm}", internationalMatch.matchPlayedOnDate)
                            : string.Format("{0:dd-MMM-yyyy}", internationalMatch.matchPlayedOnDate);

                        currentMatch.Add(new XAttribute(dateFormat, matchDate));
                    }

                    // PUT CURRENT MATCH TO ALL MATCHES
                    allMatches.Add(currentMatch);
                }

                // OUTPUT ALL MATCHES TO XML FILE
                allMatches.Save("../../international-matches.xml");

                // Problem 04
                var xmlDocument = XDocument.Load("../../leagues-and-teams.xml");
                var leagues = xmlDocument.Descendants("league");
                var processId = 1;

                foreach (var league in leagues)
                {

                    Console.WriteLine("Processing league #{0} ...", processId++);
                    var leagueName = league.Value;
                    var currentLeague = db.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);

                    if (currentLeague != null)
                    {
                        // LEAGUE ALREADY EXISTS
                        Console.WriteLine("Existing league: {0}", leagueName);
                    }
                    else if (!string.IsNullOrWhiteSpace(leagueName))
                    {
                        currentLeague = new League()
                        {
                            LeagueName = leagueName
                        };

                        db.Leagues.Add(currentLeague);


                        db.SaveChanges();

                        // LEAGUE CREATED
                        Console.WriteLine("Created league: {0}", leagueName);
                    }

                    foreach (var team in league.Descendants("team"))
                    {
                        var teamName = team.Attribute("name").Value;
                        var countryName = team.Attribute("country")?.Value;
                        var currentTeam = db.Teams
                            .FirstOrDefault(t => t.TeamName == teamName && t.Country.CountryName == countryName);

                        if (currentTeam != null)
                        {
                            // TEAM ALREADY EXIST
                            Console.WriteLine("Existing team: {0} ({1})", teamName, countryName ?? "no cuntry");
                        }
                        else
                        {
                            // CREATE CURRENT TEAM
                            currentTeam = new Team()
                            {
                                TeamName = teamName,
                                Country = db.Countries.FirstOrDefault(c => c.CountryName == countryName)
                            };

                            // ADD CURRENT TEAM TO TEAMS
                            db.Teams.Add(currentTeam);

                            Console.WriteLine("Created team: {0} ({1})", teamName, countryName ?? "no country");

                            if (currentLeague != null)
                            {
                                if (currentLeague.Teams.Contains(currentTeam))
                                {
                                    // CURRENT TEAM EXIST IN CURRENT LEAGUE
                                    Console.WriteLine("Existing team in league: {0} belongs to {1}", teamName, leagueName);
                                }
                                else
                                {
                                    // ADDED CURRENT TEAM TO CURRENT LEAGUE
                                    currentLeague.Teams.Add(currentTeam);
                                    Console.WriteLine("Added team to league: {0} to league {1}", teamName, leagueName);
                                }
                            }

                            db.SaveChanges();
                        }
                    }
                }

                // Problem 5
                var document = XDocument.Load("../../generate-matches.xml");

                //Console.WriteLine(document);

                var generateData = document.Descendants("generate");

                // IF GENERATE DATA EXIST
                var generateEntries = generateData as XElement[] ?? generateData.ToArray();
                if (generateEntries.Any())
                {
                    var rand = new Random();
                    var requestId = 1;

                    foreach (var generateEntry in generateEntries)
                    {
                        Console.WriteLine("Processing request #{0} ...", requestId++);

                        var generateCount = int.Parse(generateEntry.Attribute("generate-count")?.Value ?? "10");
                        var maxGoals = int.Parse(generateEntry.Attribute("max-goals")?.Value ?? "5");
                        var leagueName = generateEntry.XPathSelectElement("league")?.Value ?? "no league";
                        var startDate = DateTime.Parse(generateEntry.XPathSelectElement("start-date")?.Value ?? "1-Jan-2000");
                        var endDate = DateTime.Parse(generateEntry.XPathSelectElement("end-date")?.Value ?? "31-Dec-2015");

                        int startDateYear = startDate.Year,
                            startDateMonth = startDate.Month,
                            startDateDay = startDate.Day,
                            endDateYear = endDate.Year,
                            endDateMonth = endDate.Month,
                            endDateDay = endDate.Day;

                        for (var i = 0; i < generateCount; i++)
                        {
                            int randomDateYear = rand.Next(startDateYear, endDateYear + 1),
                                randomDateMonth = rand.Next(startDateMonth, endDateMonth + 1),
                                randomDateDay = rand.Next(startDateDay, endDateDay);

                            // RANDOM TEAMS
                            string randomHomeTeam,
                                   randomAwayTeam;

                            var leagueTeams = db.Leagues.FirstOrDefault(l => l.LeagueName == leagueName)
                                   ?.Teams.Select(t => t.TeamName).ToList();

                            if (leagueTeams != null)
                            {
                                int randomHomeTeamId, randomAwayTeamId;

                                while (true)
                                {
                                    randomHomeTeamId = rand.Next(0, leagueTeams.Count);
                                    randomAwayTeamId = rand.Next(0, leagueTeams.Count);

                                    if (randomHomeTeamId != randomAwayTeamId) break;
                                }

                                randomHomeTeam = leagueTeams[randomHomeTeamId];
                                randomAwayTeam = leagueTeams[randomAwayTeamId];
                            }
                            else
                            {
                                var allTeams = db.Teams.Select(t => t.TeamName).ToList();

                                int randomHomeTeamId, randomAwayTeamId;

                                while (true)
                                {
                                    randomHomeTeamId = rand.Next(0, allTeams.Count);
                                    randomAwayTeamId = rand.Next(0, allTeams.Count);

                                    if (randomHomeTeamId != randomAwayTeamId) break;
                                }

                                randomHomeTeam = allTeams[randomHomeTeamId];
                                randomAwayTeam = allTeams[randomAwayTeamId];
                            }

                            // RANDOM DATE
                            DateTime randomDate = new DateTime(randomDateYear, randomDateMonth, randomDateDay);
                            var randomDateString = string.Format("{0:dd-MMM-yyyy}", randomDate);

                            // RANDOM SCORES
                            var homeTeamScore = rand.Next(0, maxGoals);
                            var awayTeamScore = rand.Next(0, maxGoals);

                            db.TeamMatches.Add(new TeamMatch()
                            {
                                HomeTeam = db.Teams.FirstOrDefault(t => t.TeamName == randomHomeTeam),
                                HomeTeamId = db.Teams.FirstOrDefault(t => t.TeamName == randomHomeTeam)?.Id ?? 0,
                                AwayTeam = db.Teams.FirstOrDefault(t => t.TeamName == randomAwayTeam),
                                AwayTeamId = db.Teams.FirstOrDefault(t => t.TeamName == randomAwayTeam)?.Id ?? 0,
                                HomeGoals = homeTeamScore,
                                AwayGoals = awayTeamScore,
                                MatchDate = randomDate,
                                League = db.Leagues.FirstOrDefault(l => l.LeagueName == leagueName),
                                LeagueId = db.Leagues.FirstOrDefault(l => l.LeagueName == leagueName)?.Id
                            });

                            db.SaveChanges();

                            // RESULT
                            Console.WriteLine("{0}: {1} - {2}: {3}-{4} ({5})",
                                randomDateString, 
                                randomHomeTeam, randomAwayTeam, 
                                homeTeamScore, awayTeamScore, 
                                leagueName);
                        }
                        Console.WriteLine();

                    }
                }
            }
        }
    }
}
