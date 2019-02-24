using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using OtpNet;
using System.Web;
using System.Collections.Specialized;

namespace whistle
{
    public class Token
    {
        private String id;
        private long createdAt;
        private String value;
        private String issuedBy; // organization
        private String issuedTo; // email address
        private String appSrvUrl; // URL of the app server for notifications
        private Totp totp;

        public Token(string value, string issuedBy, string issuedTo) : this(value, issuedBy, issuedTo, null)
        {
        }

        public Token(string value, string issuedBy, string issuedTo, string appSrvUrl)
        {
            this.createdAt = DateTime.UtcNow.Ticks;
            this.value = value;
            this.issuedBy = issuedBy;
            this.issuedTo = issuedTo;
            this.appSrvUrl = appSrvUrl;

            SHA256 sha = SHA256.Create();
            byte[] buf = sha.ComputeHash(Encoding.UTF8.GetBytes(value.ToCharArray()));
            for (int i = 0; i < 8; i++)
            {
                this.id += string.Format("{0:x}", buf[i]);
            }

            byte[] totpBytes = Base32Encoding.ToBytes(value);
            totp = new Totp(totpBytes, step: 30);
        }

        public string Id { get; }

        public long CreatedAt { get; }

        public string IssuedBy { get; }

        public string IssuedTo { get; }

        public string NextVal()
        {
            return totp.ComputeTotp(DateTime.UtcNow);
        }

        override
        public String ToString()
        {
            return this.id;
        }

        static void Main(string[] args)
        {
            Token t = new Token("abcd", "k", "k");
            Console.WriteLine(t.ToString());
        }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   id == token.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<string>.Default.GetHashCode(id);
        }

        public static Token createFromTotpUrl(string totpUrl)
        {
            Token t = null;
            try
            {
                Uri uri = new Uri(totpUrl);
                uri.IsWellFormedOriginalString();
                NameValueCollection nvc = HttpUtility.ParseQueryString(uri.Query);
            }
            catch(Exception e)
            {
                var x = e;
                //Console.WriteLine(e.Message);
            }
            return t;
        }
    }
}