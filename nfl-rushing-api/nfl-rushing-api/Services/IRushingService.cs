using nfl_rushing_api.Helpers;
using nfl_rushing_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace nfl_rushing_api
{
    /// <summary>
    /// Rushing service interface
    /// </summary>
    public interface IRushingService
    {
        void SearchByName(ref IQueryable<RushingItem> rushingItems, string playerName);
        void ApplySort(ref IQueryable<RushingItem> rushingItems, string orderByQueryString);
        string BuildCsv(IQueryable<RushingItem> rushingItems);
    }

    /// <summary>
    /// Rushing service
    /// </summary>
    public class RushingService : IRushingService
    {
        /// <summary>
        /// dependancy injection for service
        /// </summary>
        /// <param name="provider"></param>
        public RushingService(IServiceProvider provider)
        {

        }

        /// <summary>
        /// Searches list for player names containing search term
        /// </summary>
        /// <param name="rushingItems"></param>
        /// <param name="playerName"></param>
        public void SearchByName(ref IQueryable<RushingItem> rushingItems, string playerName)
        {
            if (!rushingItems.Any() || string.IsNullOrWhiteSpace(playerName))
                return;

            rushingItems = rushingItems.Where(i => i.Player.ToLower().Contains(playerName.Trim().ToLower()));
        }

        /// <summary>
        /// Sorts list by specified fields. Sort direction is also specified in field.
        /// Order params are separated by comma.
        /// </summary>
        /// <param name="rushingItems"></param>
        /// <param name="orderByQueryString"></param>
        public void ApplySort(ref IQueryable<RushingItem> rushingItems, string orderByQueryString)
        {
            // return if list or query string  is empty
            if (!rushingItems.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
                return;

            // get properties of query params from string
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(RushingItem).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                // if longest rush use longestrushint (to sort field properly)
                if (propertyFromQueryName.Equals("longestrush", StringComparison.InvariantCultureIgnoreCase))
                    propertyFromQueryName = "longestrushint";

                // continue if property doesn't exist
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;

                // determine sort direction
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
                return;
            
            // order items
            rushingItems = rushingItems.OrderBy(orderQuery);
        }

        /// <summary>
        /// Builds csv using CsvHelper.
        /// </summary>
        /// <param name="rushingItems"></param>
        /// <returns></returns>
        public string BuildCsv(IQueryable<RushingItem> rushingItems)
        {
            try
            {
                // set filepath for csv file
                string fileName = "rushing.csv";
                string pathToSave = Directory.GetCurrentDirectory();
                string fullPath = Path.Combine(pathToSave, fileName);

                // save to csv
                using (var writer = new StreamWriter(fullPath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(rushingItems);
                    writer.Flush();
                }

                // send file to endpoint
                if (!File.Exists(fullPath))
                    throw new Exception("File Not Found.");

                return fullPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
