using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using ZZCraft.Model.Connection;
using ZZCraft.item;
using System.Collections.ObjectModel;



namespace ZZCraft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Model.BDCraftZZEntities2 db = new Model.BDCraftZZEntities2();
        private DispatcherTimer _timer;

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(TimeSpan), typeof(MainWindow), new PropertyMetadata(default(TimeSpan)));

        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(
            "Seconds", typeof(int), typeof(MainWindow), new PropertyMetadata(default(int)));

        public MainWindow()
        {
            InitializeComponent();
            Refresh();
            Seconds = 10;
            Time = TimeSpan.FromSeconds(Seconds);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
            
        }
        public void Refresh()
        {
            listView1.ItemsSource = null;
            listView1.ItemsSource = db.InitialDrop.ToList();
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }

        private void BrndItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BrndItem.IsEnabled = true;
                _timer.Stop();
                Time = TimeSpan.FromSeconds(Seconds);
                _timer.Start();
                Random random = new Random();
                int resurs = random.Next(1, 19);
                int counts = random.Next(1, 4);
                Model.InitialRes initial = BDConnection.connection.InitialRes.FirstOrDefault(u => u.id == resurs);
                CountRnd.Text = counts.ToString();
                imageRnd.Source = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
                NameRnd.Text = initial.Name.ToString();


                Model.InitialDrop usrInvent = new Model.InitialDrop() { idInitialRes = initial.id, Count= counts};
                
                db.InitialDrop.Add(usrInvent);
                db.SaveChanges();
                
               
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Refresh();
            

        }
        public int Seconds
        {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            Time = Time.Subtract(TimeSpan.FromSeconds(1));
            if (Time == TimeSpan.Zero)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                
            }
        }

        public ObservableCollection<Initial> MyInitial { get; set; }

        private ObservableCollection<Initial> GetEvents()
        {
            return new ObservableCollection<Initial>
            {
                
               
            };
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
