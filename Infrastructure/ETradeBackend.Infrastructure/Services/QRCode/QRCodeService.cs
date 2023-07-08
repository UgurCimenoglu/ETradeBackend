using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using QRCoder;

namespace ETradeBackend.Infrastructure.Services.QRCode
{
    public class QRCodeService : IQRCodeService
    {
        public byte[] GenerateQRCodeAsync(string text)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(data);
            var byteGraphis = qrCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
            return byteGraphis;
        }
    }
}
