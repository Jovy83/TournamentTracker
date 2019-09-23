using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers {
    public static class TextConnectorProcessor {
        
        public static string FullFilePath(this string fileName) {
            // with 'this' in the parameter, it becomes an extension method

            // PrizeModels.csv
            // C:\data\TournamentTracker\PrizeModels.csv
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        /// <summary>
        /// Takes in the full file path and load it as a List<string>
        /// </summary>
        /// <param name="file">the complete file path</param>
        /// <returns>A list of string from the file</returns>
        public static List<string> LoadFile(this string file) {

            // this checks if the file exists
            if (!File.Exists(file)) {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines) {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines) {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines) {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines) {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellphoneNumber = cols[4];

                output.Add(p);
            }

            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName) {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models) {
                // we're just converting each PrizeModel to a string that is comma separated
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName) {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models) {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.CellphoneNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
