using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZSoft.Util
{
    public class TwoDimensionCode
    {
        public static string buildCode(string name, string position, string mobile1, string mobile2, string telephone, string fax, string email, string CompanyName, string Website, string Address)
        {
            try
            {
                StringBuilder card = new StringBuilder();
                card.Append("BEGIN:VCARD");
                card.Append("\r\nFN:" + name.Replace("&nbsp;",""));
                card.Append("\r\nTITLE:" + position.Replace("&nbsp;", ""));
                card.Append("\r\nORG:"+ CompanyName.Replace("&nbsp;", ""));
                card.Append("\r\nTEL;CELL:" + mobile1.Replace("&nbsp;", ""));
                card.Append("\r\nTEL;pref:" + mobile2.Replace("&nbsp;", ""));//手机创建只会一个手机号，一个固话、、表示多个电话中最喜欢使用的电话，不会显示在手机创建联系人里
                card.Append("\r\nTEL;WORK:" + telephone.Replace("&nbsp;", ""));
                card.Append("\r\nTEL;WORK;FAX:" + fax.Replace("&nbsp;", ""));
                card.Append("\r\nADR;WORK:"+ Address.Replace("&nbsp;", ""));
                card.Append("\r\nEMAIL;WORK:" + email.Replace("&nbsp;", ""));
                //card.Append("\r\nURL:"+Website);
                card.Append("\r\nX-QQ:");
                card.Append("\r\nNOTE:");
                card.Append("\r\nPHOTO;ENCODING=b;TYPE=JPEG:");
                card.Append("\r\nEND:VCARD\r\n");
                string xdpath = GetTwoDimensionCode(card.ToString(), string.Empty, 250, 250, "微软雅黑");

                //string uploadDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                //string xdpath = $"/Resource/DocumentFile/QRcode/123.jpg";
                //string path = System.Web.HttpContext.Current.Server.MapPath(xdpath);//类库带全部命名空间，绝对路径
                //b.Save(path);
                return xdpath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetTwoDimensionCode(string strSource,
            string text, int width, int height, string fontName)
        {
            using (Bitmap bmp = new Bitmap(width, height))
            {
                // 从image创建 Graphics对象
                Graphics objGraphics = Graphics.FromImage(bmp);
                // 填上背景色
                objGraphics.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
                // 
                ThoughtWorks.QRCode.Codec.QRCodeEncoder qrCodeEncoder =
                    new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
                // 设置编码方法
                qrCodeEncoder.QRCodeEncodeMode =
                    ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                // 设置大小
                qrCodeEncoder.QRCodeScale = 3;
                // 适用于信息量较少的情形，图像越小保存的信息量越少
                // qrCodeEncoder.QRCodeScale = 4;
                // 设置版本
                qrCodeEncoder.QRCodeVersion = 0;
                // 设置错误校验的级别，正因为二维码有纠错能力，才能够加入logo
                qrCodeEncoder.QRCodeErrorCorrect =
                    ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
                Image image = qrCodeEncoder.Encode(strSource, Encoding.GetEncoding("utf-8"));
                // 写入二维码
                int x = (int)(width - image.Width) / 2;
                int y = (int)(height - image.Height) / 2;
                objGraphics.DrawImage(image, new Point(x, y));
                //// 添加Logo图标
                //image = TwoDimensionCodeNameCard.Properties.Resources.card;
                //x = (int)(width - image.Width) / 2;
                //y = (int)(height - image.Height) / 2;
                //objGraphics.DrawImage(image, new Point(x, y));
                // 写入字符串
                //objGraphics.DrawString(text, new Font(fontName, 13, FontStyle.Bold), 
                //    Brushes.Black, new PointF(43, 15));

                //bmp.Save(System.Web.HttpContext.Current.Server.MapPath("/Resource/DocumentFile/QRcode/123.jpg"));//把jpg转成bmp

                string uploadDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                string xdpath = $"/Resource/DocumentFile/QRcode/{uploadDate}.jpg";
                string path = System.Web.HttpContext.Current.Server.MapPath(xdpath);//类库带全部命名空间，绝对路径
                bmp.Save(path);
                objGraphics.Dispose();
                bmp.Dispose();


                return xdpath;
            }

        }
    }
}
