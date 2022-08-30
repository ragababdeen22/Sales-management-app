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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;

namespace SalesAdministration.PL
{
    public partial class FRM_CATEGORIES : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server=RAGAB\SQLEXPRESS01; Database=Product_DB; Integrated Security=true");
        SqlDataAdapter da;
        DataTable dt=new DataTable();
        BindingManagerBase bmb;
        SqlCommandBuilder cmdb;
        public FRM_CATEGORIES()
        {
            InitializeComponent();
            da = new SqlDataAdapter("select id_cat as 'المعرف', DESCRIPTION_CAT as 'الصنف' from categories", sqlcon);
            da.Fill(dt);
            dgList.DataSource = dt;
            txtID.DataBindings.Add("text", dt, "المعرف");
            txtDes.DataBindings.Add("text", dt, "الصنف");
            bmb = this.BindingContext[dt];
            lblPosition.Text = (bmb.Position+1) + "  /  " + bmb.Count;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bmb.Position = 0;
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bmb.Position = bmb.Count;
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bmb.Position -= 1;
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bmb.Position += 1;
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bmb.AddNew();
            btnNew.Enabled = false;
            btnAdd.Enabled = true;
            int id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0])+1;
            txtID.Text = id.ToString();
            txtDes.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Added successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bmb.RemoveAt(bmb.Position);
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Edited successfully", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + "  /  " + bmb.Count;

        }

        private void btnPrinAll_Click(object sender, EventArgs e)
        {
            RPT.rpt_all_categories rpt = new RPT.rpt_all_categories();
            RPT.FRM_RPT_PRODUCT frm = new RPT.FRM_RPT_PRODUCT();
            rpt.Refresh();
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.ShowDialog();
        }

        private void btnPrintCurrent_Click(object sender, EventArgs e)
        {
            RPT.rpt_single_category rpt = new RPT.rpt_single_category();
            RPT.FRM_RPT_PRODUCT frm = new RPT.FRM_RPT_PRODUCT();
            rpt.SetParameterValue("@id", Convert.ToInt32(txtID.Text));
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.ShowDialog();

        }

        private void exportToPdfAll_Click(object sender, EventArgs e)
        {
            RPT.rpt_all_categories myReport = new RPT.rpt_all_categories();

            //Create Export Options
            ExportOptions export = new ExportOptions();

            //Create Object For destination
            DiskFileDestinationOptions dfoptions = new DiskFileDestinationOptions();

            //Create Object For Excel Format
            PdfFormatOptions pdfformat = new PdfFormatOptions();

            //Set the path of destination
            dfoptions.DiskFileName = @"D:\CategoriesList.pdf";

            //Set Report Options to crystal export options
            export = myReport.ExportOptions;

            //Set Destination type
            export.ExportDestinationType = ExportDestinationType.DiskFile;

            //Set the type of document
            export.ExportFormatType = ExportFormatType.PortableDocFormat;

            //format the excel document
            export.ExportFormatOptions = pdfformat;

            //Set Destination option
            export.ExportDestinationOptions = dfoptions;

            myReport.Refresh();
            //Export the report
            myReport.Export();

            MessageBox.Show("List Exported Successfully !", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void ExportToPdfCurrent_Click(object sender, EventArgs e)
        {
            RPT.rpt_single_category myReport = new RPT.rpt_single_category();

            //Create Export Options
            ExportOptions export = new ExportOptions();

            //Create Object For destination
            DiskFileDestinationOptions dfoptions = new DiskFileDestinationOptions();

            //Create Object For Excel Format
            PdfFormatOptions pdfformat = new PdfFormatOptions();

            //Set the path of destination
            dfoptions.DiskFileName = @"D:\CategoryDetails.pdf";

            //Set Report Options to crystal export options
            export = myReport.ExportOptions;

            //Set Destination type
            export.ExportDestinationType = ExportDestinationType.DiskFile;

            //Set the type of document
            export.ExportFormatType = ExportFormatType.PortableDocFormat;

            //format the excel document
            export.ExportFormatOptions = pdfformat;

            //Set Destination option
            export.ExportDestinationOptions = dfoptions;

            myReport.SetParameterValue("@id", Convert.ToInt32(txtID.Text));
            //Export the report
            myReport.Export();

            MessageBox.Show("List Exported Successfully !", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void FRM_CATEGORIES_Load(object sender, EventArgs e)
        {

        }

    }
}
