using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace CopyFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                lbStatus.Text = "Doing...";
                if (!IsValidForm())
                {
                    lbStatus.Text = "Error";
                    return;
                }

                var data = txtURL.Text.Replace("\r\n", ",") ?? "";
                List<string> result = data.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToList();

                if (result.Count == 0)
                {
                    lbStatus.Text = "Error";
                    return;
                }

                foreach (var item in result)
                {
                    string sourcePath = Path.GetDirectoryName(item) ?? "";
                    string fileName = Path.GetFileName(item) ?? "";
                    string targetPath = sourcePath.Replace(txtSource.Text, txtTarget.Text);
                    if (Directory.Exists(sourcePath) && Directory.Exists(targetPath) && !string.IsNullOrEmpty(sourcePath) && !string.IsNullOrEmpty(fileName))
                    {
                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = Path.Combine(sourcePath, fileName);
                        string destFile = Path.Combine(targetPath, fileName);

                        // To copy a file to another location and
                        // overwrite the destination file if it already exists.
                        File.Copy(sourceFile, destFile, true);
                        lbStatus.Text = "Done";
                    }
                    else
                    {
                        lbStatus.Text = "Error";
                    }
                }
            }
            catch (Exception)
            {
                lbStatus.Text = "Error";
            }
        }

        private bool IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(txtTarget.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtTarget.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSource.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtSource.Text))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtURL.Text))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(txtURL.Text))
            {
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Clipboard.GetText()))
                txtURL.Text += ("\r\n" + Clipboard.GetText());
        }
    }
}
