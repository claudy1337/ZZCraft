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
using System.Reflection;
namespace ZZCraft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static Model.BDCraftZZEntities2 db = new Model.BDCraftZZEntities2();
        string[] itemList = new string[3];
        public ObservableCollection<Model.InitialDrops> MyInitial = new ObservableCollection<Model.InitialDrops>();
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
        private void BrndItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            try
            {
                _timer.Stop();
                Time = TimeSpan.FromSeconds(Seconds);
                _timer.Start();
                Refresh();
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
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(); 
        }
        private void BCraft_Click(object sender, RoutedEventArgs e)
        {
            
            var one = itemList[0];
            var two = itemList[1];
            var three = itemList[2];
            Model.Recipe recipe = db.Recipe.FirstOrDefault(r => r.objectOne.ToString() ==one  && r.objectTwo.ToString() == two && r.objectThree.ToString() ==three);
            Model.CraftRes craftRes = db.CraftRes.FirstOrDefault(c => c.idRecipe == recipe.id);
            Model.CraftDrop craftDrop = new Model.CraftDrop() { idCraftDrop = craftRes.id, Count = 1};
            if (recipe != null)
            {
                MessageBox.Show(recipe.id.ToString());
                resultItem.Source = new BitmapImage(new Uri(craftRes.img, UriKind.RelativeOrAbsolute));
                txtResult.Text = craftRes.Name.ToString();
                db.CraftDrop.Add(craftDrop);
                db.SaveChanges();
                Refresh();
            }
            
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testCraft testCraft = new testCraft();
            testCraft.Show();
        }
      
        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (Model.InitialDrops)listView2.SelectedItem;
           // MessageBox.Show(item.ID.ToString());
            
            Model.InitialDrops initialDrops = db.InitialDrops.FirstOrDefault(i => i.ID == item.ID);
            Model.InitialRes initial = db.InitialRes.FirstOrDefault(b => b.id == initialDrops.iDInitialRes);
            MyInitial.Add(item);

            if (MyInitial.Count == 1 && initial != null)
            {
                itemOne.Source = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
                txtOne.Text = initial.Name;
                itemList[0] = initialDrops.iDInitialRes.ToString();
                
            }
            else if (MyInitial.Count == 2 && initial != null)
            {
                itemTwo.Source = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
                txtTwo.Text = initial.Name;
                itemList[1] = initialDrops.iDInitialRes.ToString();
            }
            else if (MyInitial.Count == 3 && initial != null)
            {
                itemThree.Source = new BitmapImage(new Uri(initial.img, UriKind.RelativeOrAbsolute));
                txtThree.Text = initial.Name;
                itemList[2] = initialDrops.iDInitialRes.ToString();
            }
           
            MessageBox.Show(MyInitial.Count.ToString());
        }
    }
}
