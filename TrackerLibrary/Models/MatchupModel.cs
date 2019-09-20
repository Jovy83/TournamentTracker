using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models {
    public class MatchupModel {
        public List<MatchupEntryModel> matchupEntries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel TeamModel { get; set; }
        public int MatchupRound { get; set; }
    }
}
