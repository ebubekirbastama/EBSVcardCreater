using EbubekirBastamatxtokuma;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EBSvcardcreate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls=false;
            InitializeComponent();
        }
        Thread th; OpenFileDialog op;
        ArrayList vcard = new ArrayList();
        string dosya_yolu;
        private void button1_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog()==DialogResult.OK)
            {
                th = new Thread(bsl1);th.Start();
            }
        }
        private void bsl1()
        {
            BekraTxtOkuma.Txtİmport(op.FileName,listBox1,false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                th = new Thread(bsl2); th.Start();
            }
        }
        private void bsl2()
        {
            BekraTxtOkuma.Txtİmport(op.FileName, listBox2, false);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            th = new Thread(bsl3);th.Start();
        }
        private void bsl3()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                vcard.Add("BEGIN:VCARD");
                vcard.Add("VERSION:3.0");
                vcard.Add("FN:" + i + listBox2.Items[i].ToString());
                vcard.Add("N:"+"\""+ listBox2.Items[i].ToString() +"\""+ ";" + i + ";;;");
                vcard.Add("TEL;TYPE=CELL:" + listBox1.Items[i].ToString() + "");
                vcard.Add("END:VCARD");
            }
            dosya_yolu = Application.StartupPath + @"\" + "vcard.vcf";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < vcard.Count; i++)
            {
                sw.WriteLine(vcard[i].ToString());
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("Vcard Oluşturuldu.");
        }
    }
}
