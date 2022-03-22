﻿using System;
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
        string[] itemList = new string[3] { "One", "Two", "Three"};

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
            listView2.ItemsSource = null;
            listView1.ItemsSource = db.CraftDrop.ToList();
            listView2.ItemsSource = db.InitialDrops.ToList();


        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }
        private void BrndItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            try
            {
                _timer.Stop();
                Time = TimeSpan.FromSeconds(Seconds);
                _timer.Start();

                BrndItem.IsEnabled = true;
                Random random = new Random();
                int resurs = random.Next(1, 19);
                int counts = random.Next(1, 4);

                Model.InitialDrops findInitialDrop = db.InitialDrops.FirstOrDefault(a => a.iDInitialRes == resurs);
                Model.InitialRes initial = BDConnection.connection.InitialRes.FirstOrDefault(u => u.id == resurs);
                Model.InitialDrops usrInvent = new Model.InitialDrops() { iDInitialRes = initial.id, Counts = counts };
                CountRnd.Text = counts.ToString();
                imageRnd.Source = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
                NameRnd.Text = initial.Name.ToString();

                if (findInitialDrop != null)
                {
                    findInitialDrop = db.InitialDrops.FirstOrDefault(o => o.iDInitialRes == resurs);
                    if (findInitialDrop != null)
                    {
                        findInitialDrop.Counts += counts;
                        db.SaveChanges();
                        MessageBox.Show("double item");
                        Refresh();
                        //return;
                    }
                }
                else
                {
                    db.InitialDrops.Add(usrInvent);
                    db.SaveChanges();
                    Refresh();
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                new Initial { },
               
            };
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
            

        }

        private void BCraft_Click(object sender, RoutedEventArgs e)
        {
            testCraft testCraft = new testCraft();
            testCraft.Show();
            //listView1.SelectedItem = 
            //Model.InitialRes initialRes = db.InitialRes.FirstOrDefault(a => a.id == );
            //listView1.SelectedItem = db.InitialDrops;
            //itemOne.ImageSource = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
        }
        int count = 0;
        private void choiseItem_Click(object sender, RoutedEventArgs e)
        {
            count++;
            string[] teamUserList = new string[3] { "One", "Two", "Three" };

            for (int i = 0; i < count; i++)
            {
                if (listView1.SelectedIndex != -1)
                {
                    teamUserList[i] = listView1.SelectedItem.ToString();
                    switch (count)
                    {
                        case 1:
                            txtOne.Text = teamUserList[i];
                            break;
                        case 2:
                            txtTwo.Text = teamUserList[i];
                            break;
                        case 3:
                            txtThree.Text = teamUserList[i];
                            break;
                        default:
                            MessageBox.Show("фул item");
                            count = 0;
                            break;
                    }

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testCraft testCraft = new testCraft();
            testCraft.Show();
        }

        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
