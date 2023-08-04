
namespace HappyBrowser.Controls
{
    public class CtlComboBoxItem
    {
        //文本内容
        public string Text;

        public string Value;

        //项图片
        public Image Img;

        //构造函数
        public CtlComboBoxItem(Image img, string text="", string value="")
        {
            Text = text;
            Img = img;
            Value=value;
        }

        //重写ToString函数，返回项文本
        public override string ToString()
        {
            return Text;
        }
    }
}
