using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI {
    public partial class CreateTeamForm : Form {

        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm() {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
        }

        private void CreateSampleData() {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Bill", LastName = "Jones" });
        }

        private void WireUpLists() {

            selectTeamMemberDropdown.DataSource = null;

            selectTeamMemberDropdown.DataSource = availableTeamMembers;
            selectTeamMemberDropdown.DisplayMember = "FullName";

            teamMemberListBox.DataSource = null;

            teamMemberListBox.DataSource = selectedTeamMembers;
            teamMemberListBox.DisplayMember = "FullName";
        }

        private void CreateMemberButton_Click(object sender, EventArgs e) {
            if (ValidateForm()) {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellphoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            } else {
                MessageBox.Show("You need to fill in all of the field.");
            }
        }

        private bool ValidateForm() {
            // TODO - ADD VALIDATION TO THE FORM
            if (firstNameValue.Text.Length == 0) {
                return false;
            }

            if (lastNameValue.Text.Length == 0) {
                return false;
            }

            if (emailValue.Text.Length == 0) {
                return false;
            }

            if (cellphoneValue.Text.Length == 0) {
                return false;
            }

            return true;
        }

        private void AddMemberButton_Click(object sender, EventArgs e) {
            PersonModel p = (PersonModel)selectTeamMemberDropdown.SelectedItem;

            if (p != null) {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void RemoveSelectedMemberButton_Click(object sender, EventArgs e) {
            PersonModel p = (PersonModel)teamMemberListBox.SelectedItem;

            if (p != null) {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
        }
    }
}
