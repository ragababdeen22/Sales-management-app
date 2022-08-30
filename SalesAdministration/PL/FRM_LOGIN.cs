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
    public partial class FRM_LOGIN : Form
    {
        BL.CLS_LOGIN log = new BL.CLS_LOGIN();
        public FRM_LOGIN()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable Dt = log.LOGIN(txtID.Text, txtPWD.Text);
            if (Dt.Rows.Count > 0)
            {
                if (Dt.Rows[0][2].ToString() == "مدير")
                {
                    //don't  use 
                    FRM_MAIN FRM_MAIN = new FRM_MAIN();
                    // Form_Main.getMainForm.المنتوجاتToolStripMenuItem.Enabled = true;
                    // Form_Main.getMainForm.العملاءToolStripMenuItem.Enabled = true;
                    // Form_Main.getMainForm.المستخدمونToolStripMenuItem.Enabled = true;
                    // Form_Main.getMainForm.انشاءنسخهاحتياطيهToolStripMenuItem.Enabled = true;
                    //// Form_Main.getMainForm.استعادةنسخهمحفوظهToolStripMenuItem.Enabled = true;
                    // Form_Main.getMainForm.المستخدمونToolStripMenuItem.Visible = true;
                    // Program.SalesMan = Dt.Rows[0]["FullName"].ToString();
                    FRM_MAIN.getMainForm.المنتوجاتToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.العملاءToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.إنشاءنسخةاحتياطيةToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.استعادةنسخةمحفوظةToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = true;
                    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = true;

                    Program.SalesMan = Dt.Rows[0]["FullName"].ToString();


                    this.Close();
                }
                else  if (Dt.Rows[0][2].ToString() == "عادي")
                {
                   
                    FRM_MAIN FRM_MAIN = new FRM_MAIN();
               
                    FRM_MAIN.getMainForm.المنتوجاتToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.العملاءToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = false;
                    FRM_MAIN.getMainForm.إنشاءنسخةاحتياطيةToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.استعادةنسخةمحفوظةToolStripMenuItem.Enabled = true;
                    FRM_MAIN.getMainForm.المستخدمونToolStripMenuItem.Visible = true;
                    Program.SalesMan = Dt.Rows[0]["FullName"].ToString();


                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("login failed");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
