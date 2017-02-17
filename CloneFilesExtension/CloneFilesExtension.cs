using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloneFilesExtension
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    public class CloneFilesExtension : SharpContextMenu
    {
        public static int numberOfCopies = 0;
        protected override bool CanShowMenu()
        {
            //  We will always show the menu.
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            var menu = new ContextMenuStrip();            
            var cloneFiles = new ToolStripMenuItem
            {
                Text = "Clone selected files"
            };
            cloneFiles.Click += (sender, args) => CloneFiles();
            //  Add the item to the context menu.
            menu.Items.Add(cloneFiles);
            //  Return the menu.
            return menu;
        }

        private void CloneFiles()
        {
            //to avoid spinner
            Task.Factory.StartNew(() =>
            {
                var copies = 0;               
                frmInputDialog form = new frmInputDialog();
                form.StartPosition = FormStartPosition.Manual;
                form.Location = Cursor.Position;
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Read the contents of testDialog's TextBox.
                    copies = CloneFilesExtension.numberOfCopies;
                    //  Go through each file.
                    foreach (var filePath in SelectedItemPaths)
                    {
                        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                        var ext = Path.GetExtension(filePath);
                        for (int i = 0; i < copies; i++)
                        {
                            var target = $@"{Path.GetDirectoryName(filePath)}\{fileNameWithoutExt}_{i}{ext}";
                            File.Copy(filePath, target);
                        }
                    }
                }                 
                form.Dispose();
            });
        }
    }
}
