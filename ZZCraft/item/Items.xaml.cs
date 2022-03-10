using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZZCraft.item
{
    /// <summary>
    /// Логика взаимодействия для Items.xaml
    /// </summary>
    public partial class Items : UserControl
    {
        public Items()
        {
            InitializeComponent();
        }
        internal ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        internal static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(Items));

        internal string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        internal static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Items));

        public string Count
        {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(string), typeof(Items));
    }
}
