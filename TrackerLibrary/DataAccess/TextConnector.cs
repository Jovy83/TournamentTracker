using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess.TextHelpers;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess {
    public class TextConnector : IDataConnection {

        // use pascal case for const instead of camel case
        private const string PrizesFile = "PrizeModels.csv";

        // TODO - Wire up the CreatePrize for text files
        public PrizeModel CreatePrize(PrizeModel model) {

            // Load the text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModel();

            // Find the max ID 
            int currentId = 1; // if the file does not exist yet, it means we're inserting the very first Prize entry. so it should have an Id of 1

            if (prizes.Count > 0) {
                // only increment currentId if there's already an existing file with records so that the program does not crash
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // Convert the prizes to List<string>
            // Save the List<String> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
