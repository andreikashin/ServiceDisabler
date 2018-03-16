using System;
using System.Windows;
using System.Windows.Controls;

namespace ServiceDisabler.View
{
    /// <summary>
    /// Interaction logic for ServiceProperties.xaml
    /// </summary>
    public partial class ServiceProperties
    {
        public ServiceProperties()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            tb?.SelectAll();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb == null) return;
            int hh;
            if (!int.TryParse(tb.Text, out hh))
            {
                tb.Text = "00";
                return;
            }
            if (hh < 0 || hh > 23)
            {
                tb.Text = "00";
                return;
            }

            if (tb.Text.Length == 1)
            {
                tb.Text = "0" + tb.Text;
            }
        }

        private void ApplyScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CancelScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
