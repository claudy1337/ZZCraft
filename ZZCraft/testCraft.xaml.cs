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
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace ZZCraft
{
    /// <summary>
    /// Логика взаимодействия для testCraft.xaml
    /// </summary>
    public partial class testCraft : Window
    {
       public Model.BDCraftZZEntities2 db = new Model.BDCraftZZEntities2();
        public testCraft()
        {
            InitializeComponent();
            listView1.ItemsSource = db.CraftRes.ToList();
            listView.ItemsSource = db.InitialRes.ToList();
            //listv.ItemsSource = db.Recipe.ToList();

        }
        
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
