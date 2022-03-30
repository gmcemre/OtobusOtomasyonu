using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobusOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmbOtobus_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbOtobus.Text)
            {
                case "Travego":
                    KoltukDoldur(8, false);
                    break;
                case "Setra":
                    KoltukDoldur(12, true);
                    break;
                case "Neoplan":
                    KoltukDoldur(10, false);
                    break;
                default:
                    break;
            }
        }

        void KoltukDoldur(int sira, bool arkaBesliMi)
        {
        yavaslat:
            foreach (Control item in this.Controls)
            {
                Button btn = item as Button;
                if (item is Button)
                {
                    if (btn.Text == "KAYDET")
                    {
                        continue;
                    }
                    else
                    {
                        this.Controls.Remove(item);
                        goto yavaslat;
                    }
                }
            }
            int koltukNo = 1;
            for (int i = 0; i < sira; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == sira / 2 && j > 2)
                    {
                        continue;
                    }
                    if (arkaBesliMi)
                    {
                        if (i != sira - 1 && j == 2)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (j == 2)
                            continue;
                    }

                    Button koltuk = new Button();
                    koltuk.Height = koltuk.Width = 40;
                    koltuk.Top = 30 + (i * 45);
                    koltuk.Left = 5 + (j * 45);
                    koltuk.Text = koltukNo.ToString();
                    koltukNo++;
                    koltuk.ContextMenuStrip = contextMenuStrip1;
                    koltuk.MouseDown += Koltuk_MouseDown;
                    this.Controls.Add(koltuk);
                }
            }
        }

        Button tiklanan;
        private void Koltuk_MouseDown(object sender, MouseEventArgs e)
        {
            tiklanan = sender as Button;
        }

        private void rezerveEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tiklanan.BackColor == Color.Pink || tiklanan.BackColor == Color.Blue)
            {
                MessageBox.Show("Seçmek istediğiniz koltuk dolu.Lütfen farklı bir koltuk seçiniz.");
                return;
            }
            if (cmbOtobus.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbOtobus, "Otobüs Seçiniz");
            }
            else if (cmbNereden.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbNereden, "Boş Geçilemez");
            }
            else if (cmbNereye.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbNereye, "Boş Geçilemez");
            }

            if (cmbOtobus.SelectedIndex == -1 || cmbNereden.SelectedIndex == -1 || cmbNereye.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen önce gerekli alanları doldurunuz.");
                return;
            }
            KayitFormu kayitFormu = new KayitFormu();
            DialogResult sonuc = kayitFormu.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = string.Format("{0} {1}", kayitFormu.txtIsim.Text, kayitFormu.txtSoyisim.Text);
                lvi.SubItems.Add(kayitFormu.mskdTelefon.Text);
                if (kayitFormu.rdbBay.Checked)
                {
                    lvi.SubItems.Add("BAY");
                    tiklanan.BackColor = Color.Blue;
                }
                if (kayitFormu.rdbBayan.Checked)
                {
                    lvi.SubItems.Add("BAYAN");
                    tiklanan.BackColor = Color.Pink;
                }
                lvi.SubItems.Add(cmbNereden.Text);
                lvi.SubItems.Add(cmbNereye.Text);
                lvi.SubItems.Add(tiklanan.Text);
                lvi.SubItems.Add(dtpTarih.Text);
                lvi.SubItems.Add(nudFiyat.Value.ToString());
                listView1.Items.Add(lvi);
            }
        }
    }
}
