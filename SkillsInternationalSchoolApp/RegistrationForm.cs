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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

            string query = "INSERT INTO Registration (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) " +
                           "VALUES (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@dateOfBirth", dtpDateOfBirth.Value);
                cmd.Parameters.AddWithValue("@gender", rbMale.Checked ? "Male" : "Female");
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@mobilePhone", txtMobilePhone.Text);
                cmd.Parameters.AddWithValue("@homePhone", txtHomePhone.Text);
                cmd.Parameters.AddWithValue("@parentName", txtParentName.Text);
                cmd.Parameters.AddWithValue("@nic", txtNIC.Text);
                cmd.Parameters.AddWithValue("@contactNo", txtContactNo.Text);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        SqlCommand verifyCmd = new SqlCommand("SELECT COUNT(*) FROM Registration WHERE regNo = @regNo", con);
                        verifyCmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));

                        int recordExists = (int)verifyCmd.ExecuteScalar();
                        if (recordExists > 0)
                        {
                            MessageBox.Show("Registration successful and confirmed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registration was successful, but could not confirm.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. No rows were affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

            string query = "UPDATE Registration SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, " +
                           "gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, " +
                           "homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo " +
                           "WHERE regNo = @regNo";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@regNo", cmbRegNo.Text);
                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@dateOfBirth", dtpDateOfBirth.Value);
                    cmd.Parameters.AddWithValue("@gender", rbMale.Checked ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@mobilePhone", txtMobilePhone.Text);
                    cmd.Parameters.AddWithValue("@homePhone", txtHomePhone.Text);
                    cmd.Parameters.AddWithValue("@parentName", txtParentName.Text);
                    cmd.Parameters.AddWithValue("@nic", txtNIC.Text);
                    cmd.Parameters.AddWithValue("@contactNo", txtContactNo.Text);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found with the provided Registration Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

                string query = "DELETE FROM Registration WHERE regNo = @regNo";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@regNo", cmbRegNo.Text);

                        try
                        {
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with the provided Registration Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Delete failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void nic_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            txtMobilePhone.Clear();
            txtHomePhone.Clear();
            txtParentName.Clear();
            txtNIC.Clear();
            txtContactNo.Clear();

            dtpDateOfBirth.Value = DateTime.Today;

            rbMale.Checked = false;
            rbFemale.Checked = false;

            cmbRegNo.SelectedIndex = -1;

            cmbRegNo.Focus();
        }

        private void cmbRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            cmbRegNo.Items.Clear();

            LoadRegNos();
        }


        private void LoadRegNos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;

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

                        cmbRegNo.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading registration numbers: " + ex.Message);
                    }
                }
            }
        }

        private void logoutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                this.Close();
            }
        }

        private void exitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void RegistrationForm_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void RegistrationForm_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            ViewDetailsForm detailsForm = new ViewDetailsForm();
            detailsForm.ShowDialog();
        }
    }
}
