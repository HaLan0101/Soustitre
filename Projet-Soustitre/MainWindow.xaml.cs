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


namespace Projet_Soustitre
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FichierSoustitre f = new FichierSoustitre();

            f.ReadFile();

            Task t = f.GetSrt(this);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MediaElement1.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MediaElement1.Stop();
        }
        
        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            MediaElement1.Source = new Uri(filename);

            MediaElement1.LoadedBehavior = MediaState.Manual;
            MediaElement1.UnloadedBehavior = MediaState.Manual;
            MediaElement1.Play();
        }
        public void Update_Text(string soustittre)
        {
            TextBlock.Text = soustittre;
        }
       
    }
}
