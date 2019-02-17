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
        Style labelStyle;
        public MainPage()
        {
            InitializeComponent();
            labelStyle = new Style(typeof(Label))
            {
                Setters =
                    {
                        new Setter
                        {
                            Property = View.HorizontalOptionsProperty,
                            Value = LayoutOptions.Center
                        },
                        new Setter
                        {Property = View.VerticalOptionsProperty,
                            Value = LayoutOptions.CenterAndExpand
                        },
                        new Setter
                        {
                            Property = Button.BorderWidthProperty,
                            Value = 3
                        },
                        new Setter
                        {
                            Property = Button.TextColorProperty,
                            Value = Color.Red
                        },
                        new Setter
                        {
                            Property = Button.FontSizeProperty,
                            Value = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                        }
                       // new Setter
                       // {
                       //     Property = VisualElement.BackgroundColorProperty,
                       //     Value = Device.OnPlatform(Color.Default,
                       //                               Color.FromRgb(0x40, 0x40, 0x40),
                       //                               Color.Default)
                       // },
                       // new Setter
                       // {
                       //     Property = Button.BorderColorProperty,
                       //     Value = Device.OnPlatform(Color.Default,
                       //                               Color.White,
                       //                               Color.Black)
                       //}
                    }
            };

        StackLayout topLayout = (Content as StackLayout);
            StackLayout otpLayout = (StackLayout)((ScrollView)topLayout.Children[1]).Content;
            tgr.Command = new Command(OnTapped);
            addTokenLabels(otpLayout);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"clicked = {count}";
        }

        void addTokenLabels(StackLayout otpLayout)
        {
            int count = 7;
            for(int i=0; i < count; i++)
            {
                var key = KeyGeneration.GenerateRandomKey(20);
                var totpVal = Base32Encoding.ToString(key);
                Token t = new Token(totpVal, "k", "k");
                lstTokens.Add(t);
                Label l = new Label();
                l.Style = labelStyle;
                l.GestureRecognizers.Add(tgr);
                l.Text = String.Format("{0}", t.NextVal());
                otpLayout.Children.Add(l);
            }
        }

        void OnTapped(Object source)
        {
            Console.WriteLine("Tapped on " + source);
        }
    }
}
