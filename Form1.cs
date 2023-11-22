using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOpgaven_2211
{
    public partial class FacilitiesFacilityIDLbl : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        public FacilitiesFacilityIDLbl()
        {
            InitializeComponent();
        }

        private void FacilitiesFacilityIDLbl_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDatabase;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        private void InsertFacilitiesBtn_Click(object sender, EventArgs e)
        {
            string query = $"insert into Facilities values('{txtFacilityId.Text.ToString()}','{txtHotelNo.Text.ToString()}','{txtFacilityName.Text}','{txtDescription.Text}')";
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            cleardata();
            conn.Close();
            displaydata();
        }
        private void cleardata()
        {
            txtFacilityId.Clear();
            txtHotelNo.Clear();
            txtFacilityName.Clear();
            txtDescription.Clear();
        }

        private void UpdateFacilitiesBtn_Click(object sender, EventArgs e)
        {
            //conn.Open();
            //SqlCommand cmd = conn.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "UPDATE Facilities set FacilityID='" + txtFacilityId.Text.ToString() + "',Hotel_no='" + txtHotelNo.Text.ToString() + "',FacilityName='" + txtFacilityName.Text + "' where Description='" + txtDescription.Text + "' ";
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //displaydata();
            //cleardata();

            conn.Open();
            cmd.Parameters.Clear();

            StringBuilder updateQuery = new StringBuilder("UPDATE Facilities SET ");

            if (!string.IsNullOrEmpty(txtFacilityId.Text))
            {
                updateQuery.Append("FacilityID = @FacilityID, ");
                cmd.Parameters.AddWithValue("@FacilityID", txtFacilityId.Text.ToString());
            }

            if (!string.IsNullOrEmpty(txtHotelNo.Text))
            {
                updateQuery.Append("Hotel_no = @Hotel_no, ");
                cmd.Parameters.AddWithValue("@Hotel_no", txtHotelNo.Text.ToString());
            }

            if (!string.IsNullOrEmpty(txtFacilityName.Text))
            {
                updateQuery.Append("FacilityName = @FacilityName, ");
                cmd.Parameters.AddWithValue("@FacilityName", txtFacilityName.Text);
            }

            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                updateQuery.Append("Description = @Description, ");
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            }

            updateQuery.Length -= 2;
            updateQuery.Append(" WHERE FacilityID = @FacilityID AND Hotel_no = @Hotel_no AND FacilityName = @FacilityName AND Description = @Description");

            cmd.CommandText = updateQuery.ToString();
            cmd.ExecuteNonQuery();

            conn.Close();
            displaydata();
            cleardata();
        }

        private void ShowAllFacilitiesBtn_Click(object sender, EventArgs e)
        {
            displaydata();
        }
        private void displaydata()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Facilities";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewFacilities.DataSource = dt;
            conn.Close();
        }

        private void DeleteFacilitiesBtn_Click(object sender, EventArgs e)
        {
            string query = $"delete Facilities where FacilityID='{txtFacilityId.Text.ToString()}'";
            cmd.CommandText = query;
            conn.Open();
            cmd.ExecuteNonQuery();
            dataGridViewFacilities.DataSource = query;
            cleardata();
            conn.Close();
            displaydata();
        }

        private void FindFacilitiesBtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Facilities where FacilityID='" + FindBarFacilities.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            txtHotelNo.Text = dt.ToString();
            txtFacilityName.Text = dt.ToString();
            txtDescription.Text = dt.ToString();
            dataGridViewFacilities.DataSource = dt;
            conn.Close();
        }

        private void ExitFacilitiesBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuFacilitiesBtn_Click(object sender, EventArgs e)
        {
            new MainMenu();
        }
    }
}
