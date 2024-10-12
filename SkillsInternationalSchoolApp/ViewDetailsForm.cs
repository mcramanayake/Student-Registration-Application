using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkillsInternationalSchoolApp
{
    public partial class ViewDetailsForm : Form
    {
        public ViewDetailsForm()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ViewDetailsForm_Load(object sender, EventArgs e)
        {
            LoadRegNos();
        }

        private void LoadRegNos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

            // Clear existing items if needed
            cmbRegNo.Items.Clear();
            cmbRegNo.Items.Add("Select Registration Number");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT regNo FROM Registration";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cmbRegNo.Items.Add(reader["regNo"].ToString());
                        }

                        cmbRegNo.SelectedIndex = 0; // Set default to "Select Registration Number"
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading registration numbers: " + ex.Message);
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbRegNo.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a valid registration number.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
            string query = "SELECT * FROM Registration WHERE regNo = @regNo";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@regNo", cmbRegNo.SelectedItem.ToString());

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate TextBoxes with data from the database
                        txtFirstName.Text = reader["firstName"].ToString();
                        txtLastName.Text = reader["lastName"].ToString();
                        txtDateOfBirth.Text = Convert.ToDateTime(reader["dateOfBirth"]).ToString("d");
                        txtGender.Text = reader["gender"].ToString();
                        txtAddress.Text = reader["address"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtMobilePhone.Text = reader["mobilePhone"].ToString();
                        txtHomePhone.Text = reader["homePhone"].ToString();
                        txtParentName.Text = reader["parentName"].ToString();
                        txtNIC.Text = reader["nic"].ToString();
                        txtContactNo.Text = reader["contactNo"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No details found for the selected registration number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving details: " + ex.Message);
                }
            }
        }
    }
}

