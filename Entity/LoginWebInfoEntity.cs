
namespace HappyBrowser.Entity
{
    public class LoginWebInfoEntity
    {
        public LoginWebInfoEntity() {
            this.IsLogin = false;
        }

        public LoginWebInfoEntity(bool isLogin,string url) 
        {
            this.IsLogin=isLogin;
            this.Url= url; ;
        }

        public bool IsLogin {
        get; set;
        }

        public string Url { get; set; }

        public string AccountId { get; set; }
        public string AccountName { get; set; }

        public string AccountValue { get; set; }

        public string passwordId { get; set; }
        public string passwordName { get; set; }
        public string passwordValue { get; set; }
    }
}
