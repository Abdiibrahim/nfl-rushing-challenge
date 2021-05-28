using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing_api.Helpers
{
    /// <summary>
    /// Constants used in CsvHelper
    /// </summary>
    public class CsvConstants
    {
        /// <summary>
        /// Column headers for csv file
        /// </summary>
        public class CsvHeaders
        {
            public const string Player = "Player";
            public const string Team = "Team";
            public const string Position = "Position";
            public const string RushingAttempts = "Attempts";
            public const string RushingAttemptsPerGame = "Attempts/G";
            public const string TotalRushingYards = "Total Yards";
            public const string AverageRushingYardsPerAttempt = "Average Yards/Attempt";
            public const string RushingYardsPerGame = "Yards/G";
            public const string RushingTouchdowns = "Touchdowns";
            public const string LongestRush = "Longest Rush";
            public const string RushingFirstDowns = "First Downs";
            public const string RushingFirstDownPercentage = "First Down %";
            public const string RushingTwentyPlusYards = "20+ Yards";
            public const string RushingFortyPlusYards = "40+ Yards";
            public const string RushingFumbles = "Fumbles";
        }
    }
}
