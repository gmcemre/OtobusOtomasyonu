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
    public partial class KayitFormu : Form
    {
        public KayitFormu()
        {
            InitializeComponent();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (txtIsim.Text == string.Empty)
                errorProvider1.SetError(txtIsim, "Boş Geçilemez");
            if (txtSoyisim.Text == string.Empty)
                errorProvider1.SetError(txtSoyisim, "Boş Geçilemez");
            if (txtIsim.Text != string.Empty && txtSoyisim.Text != string.Empty)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
