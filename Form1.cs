using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Final_Exam
    {
    public partial class Form1 : Form
        {
        private ShipContext Context = new ShipContext();

        public Form1 ()
            {
            InitializeComponent();
            }

        private void Form1_Load ( object sender, EventArgs e )
            {
            GetList();
            ClearFields();
            }

        private void GetList ()
            {
            dataGridView1.DataSource = Context.Cargos.ToList();
            dataGridView1.Columns["ID"].Visible = false;
            }

        private void ClearFields ()
            {
            txtName.Text = string.Empty;
            txtName.Tag = string.Empty;
            txtName.Tag = null;
            txtMaterial.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtDestination.Text = string.Empty;
            }

        //---- Basic TextBoxes Settings ----

        /// <summary>
        ///  change Color of txtbox when Entring it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Enter ( object sender, EventArgs e )
            {
            TextBox tb = (TextBox)sender;
            tb.BackColor = Color.LightSkyBlue;
            }

        /// <summary>
        /// change Color of txtbox when LEAVING it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_Leave ( object sender, EventArgs e )
            {
            TextBox tb = (TextBox)sender;
            tb.BackColor = Color.White;
            }

        /// <summary>
        /// only decimal inputs & key controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_KeyPress ( object sender, KeyPressEventArgs e )
            {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains('.')) return;
            e.Handled = true;
            }

        //---- End of Basic TextBoxes Settings ----

        //--- Basic DataGridView Settings ----

        /// <summary>
        /// fetch data from dataGridView into related textboxes whenever clicking on each cell 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick ( object sender, DataGridViewCellEventArgs e )
            {
            txtName.Tag = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            txtMaterial.Text = dataGridView1.CurrentRow.Cells["Material"].Value.ToString();
            txtWeight.Text = dataGridView1.CurrentRow.Cells["Weight"].Value.ToString();
            txtDestination.Text = dataGridView1.CurrentRow.Cells["Destination"].Value.ToString();
            }

        /// <summary>
        /// counter cell at the begining of each row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting ( object sender, DataGridViewCellFormattingEventArgs e )
            {
            dataGridView1.Rows[e.RowIndex].Cells["Counter"].Value = e.RowIndex + 1;
            }

        //--- End of Basic DataGridView Settings ----


        //---- Action Buttons ----

        /// <summary>
        /// BTN 'Insert'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click ( object sender, EventArgs e )
            {
            Cargo cargo = new Cargo();
            cargo.Name = txtName.Text;
            cargo.Material = txtMaterial.Text;
            cargo.Weight = Convert.ToDecimal(txtWeight.Text);
            cargo.Destination = txtDestination.Text;

            Context.Cargos.Add(cargo);
            Context.SaveChanges();

            GetList();
            ClearFields();
            MessageBox.Show("Cargo Successfully Added");
            }

        /// <summary>
        /// BTN 'Update'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click ( object sender, EventArgs e )
            {
            long cargoID = Convert.ToInt64(txtName.Tag.ToString());
            Cargo cargo = Context.Cargos.Where(ca => ca.ID == cargoID).First();

            Context.Cargos.Attach(cargo);
            cargo.Name = txtName.Text;
            cargo.Material = txtMaterial.Text;
            cargo.Weight = Convert.ToDecimal(txtWeight.Text);
            cargo.Destination = txtDestination.Text;

            Context.SaveChanges();
            GetList();
            ClearFields();
            MessageBox.Show("Cargo Successfully Edited");
            }

        /// <summary>
        /// BTN 'Delete'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelet_Click ( object sender, EventArgs e )
            {
            long cargoID = Convert.ToInt64(txtName.Tag.ToString());
            Cargo cargo = Context.Cargos.Where(ca => ca.ID == cargoID).First();
            if (MessageBox.Show("Would you really want to Delete?", "Delete Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                Context.Cargos.Remove(cargo);

                Context.SaveChanges();
                GetList();
                ClearFields();
                MessageBox.Show("Cargo Successfully Deleted");
                }
            }

        //---- End of Action Buttons ----

        //---- Search via textBoxes ----

        /// <summary>
        /// search by 'Name'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged ( object sender, EventArgs e )
            {
            var query = from ca in Context.Cargos
                        where ca.Name.Contains(txtName.Text)
                        select ca;
            dataGridView1.DataSource= query.ToList();
            }

        /// <summary>
        /// seach by 'Material'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaterial_TextChanged ( object sender, EventArgs e )
            {
            var query = from ca in Context.Cargos
                        where ca.Material.Contains(txtMaterial.Text)
                        select ca;
            dataGridView1.DataSource = query.ToList();
            }

        /// <summary>
        /// search by 'Weight'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight_TextChanged ( object sender, EventArgs e )
            {
            
            var query = from ca in Context.Cargos
                        where ca.Weight.ToString().Contains(txtWeight.Text)
                        select ca;
            dataGridView1.DataSource = query.ToList();
            }

        /// <summary>
        /// search by 'Destination'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDestination_TextChanged ( object sender, EventArgs e )
            {
            var query = from ca in Context.Cargos
                        where ca.Destination.Contains(txtDestination.Text)
                        select ca;
            dataGridView1.DataSource = query.ToList();
            }

        //---- End of Search via textBoxes ----

        }
    }
    
