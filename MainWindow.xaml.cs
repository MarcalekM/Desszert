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
            LegdragabbAdat.Content = desszertek.MaxBy(d => d.Price).Price.ToString() + "/" + desszertek.MaxBy(d => d.Price).Amount.ToString();
            LegolcsobbNev.Text = desszertek.MinBy(d => d.Price).Name.ToString();
            LegolcsobbAdat.Content = desszertek.MinBy(d => d.Price).Price.ToString() + "/" + desszertek.MinBy(d => d.Price).Amount.ToString();

            var f5 = desszertek.DistinctBy(d => d.Name).OrderBy(d => d.Name).ToList();
            using StreamWriter sw = new(
                path: @"..\..\..\src\lista.txt",
                append: false);
            foreach (var f in f5)
            {
                sw.WriteLine($"{f.Name} {f.Type}");
            }

            using StreamWriter sw2 = new(
                path: @"..\..\..\src\stat.txt",
                append: false);
            var f6 = desszertek.DistinctBy(d => d.Type).ToList();
            foreach (var f in f6)
            {
                sw2.WriteLine($"{f.Type} - {desszertek.Count(d => d.Type.Equals(f.Type))}");
            }

        }

        private void ujFelveletel_Click(object sender, RoutedEventArgs e)
        {
            if (ujNev.Text == "") MessageBox.Show("Nem adta meg a sütemény nevét!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            if (ujTipus.Text == "") MessageBox.Show("Nem adta meg a sütemény típusát!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            if (ujEgyseg.Text == "") MessageBox.Show("Nem adta meg a sütemény mértékegységét!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            if (ujAr.Text == "") MessageBox.Show("Nem adta meg a sütemény árát!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            if (ujNev.Text != "" && ujTipus.Text != "" && ujEgyseg.Text != "" && ujAr.Text != "")
            {
                var d = new Dessert(ujNev.Text, ujTipus.Text, (bool)ujDijazott.IsChecked, int.Parse(ujAr.Text), ujEgyseg.Text);
                using StreamWriter sw = new(
                path: @"..\..\..\src\cuki.txt",
                append: true);
                sw.WriteLine($"{d.Name};{d.Type};{d.Awarded};{d.Price};{d.Amount}");
                MessageBox.Show("A mentés sikeresen megtörtént", "Sikeses mentés", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void arAjanlat_Click(object sender, RoutedEventArgs e)
        {
            if (desszertek.Where(d => d.Type.Equals(ajanlat.Text)).Count() != 0)
            {
                var f7 = desszertek.Where(d => d.Type.Equals(ajanlat.Text)).ToList();
                using StreamWriter sw = new(
                path: @"..\..\..\src\ajanlat.txt",
                append: false);
                foreach (var f in f7)
                {
                    sw.WriteLine($"{f.Name} {f.Price} {f.Amount}");
                }
                MessageBox.Show($"{f7.Count} darab {ajanlat.Text} süti van, amik átlagára: {f7.Sum(f => f.Price) / f7.Count} Ft", "Eredmény", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else MessageBox.Show("Nincs ilyen típusú süti!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}