using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace S3_HW_26._09._21
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Grid dancePB = new Grid();
            var countPB = int.Parse(tbCountPB.Text);
            for (int i = 0; i < countPB; i++)
            {
                dancePB.ColumnDefinitions.Add(new ColumnDefinition()); 
                ProgressBar pb = new ProgressBar();
                pb.Minimum = 0;
                pb.Maximum = 100;
                pb.Orientation = Orientation.Vertical;
                Grid.SetColumn(pb, i);                
                dancePB.Children.Add(pb);
            }

            foreach (var item in dancePB.Children)
            {
                if(item is ProgressBar)
                {
                    Thread myThread = new Thread(new ParameterizedThreadStart(SetPB));
                    myThread.Start(item);
                }
            }

            Grid.SetRow(dancePB, 1);
            Grid.SetColumnSpan(dancePB, 3);
            MainGrid.Children.Add(dancePB);                    
            
        }
        public void SetPB(object obj)
        {
            TimerCallback tm = new TimerCallback(RandomValue);
            Timer timer = new Timer(tm, obj, 0, 1000);
        }

        public static void RandomValue(object obj)
        {
            Random rnd = new Random();
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            (obj as ProgressBar).Background = brush;
            (obj as ProgressBar).Value = rnd.Next(0, 100);
        }      
    }
}
