using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using nfl_rushing_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nfl_rushing_api.Models
{
    /// <summary>
    /// Rushing item model class. Contain tags for json properties and csv headers
    /// </summary>
    public class RushingItem
    {
        [JsonProperty("Player")]
        [Name(CsvConstants.CsvHeaders.Player)]
        public string Player { get; set; }

        [JsonProperty("Team")]
        [Name(CsvConstants.CsvHeaders.Team)]
        public string Team { get; set; }
        
        [JsonProperty("Pos")]
        [Name(CsvConstants.CsvHeaders.Position)]
        public string Position { get; set; }
        
        [JsonProperty("Att")]
        [Name(CsvConstants.CsvHeaders.RushingAttempts)]
        public int RushingAttempts { get; set; }
        
        [JsonProperty("Att/G")]
        [Name(CsvConstants.CsvHeaders.RushingAttemptsPerGame)]
        public double RushingAttemptsPerGame { get; set; }

        [JsonProperty("Yds")]
        [JsonConverter(typeof(StringToInt64Converter))]
        [Name(CsvConstants.CsvHeaders.TotalRushingYards)]
        public long TotalRushingYards { get; set; }

        [JsonProperty("Avg")]
        [Name(CsvConstants.CsvHeaders.AverageRushingYardsPerAttempt)]
        public double AverageRushingYardsPerAttempt { get; set; }

        [JsonProperty("Yds/G")]
        [Name(CsvConstants.CsvHeaders.RushingYardsPerGame)]
        public double RushingYardsPerGame { get; set; }

        [JsonProperty("TD")]
        [Name(CsvConstants.CsvHeaders.RushingTouchdowns)]
        public int RushingTouchdowns { get; set; }

        [JsonProperty("Lng")]
        [Name(CsvConstants.CsvHeaders.LongestRush)]
        public string LongestRush { get; set; }

        [JsonProperty("1st")]
        [Name(CsvConstants.CsvHeaders.RushingFirstDowns)]
        public int RushingFirstDowns { get; set; }

        [JsonProperty("1st%")]
        [Name(CsvConstants.CsvHeaders.RushingFirstDownPercentage)]
        public double RushingFirstDownPercentage { get; set; }

        [JsonProperty("20+")]
        [Name(CsvConstants.CsvHeaders.RushingTwentyPlusYards)]
        public int RushingTwentyPlusYards { get; set; }

        [JsonProperty("40+")]
        [Name(CsvConstants.CsvHeaders.RushingFortyPlusYards)]
        public int RushingFortyPlusYards { get; set; }

        [JsonProperty("FUM")]
        [Name(CsvConstants.CsvHeaders.RushingFumbles)]
        public double RushingFumbles { get; set; }

        public int LongestRushInt => LongestRush.Contains("T") ? int.Parse(LongestRush.Substring(0, LongestRush.Length - 1)) : int.Parse(LongestRush);
    }
}
