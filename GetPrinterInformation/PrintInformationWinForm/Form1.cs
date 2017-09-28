using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintInformationWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = new System.Drawing.Printing.PrintDocument();
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var sb = new StringBuilder();
                var printerSettings = printDialog1.Document.PrinterSettings;
                sb.AppendLine(String.Format("{0}: {1}", "Name", printerSettings.PrinterName));

                sb.AppendLine("Paper Sources:");
                foreach (PaperSource prop in printerSettings.PaperSources)
                {
                    sb.AppendLine(String.Format("\tSourceName: {0}\n\t\tKind: {1}\n\t\tRawKind: {2}", prop.SourceName, prop.Kind, prop.RawKind));
                }

                sb.AppendLine("Paper Sources:");
                foreach (PaperSize prop in printerSettings.PaperSizes)
                {
                    sb.AppendLine(String.Format("\tPaperName: {0}\n\t\tKind: {1}\n\t\tRawKind: {2}\n\t\tHeight: {3}\n\t\tWidth: {4}", prop.PaperName, prop.Kind, prop.RawKind, prop.Height, prop.Width));
                }
                this.richTextBox1.Text = sb.ToString();
            }
        }
    }
}
