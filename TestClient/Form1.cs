using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class Form1 : Form
    {
        private readonly ServiceReference1.CarServiceClient _carServiceClient;
        public Form1()
        {
            InitializeComponent();
            _carServiceClient = new ServiceReference1.CarServiceClient();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCars();
        }

        private void LoadCars()
        {
            var cars = _carServiceClient.GetAllCars();
            dataGridView1.DataSource = cars;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxModel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPrice.Text) ||
                    string.IsNullOrWhiteSpace(textBoxMileage.Text) ||
                    string.IsNullOrWhiteSpace(textBoxVinCode.Text))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }
                if (!decimal.TryParse(textBoxPrice.Text, out decimal price))
                {
                    MessageBox.Show("Invalid price value. Please enter a valid decimal number.");
                    return;
                }

                if (!int.TryParse(textBoxMileage.Text, out int mileage))
                {
                    MessageBox.Show("Invalid mileage value. Please enter a valid integer number.");
                    return;
                }
                string modelName = textBoxModel.Text;
                string vinCode = textBoxVinCode.Text;
                _carServiceClient.AddCar( modelName, price, mileage, vinCode);
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating car: {ex.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                idTextbox.Text = row.Cells["Id"].Value.ToString();
                textBoxModel.Text = row.Cells["Model"].Value.ToString();
                textBoxPrice.Text = row.Cells["Price"].Value.ToString();
                textBoxMileage.Text = row.Cells["Mileage"].Value.ToString();
                textBoxVinCode.Text = row.Cells["VinCode"].Value.ToString();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (string.IsNullOrWhiteSpace(idTextbox.Text) ||
                    string.IsNullOrWhiteSpace(textBoxModel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPrice.Text) ||
                    string.IsNullOrWhiteSpace(textBoxMileage.Text) ||
                    string.IsNullOrWhiteSpace(textBoxVinCode.Text))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }
                if (!int.TryParse(idTextbox.Text, out int id))
                {
                    MessageBox.Show("Invalid ID value. Please enter a valid integer number.");
                    return;
                }

                if (!decimal.TryParse(textBoxPrice.Text, out decimal price))
                {
                    MessageBox.Show("Invalid price value. Please enter a valid decimal number.");
                    return;
                }

                if (!int.TryParse(textBoxMileage.Text, out int mileage))
                {
                    MessageBox.Show("Invalid mileage value. Please enter a valid integer number.");
                    return;
                }
                string modelName = textBoxModel.Text;
                string vinCode = textBoxVinCode.Text;
                _carServiceClient.UpdateCar(new ServiceReference1.Car { Id = id, Model = modelName, Price = price, Mileage = mileage, VinCode = vinCode });
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing car: {ex.Message}");
            }
        }

        private void idTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(idTextbox.Text);
                _carServiceClient.DeleteCar(id);
                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting car: {ex.Message}");
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            textBoxModel.Text = "";
            textBoxPrice.Text = "";
            textBoxMileage.Text = "";
            textBoxVinCode.Text = "";
            idTextbox.Text = "";
        }
    }
}
