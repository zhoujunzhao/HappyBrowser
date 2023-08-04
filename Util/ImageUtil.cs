using System.IO;
using System.Text.RegularExpressions;

namespace HappyBrowser.Util
{
    public class ImageUtil
    {
        #region 图片Base64互转
        public static Image ConvertBase64ToImage(string base64Imag)
        {
            var base64Data = Regex.Match(base64Imag, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            byte[] bytes = Convert.FromBase64String(base64Data);
            Image img;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                img = new Bitmap(ms);
            }
            return img;
        }

        public static string ConvertImageToBase64(Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return "data:image/png;base64," + Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return "";
            }
        }
        #endregion 图片Base64互转
    }
}
