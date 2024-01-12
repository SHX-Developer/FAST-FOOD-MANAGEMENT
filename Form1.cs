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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FastFood
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=FastFoodDB;Integrated Security=True;"))
            {
                connection.Open();
                try
                {
                    {



                        string client = textBox1.Text.Trim();
                        string hotdog = comboBox1.Text.ToString();
                        string lavash = comboBox2.Text.ToString();
                        string hamburger = comboBox3.Text.ToString();
                        string drinks = comboBox4.Text.ToString();
                        DateTime Orderdate = dateTimePicker1.Value;
                        string totalCost = textBox2.Text.Trim();
                        string tableNumber = comboBox5.Text.ToString();






                        if (client == "" || hotdog == "" || lavash == "" || hamburger == "" || drinks == "" || totalCost == "")

                        {
                            MessageBox.Show("Please fill in all required fields.");
                            return;
                        }


                        SqlCommand command = new SqlCommand("INSERT INTO FastFoodTable (Client, Hotdog, Lavash, Hamburger, Drinks, OrderDate, TotalCost, TableNumber\n\r" +
                            ") VALUES (@Client, @Hotdog, @Lavash, @Hamburger, @Drinks, @OrderDate, @TotalCost, @TableNumber)", connection);


                        command.Parameters.AddWithValue("@Client", client);
                        command.Parameters.AddWithValue("@Hotdog", hotdog);
                        command.Parameters.AddWithValue("@Lavash", lavash);
                        command.Parameters.AddWithValue("@Hamburger", hamburger);
                        command.Parameters.AddWithValue("@Drinks", drinks);
                        command.Parameters.AddWithValue("@OrderDate", Orderdate);
                        command.Parameters.AddWithValue("@TotalCost", totalCost);
                        command.Parameters.AddWithValue("@TableNumber", tableNumber);





                        command.ExecuteNonQuery();


                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        comboBox3.Text = "";
                        comboBox4.Text = "";
                        comboBox5.Text  = "";
                        textBoxID.Text = "";



                        MessageBox.Show("Record added successfully");

                        dataTable.Clear();
                        adapter.Fill(dataTable);


                        dataGridView1.DataSource = dataTable;
                        dataGridView1.ResetBindings();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error adding Record: " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fastFoodDBDataSet.FastFoodTable' table. You can move, or remove it, as needed.
            this.fastFoodTableTableAdapter.Fill(this.fastFoodDBDataSet.FastFoodTable);
            connection = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=FastFoodDB;Integrated Security=True;");
            connection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM FastFoodTable", connection);

            dataTable = new DataTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBoxID.Text.Trim();


            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM FastFoodTable WHERE ID = @ID", connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("Record with ID " + id + " not found.");
                    return;
                }
            }
            string updateQuery = "UPDATE FastFoodTable SET Client = @Client, Hotdog = @Hotdog, Lavash = @Lavash,\n\r" +
                " Hamburger = @Hamburger, Drinks = @Drinks, OrderDate = @OrderDate, TotalCost = @TotalCost, TableNumber = @TableNumber WHERE ID = @ID";




            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {

                string client = textBox1.Text.Trim();
                string hotdog = comboBox1.Text.ToString();
                string lavash = comboBox2.Text.ToString();
                string hamburger = comboBox3.Text.ToString();
                string drinks = comboBox4.Text.ToString();
                DateTime orderDate = dateTimePicker1.Value;
                string totalCost = textBox2.Text.Trim();
                string tableNumber = comboBox5.Text.ToString();



                command.Parameters.AddWithValue("@Client", client);
                command.Parameters.AddWithValue("@Hotdog", hotdog);
                command.Parameters.AddWithValue("@Lavash", lavash);
                command.Parameters.AddWithValue("@Hamburger", hamburger);
                command.Parameters.AddWithValue("@Drinks", drinks);
                command.Parameters.AddWithValue("@OrderDate", orderDate);
                command.Parameters.AddWithValue("@TotalCost", totalCost);
                command.Parameters.AddWithValue("@TableNumber", tableNumber);
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();


                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                textBoxID.Text = "";
            }



            dataTable.Clear();
            adapter.Fill(dataTable);
            


            dataGridView1.DataSource = dataTable;
            dataGridView1.ResetBindings();

            MessageBox.Show("Record updated successfully.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }


            string id = textBoxID.Text.Trim();


            using (SqlCommand command = new SqlCommand("DELETE FROM FastFoodTable WHERE ID = @ID", connection))
            {

                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
                                               
                MessageBox.Show("Record deleted successfully.");


                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                textBoxID.Text = "";
            }
            dataTable.Clear();
            adapter.Fill(dataTable);
            


            dataGridView1.DataSource = dataTable;
            dataGridView1.ResetBindings();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = textBoxID.Text.Trim();

            if (id == "")
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }


            using (SqlCommand command = new SqlCommand("SELECT * FROM FastFoodTable WHERE ID = @ID", connection))
            {
                

                command.Parameters.AddWithValue("@ID", id);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        textBox1.Text = reader["Client"].ToString();
                        textBox2.Text = reader["HotDog"].ToString();
                        comboBox1.Text = reader["Lavash"].ToString();
                        comboBox2.Text = reader["Hamburger"].ToString();
                        comboBox3.Text = reader["Drinks"].ToString();
                        dateTimePicker1.Value = (DateTime)reader["OrderDate"];
                        textBox2.Text = reader["TotalCost"].ToString();
                        comboBox5.Text = reader["TableNumber"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Record with ID " + id + " not found.");
                    }
                    reader.Close();
                    dataTable.Clear();
                    adapter.Fill(dataTable);
                    

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.ResetBindings();
                    
                }
            }
        }
    }
}
