using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesAdministration.PL
{
    public partial class FRM_CUSTOMERS_LIST : Form
    {
        public FRM_CUSTOMERS_LIST()
        {
            InitializeComponent();
        }
        BL.CLS_CUSTOMERS cust = new BL.CLS_CUSTOMERS();
        private void CUSTOMERS_LIST_Load(object sender, EventArgs e)
        {
            this.dgvCustomers.DataSource = cust.GET_ALL_CUSTOMERS();
            this.dgvCustomers.Columns[0].Visible = false;
            this.dgvCustomers.Columns[5].Visible = false;

        }

        private void dgList_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
