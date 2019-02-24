package com.keydap.whistle;

import java.security.MessageDigest;

public class Token {
    private String id;
    private long createdAt;
    private String value;
    private String issuedBy; // organization
    private String issuedTo; // email address
    private String appSrvUrl; // URL of the app server for notifications
    //private Totp totp;

    public Token(String value, String issuedBy, String issuedTo)
    {
        this(value,issuedBy,issuedTo,null);
    }

    public Token(String value, String issuedBy, String issuedTo, String appSrvUrl) {
        this.createdAt = System.currentTimeMillis();
        this.value = value;
        this.issuedBy = issuedBy;
        this.issuedTo = issuedTo;
        this.appSrvUrl = appSrvUrl;

        try {
            MessageDigest sha = MessageDigest.getInstance("SHA256");
            byte[] buf = sha.digest(value.getBytes("utf-8"));
            for (int i = 0; i < 8; i++) {
                this.id += String.format("%x", buf[i]);
            }

            //byte[] totpBytes = Base.ToBytes(value);
            //totp = new Totp(totpBytes, step:30);
        }
        catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    public String NextVal() {
        return "";//totp.ComputeTotp(DateTime.UtcNow);
    }

    public String ToString() {
        return this.id;
    }

    //otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example
    public static Token createFromTotpUrl(String totpUrl) {
//        Token t = null;
//        try {
//            Uri uri = new Uri(totpUrl);
//            uri.IsWellFormedOriginalString();
//            NameValueCollection nvc = HttpUtility.ParseQueryString(uri.Query);
//        } catch (Exception e) {
//            var x = e;
//            //Console.WriteLine(e.Message);
//        }
        return null;
    }
}
