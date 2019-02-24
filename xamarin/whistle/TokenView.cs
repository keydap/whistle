using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace whistle
{
    public class TokenView : ContentView
    {
        private Token token;
        private Label timeLabel;
        private Label otpLabel;
        private String curOtpVal;
        private int secCount = 29;

        public TokenView(Token token)
        {
            this.token = token;

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height = new GridLength(20)
                    },
                    new RowDefinition
                    {
                        Height = new GridLength(30)
                    },
                    new RowDefinition
                    {
                        Height = new GridLength(20)
                    }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition
                    {
                        Width = new GridLength(20)
                    },
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            grid.HorizontalOptions = LayoutOptions.CenterAndExpand;

            Label issuer = new Label();
            issuer.Text = token.IssuedBy;
            issuer.FontFamily = "MarkerFelt-Thin, Bold, 32";
            issuer.HorizontalOptions = LayoutOptions.Start;
            grid.Children.Add(issuer, 0, 1); // 1st row, 2nd column

            otpLabel = new Label();
            otpLabel.Text = token.NextVal();
            otpLabel.FontFamily = "MarkerFelt-Thin, Bold, 32";
            otpLabel.HorizontalOptions = LayoutOptions.Center;
            otpLabel.VerticalOptions = LayoutOptions.Center;
            otpLabel.ScaleX = 2.0;
            otpLabel.ScaleY = 2.0;
            grid.Children.Add(otpLabel, 1, 1); // 2nd row, 2nd column

            Label issuedTo = new Label();
            issuedTo.Text = token.IssuedTo;
            issuedTo.FontFamily = "MarkerFelt-Thin, Bold, 32";
            issuedTo.HorizontalOptions = LayoutOptions.Start;
            issuedTo.VerticalOptions = LayoutOptions.End;
            grid.Children.Add(issuedTo, 2, 1); // 3rd row, 2nd column

            timeLabel = new Label();
            timeLabel.Text = "29s";
            timeLabel.FontFamily = "MarkerFelt-Thin, Bold, 32";
            timeLabel.HorizontalOptions = LayoutOptions.Center;
            timeLabel.VerticalOptions = LayoutOptions.End;
            grid.Children.Add(timeLabel, 2, 2); // 3rd row, 3rd column

            Switch switchBox = new Switch();
            switchBox.VerticalOptions = LayoutOptions.Center;
            switchBox.IsVisible = true;
            //grid.Children.Add(switchBox, 1, 0); // 2rd row, 1st column

            Content = grid;
            Device.StartTimer(TimeSpan.FromSeconds(1), genNewOtp);
        }

        private bool genNewOtp()
        {
            secCount--;
            if (secCount == 0)
            {
                curOtpVal = token.NextVal();
                secCount = 29;
                timeLabel.Text = "29s";
                otpLabel.Text = curOtpVal;
            }
            else
            {
                timeLabel.Text = secCount + "s";
            }
            Console.WriteLine("seconds left {0}", secCount);
            return true;
        }
    }
}
