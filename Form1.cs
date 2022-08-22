using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ObjectOrientedProgramming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            string sourcePath = sourceTextBox.Text;
            string targetPath = targetTextBox.Text;
            if (sourceTextBox.Text == "" || targetTextBox.Text == "")
            {
                MessageBox.Show("Kaynak dizin ve/veya hedef dizin yolu gereklidir", "Uyarı");
            }
            else
            {
                CopyAll(sourcePath, targetPath);
            }
        }

        private void CopyAll(string source, string target)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(source);
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
                FileInfo[] files = directoryInfo.GetFiles();

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                if (subDirectories.Length > 0)
                {
                    foreach (var subDirectory in subDirectories)
                    {
                        foreach (var file in files)
                        {
                            File.Copy(source + "\\" + file, target + "\\" + file, true);
                        }
                        Directory.CreateDirectory(target + "\\" + subDirectory.Name);

                        CopyAll(source + "\\" + subDirectory.Name, target + "\\" + subDirectory.Name);
                    }
                }
                else
                {
                    foreach (var file in files)
                    {
                        File.Copy(source + "\\" + file, target + "\\" + file, true);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Kaynak ve/veya hedef klasörün yolu bulunamadı", "Uyarı");
            }
            catch (IOException)
            {
                MessageBox.Show("Dosya kullanılırken kopyalama yapamazsınız", "Uyarı");
            }
            catch
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu", "Uyarı");
            }
        }
    }
}
