using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;

namespace son_atik_takip
{
    public partial class QRCodeForm : Form
    {
        public QRCodeForm(string qrData)
        {
            InitializeComponent();
            GenerateQRCode(qrData);
        }

        private void GenerateQRCode(string data)
        {
            var writer = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 300,
                    Width = 300,
                    Margin = 1
                }
            };
            Bitmap bitmap = writer.Write(data);
            picQRCode.Image = bitmap;
        }
    }
}
