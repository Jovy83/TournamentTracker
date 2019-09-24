using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess {
    public class SqlConnector : IDataConnection {

        private const string db = "Tournaments";


        /// <summary>
        ///  Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model) {

            // this is the safe way to connect to our database in such a way that we don't leave connections open
            // using keyword = will destroy properly the connection object once it reaches the end of the 'using' curly brace. 
            // this way, we're preventing against memory leaks
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db))) {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output); 
                // Int32 is the standard lower case int in C#. We also define this as output like we did in our stored procedure

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public PersonModel CreatePerson(PersonModel model) {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db))) {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                // Int32 is the standard lower case int in C#. We also define this as output like we did in our stored procedure

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public List<PersonModel> GetPerson_All() {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db))) {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
