using System;
using System.Windows;

namespace Impol_Naloga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("btnCalculate_Click()");

            try
            {
                double width = ParseDoubleFromText(tbWidth.Text.Replace('.', ','));
                double length = ParseDoubleFromText(tbLength.Text.Replace('.', ','));
                double r = ParseDoubleFromText(tbRadius.Text.Replace('.', ','));
                double minDistanceBetween = ParseDoubleFromText(tbMinDistanceBetween.Text.Replace('.', ','));
                double minDistanceFromEdges = ParseDoubleFromText(tbMinDistanceFromEdges.Text.Replace('.', ','));

                if (width < 0 || length < 0 || r < 0 || minDistanceBetween < 0 || minDistanceFromEdges < 0)
                    MessageBox.Show("Nepravilni vhodni podatki. Vrednosti morajo biti podane kot celoštevilske ali decimalne.", "Napaka", MessageBoxButton.OK, MessageBoxImage.Warning);

                int temp = RondelCalculationDLL.RondelCalculation.Calculate(1.0, 1.0, 1.0, 1.0, 1.0);
                tbMaxNumberOfRondels.Text = temp.ToString();
            }
            catch (Exception ex)
            {
                _log.Error("Error in calculation.", ex);
            }
        }

        private double ParseDoubleFromText(string text)
        {
            try
            {
                return double.Parse(text);
            }
            catch (Exception ex)
            {
                _log.Error($"Error parsing double from text:[{text}].", ex);
                return double.MinValue;
            }
        }
    }
}
