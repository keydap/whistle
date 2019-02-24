using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OtpNet;

namespace whistle
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        TapGestureRecognizer tgr = new TapGestureRecognizer();
        List<Token> lstTokens = new List<Token>();

        public MainPage()
        {
            InitializeComponent();
            StackLayout topLayout = (Content as StackLayout);
            StackLayout otpLayout = (StackLayout)((ScrollView)topLayout.Children[1]).Content;
            tgr.Command = new Command(OnTapped);
            addTokenLabels(otpLayout);
        }

        void readFromQRCode(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"clicked = {count}";
//#if __ANDROID__
//                // Initialize the scanner first so it can track the current context
//                MobileBarcodeScanner.Initialize (Application);
//#endif

            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>()
            {
                ZXing.BarcodeFormat.QR_CODE
            };
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var task = scanner.Scan(options);

            if (task.IsCompleted)
            {
                Console.WriteLine("Scanned Barcode: " + task.Result.Text);
            }
        }

        void addTokenLabels(StackLayout otpLayout)
        {
            int count = 2;
            for(int i=0; i < count; i++)
            {
                var key = KeyGeneration.GenerateRandomKey(20);
                var totpVal = Base32Encoding.ToString(key);
                Token t = new Token(totpVal, "k"+i, "k"+i);
                lstTokens.Add(t);

                TokenView tv = new TokenView(t);
                otpLayout.Children.Add(tv);
                //String fontFamily = "Lobster-Regular"; // iOS
                //if(Device.RuntimePlatform == Device.Android)
                //{
                //    fontFamily = "Lobster-Regular.ttf#Lobster-Regular";
                //}
                //Label l = new Label();
                //l.FontFamily = fontFamily;
                ////l.Style = labelStyle;
                //l.GestureRecognizers.Add(tgr);
                //l.Text = String.Format("{0}", t.NextVal());
                //otpLayout.Children.Add(l);
            }
        }

        void OnTapped(Object source)
        {
            Console.WriteLine("Tapped on " + source);
        }
    }
}
