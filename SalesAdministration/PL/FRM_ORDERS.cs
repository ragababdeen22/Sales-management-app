using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesAdministration.PL
{
    public partial class FRM_ORDERS : Form
    {
       BL.CLS_ORDERS order = new BL.CLS_ORDERS();
        DataTable Dt = new DataTable();

        void CalculateAmount()
        {
            if (txtPrice.Text != string.Empty && txtQty.Text != string.Empty)

                txtAmount.Text = (Convert.ToDouble(txtPrice.Text) * Convert.ToInt32(txtQty.Text)).ToString();
        }

        void CalculateTotalAmount()
        {
            if (txtDiscount.Text != string.Empty && txtAmount.Text != string.Empty)
            {
                double Discount = Convert.ToDouble(txtDiscount.Text);
                double Amount = Convert.ToDouble(txtAmount.Text);
                double TotalAmount = Amount - (Amount * (Discount / 100));
                txtTotalAmount.Text = TotalAmount.ToString();
            }
        }
        void ClearBoxes()
        {
            txtIDproduct.Clear();
            txtNameProduct.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtAmount.Clear();
            txtDiscount.Clear();
            txtTotalAmount.Clear();
            btnBrowse.Focus();
        }
        void ClearData()
        {
            txtOrderID.Clear();
            txtDesOrder.Clear();
            txtSalesMan.Clear();
            dtOrder.ResetText();
            txtCustomerID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtTel.Clear();
            ClearBoxes();
            Dt.Clear();
            dgvProducts.DataSource = null;
            txtSumTotals.Clear();
            pbox.Image = null;
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnPrint.Enabled = true;
            btnPrint.Enabled = true;
        }


        void CreateDataTable()
        {
            Dt.Columns.Add("معرف المنتوج");
            Dt.Columns.Add("اسم المنتوج");
            Dt.Columns.Add("الثمن");
            Dt.Columns.Add("الكمية");
            Dt.Columns.Add("المبلغ");
            Dt.Columns.Add("نسبة الخصم (%)");
            Dt.Columns.Add("المبلغ الإجمالي");

            dgvProducts.DataSource = Dt;

             //Add ButtonColumn To DataGridView
            /* DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
              btn.HeaderText = "اختيار منتج";
              btn.Text = "البحث";
              btn.UseColumnTextForButtonValue = true;
              dgvProducts.Columns.Insert(0,btn); */
        }
        void ResizeDGV()
        {
            //this.dgvProducts.RowHeadersWidth = 86;
            //this.dgvProducts.Columns[0].Width = 99;
            //this.dgvProducts.Columns[1].Width = 199;
            //this.dgvProducts.Columns[2].Width = 85;
            //this.dgvProducts.Columns[3].Width = 93;
            //this.dgvProducts.Columns[4].Width = 85;
            //this.dgvProducts.Columns[5].Width = 93;
            //this.dgvProducts.Columns[6].Width = 121;

        }

        public FRM_ORDERS()
        {
            InitializeComponent();
            CreateDataTable();
            //  ResizeDGV();
            txtSalesMan.Text = Program.SalesMan;
        }


        private void btnNew_Click_1(object sender, EventArgs e)
        {
            this.txtOrderID.Text = order.GET_LAST_ORDER_ID().Rows[0][0].ToString();
            btnNew.Enabled = false;
            btnAdd.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FRM_CUSTOMERS_LIST frm = new FRM_CUSTOMERS_LIST();
            frm.ShowDialog();
            if (frm.dgvCustomers.CurrentRow.Cells[5].Value is DBNull)
            {
                MessageBox.Show("هذا العميل لا يتوفر على صورة", "لا توجد صورة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pbox.Image = null;
                this.txtCustomerID.Text = frm.dgvCustomers.CurrentRow.Cells[0].Value.ToString();
                this.txtFirstName.Text = frm.dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                this.txtLastName.Text = frm.dgvCustomers.CurrentRow.Cells[2].Value.ToString();
                this.txtTel.Text = frm.dgvCustomers.CurrentRow.Cells[3].Value.ToString();
                this.txtEmail.Text = frm.dgvCustomers.CurrentRow.Cells[4].Value.ToString();
                return;
            }
            this.txtCustomerID.Text = frm.dgvCustomers.CurrentRow.Cells[0].Value.ToString();
            this.txtFirstName.Text = frm.dgvCustomers.CurrentRow.Cells[1].Value.ToString();
            this.txtLastName.Text = frm.dgvCustomers.CurrentRow.Cells[2].Value.ToString();
            this.txtTel.Text = frm.dgvCustomers.CurrentRow.Cells[3].Value.ToString();
            this.txtEmail.Text = frm.dgvCustomers.CurrentRow.Cells[4].Value.ToString();
            byte[] custPicture = (byte[])frm.dgvCustomers.CurrentRow.Cells[5].Value;
            MemoryStream ms = new MemoryStream(custPicture);
            this.pbox.Image = Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ClearBoxes();
            FRM_PRODUCTS_LIST frm = new FRM_PRODUCTS_LIST();
            frm.ShowDialog();
            txtIDproduct.Text = frm.dgvProducts.CurrentRow.Cells[0].Value.ToString();
            txtNameProduct.Text = frm.dgvProducts.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = frm.dgvProducts.CurrentRow.Cells[3].Value.ToString();
            txtQty.Focus();
        }

        // - منع كتابة النصوص و الرموز في الكمية والمبلغ


        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

       // - منع كتابة النصوص و الرموز في الكمية والمبلغ
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char DecimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != DecimalSeparator)
            {
                e.Handled = true;
            }
        }

        private void txtPrice_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPrice.Text != string.Empty)
            {
                txtQty.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtQty.Text != string.Empty)
            {
                txtDiscount.Focus();
            }
        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateAmount();
            CalculateTotalAmount();
           
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateAmount();
            CalculateTotalAmount();
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateTotalAmount();
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (order.VerifyQty(txtIDproduct.Text, Convert.ToInt32(txtQty.Text)).Rows.Count < 1)
                {
                    MessageBox.Show("الكمية المدخلة لهذا المنتج غير متاحة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                for (int i = 0; i < dgvProducts.Rows.Count - 1; i++)
                {
                    if (dgvProducts.Rows[i].Cells[0].Value.ToString() == txtIDproduct.Text)
                    {
                        MessageBox.Show("هذا المنتج تم إدخاله مسبقا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                }

                DataRow r = Dt.NewRow();
                r[0] = txtIDproduct.Text;
                r[1] = txtNameProduct.Text;
                r[2] = txtPrice.Text;
                r[3] = txtQty.Text;
                r[4] = txtAmount.Text;
                r[5] = txtDiscount.Text;
                r[6] = txtTotalAmount.Text;

                Dt.Rows.Add(r);

                dgvProducts.DataSource = Dt;
                ClearBoxes();

                txtSumTotals.Text =
                 (from DataGridViewRow row in dgvProducts.Rows
                  where row.Cells[6].FormattedValue.ToString() != string.Empty
                  select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();

            }
          
        }

        private void dgvProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtIDproduct.Text = this.dgvProducts.CurrentRow.Cells[0].Value.ToString();
                txtNameProduct.Text = this.dgvProducts.CurrentRow.Cells[1].Value.ToString();
                txtPrice.Text = this.dgvProducts.CurrentRow.Cells[2].Value.ToString();
                txtQty.Text = this.dgvProducts.CurrentRow.Cells[3].Value.ToString();
                txtAmount.Text = this.dgvProducts.CurrentRow.Cells[4].Value.ToString();
                txtDiscount.Text = this.dgvProducts.CurrentRow.Cells[5].Value.ToString();
                txtTotalAmount.Text = this.dgvProducts.CurrentRow.Cells[6].Value.ToString();
                dgvProducts.Rows.RemoveAt(dgvProducts.CurrentRow.Index);
                txtQty.Focus();
            }
            catch
            {
                return;
            }
        }

        private void dgvProducts_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtSumTotals.Text =
                (from DataGridViewRow row in dgvProducts.Rows
                 where row.Cells[6].FormattedValue.ToString() != string.Empty
                 select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvProducts.SelectedRows.Count > 0)
            {
                dgvProducts.Rows.RemoveAt(this.dgvProducts.SelectedRows[0].Index);
            }
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvProducts_DoubleClick(sender, e);
        }

        private void حذفالسطرالحاليToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvProducts.Rows.RemoveAt(dgvProducts.CurrentRow.Index);

        }

        private void حذفالكلToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dt.Clear();
            dgvProducts.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Check Values

            if (txtOrderID.Text == string.Empty || txtCustomerID.Text == string.Empty || dgvProducts.Rows.Count < 1 || txtDesOrder.Text == string.Empty)
            {
                MessageBox.Show("ينبغي تسجيل المعلومات المهمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          //إضافة معلومات الفاتورة
            order.ADD_ORDER(Convert.ToInt32(txtOrderID.Text), dtOrder.Value, Convert.ToInt32(txtCustomerID.Text), txtDesOrder.Text, txtSalesMan.Text);
            //MessageBox.Show("تمت عملية الحفظ بنجاح", "عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //إضافة المنتجات المدخلة
            for (int i = 0; i < dgvProducts.Rows.Count - 1; i++)
            {
                order.ADD_ORDER_DETAILS(dgvProducts.Rows[i].Cells[0].Value.ToString(),
                                        Convert.ToInt32(txtOrderID.Text),
                                        Convert.ToInt32(dgvProducts.Rows[i].Cells[3].Value),
                                        dgvProducts.Rows[i].Cells[2].Value.ToString(),
                                        Convert.ToInt32(dgvProducts.Rows[i].Cells[5].Value),
                                        dgvProducts.Rows[i].Cells[4].Value.ToString(),
                                        dgvProducts.Rows[i].Cells[6].Value.ToString());


            }

            MessageBox.Show("تمت عملية الحفظ بنجاح", "عملية الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
           ClearData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //get The last order


           // this.Cursor = Cursors.WaitCursor;
            int order_ID = Convert.ToInt32(order.GET_LAST_ORDER_FOR_PRINT().Rows[0][0]);
            RPT.rpt_orders report = new RPT.rpt_orders();
            RPT.FRM_RPT_PRODUCT frm = new RPT.FRM_RPT_PRODUCT();
            report.SetDataSource(order.GetOrderDetails(order_ID));
            frm.crystalReportViewer1.ReportSource = report;
            frm.ShowDialog();
          //  this.Cursor = Cursors.Default;
        }
    }
}

