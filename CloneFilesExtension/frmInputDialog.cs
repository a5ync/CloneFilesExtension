using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloneFilesExtension
{
    public partial class frmInputDialog : Form
    {
        public frmInputDialog()            
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            label1.Text = fvi.FileVersion;
            //this.Location = Screen.AllScreens[1].WorkingArea.Location;
        }

      

        protected override void OnClosing(CancelEventArgs e)
        {
            CloneFilesExtension.numberOfCopies = Convert.ToInt32(numericUpDown1.Value);
            base.OnClosing(e);
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
