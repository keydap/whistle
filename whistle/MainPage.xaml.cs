using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace whistle
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
            StackLayout topLayout = (Content as StackLayout);
            StackLayout otpLayout = (StackLayout)((ScrollView)topLayout.Children[1]).Content;
            addTokens(otpLayout);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"clicked = {count}";
        }

        void addTokens(StackLayout otpLayout)
        {
            int count = 27;
            for(int i=0; i < count; i++)
            {
                Label l = new Label();
                l.Text = String.Format("{0:000000}", i);
                otpLayout.Children.Add(l);
            }
        }
    }
}
