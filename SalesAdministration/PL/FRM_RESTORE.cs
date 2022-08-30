using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SalesAdministration.PL
{
    public partial class FRM_RESTORE : Form
    {
        SqlConnection con = new SqlConnection(@"Server=.\KHALID; Database=master; Integrated Security=True");
        SqlCommand cmd;

        public FRM_RESTORE()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }

        private void l_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string strQuery = "ALTER Database Product_DB SET OFFLINE WITH ROLLBACK IMMEDIATE; Restore Database Product_DB From Disk='"+txtFileName.Text+"'";
            cmd = new SqlCommand(strQuery, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح", "استعادة النسخة الاحتياطية", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
