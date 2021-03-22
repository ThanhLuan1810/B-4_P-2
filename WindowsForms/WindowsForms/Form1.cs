using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-RTSD795;Initial Catalog=DemoCRUD;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
            GetStudentsRecord();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GetStudentsRecord()
        {
            string query = "SELECT* FROM StudentsTb";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if (TxtHName.Text == string.Empty || TxtNName.Text == string.Empty || TxtAddress.Text == string.Empty || string.IsNullOrEmpty(TxtPhone.Text) || string.IsNullOrEmpty(TxtRoll.Text))
            {
                MessageBox.Show("Có chỗ chứa nhập dữ liệu!!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES" +
                    "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
        }

        public int StudentID;

        private void StudentRecordData_CellClick(object sender, EventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            TxtHName.Text = StudentRecordData.SelectedRows[0].Cells[1].Value.ToString();
            TxtNName.Text = StudentRecordData.SelectedRows[0].Cells[2].Value.ToString();
            TxtRoll.Text = StudentRecordData.SelectedRows[0].Cells[3].Value.ToString();
            TxtAddress.Text = StudentRecordData.SelectedRows[0].Cells[4].Value.ToString();
            TxtPhone.Text = StudentRecordData.SelectedRows[0].Cells[5].Value.ToString();
        }
        

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET " +
                    "Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber, Address = @Address, Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
                ResetData();
            }else
            {
                MessageBox.Show("Cập nhật lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetData()
        {
           
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
