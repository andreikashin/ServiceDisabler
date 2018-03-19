using System;
using System.Windows;
using System.Windows.Controls;

namespace ServiceDisabler
{
    /// <summary>
    /// Interaction logic for StopSettingsWindow.xaml
    /// </summary>
    public partial class StopSettingsWindow : Window
    {
        public StopSettingsWindow()
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

        //private void ApplyScheduleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var date = Calendar.SelectedDate;
        //    var hour = Convert.ToInt32(Hour.Text); 
        //    var minute = Convert.ToInt32(Minute.Text); 
        //    var second = Convert.ToInt32(Second.Text);

            

        //    var time = sender;
        //}

        //private void CancelScheduleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
