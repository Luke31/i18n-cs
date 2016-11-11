using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLocalizationExtension.Extensions;
using WPFLocalizationExtension.Loc;
using WPFLocalizeExtension.Engine;

namespace WPFLocalizationExtension
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;

            //Set Extension Culture to System culture:
            LocalizeDictionary.Instance.Culture = new CultureInfo(CultureInfo.CurrentUICulture.Name);
            //LocalizeDictionary.Instance.Culture = new CultureInfo("ja-JP");

            InitializeComponent();

            var txtblk = new TextBlock()
            {
                Text = LocResources.helloWorld,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            
            var image = new Image
            {
                Source = LocResources.img.ConvertToBitmapImage(), //Call to custom extension Method to convert Bitmap to BitmapImage
                //Source = new BitmapImage(new Uri("Images/en.png", UriKind.RelativeOrAbsolute)), //Previous
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Stretch = Stretch.None
            };

            contentGrid.Children.Add(image);
            contentGrid.Children.Add(txtblk);
        }
    }
}
