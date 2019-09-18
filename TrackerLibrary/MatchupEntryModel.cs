using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary {
    public class MatchupEntryModel {

        /// <summary>
        /// Represents one team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that this team came 
        /// from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

        /// <summary>
        /// Constructor for the MatchupEntryModel
        /// </summary>
        /// <param name="initialScore">
        /// The team's initial score
        /// </param>
        public MatchupEntryModel(double initialScore) {
            Console.WriteLine();
        }
    }
}
