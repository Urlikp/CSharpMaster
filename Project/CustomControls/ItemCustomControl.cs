using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CSharpMaster.CustomControls
{
    public class ItemCustomControl : ListBoxItem
    {
        static ItemCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemCustomControl), new FrameworkPropertyMetadata(typeof(ItemCustomControl)));
        }

        public string ItemIcon
        {
            get => (string)GetValue(ItemIconProperty);
            set => SetValue(ItemIconProperty, value);
        }

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public string ItemDetail
        {
            get => (string)GetValue(ItemDetailProperty);
            set => SetValue(ItemDetailProperty, value);
        }

        public static readonly DependencyProperty ItemIconProperty =
            DependencyProperty.Register("ItemIcon", typeof(string), typeof(ItemCustomControl));
        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(ItemCustomControl));
        public static readonly DependencyProperty ItemDetailProperty =
            DependencyProperty.Register("ItemDetail", typeof(string), typeof(ItemCustomControl));
    }

    public class ItemIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(value as string ?? string.Empty));

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
