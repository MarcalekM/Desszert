using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Desszert;

namespace Desszert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dessert> desszertek = new();
        public MainWindow()
        {
            InitializeComponent();
            using StreamReader sr = new(
                path: @"..\..\..\src\palma.txt",
                encoding: UTF32Encoding.UTF8);
            while (!sr.EndOfStream) desszertek.Add(new(sr.ReadLine()));
            Ajanlat.Text = "Mai ajánlatunk: " + desszertek[25].Name;
            Dijazott.Text = desszertek.Where(d => d.Awarded == true).Count().ToString() + " féle díjnyertes édességből választhat!";
            LegdragabbNev.Text = desszertek.MaxBy(d => d.Price).Name.ToString();
            LegolcsobbNev.Text = desszertek.MinBy(d => d.Price).Name.ToString();
        }
    }
}