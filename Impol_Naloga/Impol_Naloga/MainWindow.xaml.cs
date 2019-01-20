using RondelCalculationDLL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Impol_Naloga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<Rondel> _rondels;
        private InputData _inputData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculateDLL_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("btnCalculateDLL_Click()");

            try
            {
                // get input data from form
                _inputData = GetInputData();
                if (_inputData == null)
                    return;

                //calculate rondels positions -> call dll
                _rondels = RondelCalculation.Calculate(_inputData.Width, _inputData.Length, _inputData.Radius, _inputData.MinDistanceBetween, _inputData.MinDistanceFromEdges);
                tbMaxNumberOfRondels.Text = _rondels.Count.ToString();

                // draw rondels
                DrawRondels();
            }
            catch (Exception ex)
            {
                _log.Error("Error in calculation.", ex);
            }
        }

        private void btnCalculateApi_Click(object sender, RoutedEventArgs e)
        {
            _log.Info("btnCalculateApi_Click()");

            try
            {
                //get input data from form
                _inputData = GetInputData();
                if (_inputData == null)
                    return;

                // calculate rondels positions -> call web api
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage result = httpClient.GetAsync($"http://localhost:9000/api/rondelcalculation?width={_inputData.Width.ToString().Replace(',', '.')}&length={_inputData.Length.ToString().Replace(',', '.')}&r={_inputData.Radius.ToString().Replace(',', '.')}&minDistanceBetween={_inputData.MinDistanceBetween.ToString().Replace(',', '.')}&minDistanceFromEdges={_inputData.MinDistanceFromEdges.ToString().Replace(',', '.')}").Result;

                    // deserialize from json
                    _rondels = JsonConvert.DeserializeObject<List<Rondel>>(result.Content.ReadAsStringAsync().Result);
                    tbMaxNumberOfRondels.Text = _rondels.Count.ToString();

                    // draw rondels
                    DrawRondels();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Prišlo je do napake pri izračunu preko API-ja. Preverite, ali je aplikacija z API-jem zagnana.", "Napaka", MessageBoxButton.OK, MessageBoxImage.Warning);
                _log.Error("Error in calculation.", ex);
            }
        }

        private InputData GetInputData()
        {
            try
            {
                // parse data from textboxes
                double width = ParseDoubleFromText(tbWidth.Text.Replace('.', ','));
                double length = ParseDoubleFromText(tbLength.Text.Replace('.', ','));
                double r = ParseDoubleFromText(tbRadius.Text.Replace('.', ','));
                double minDistanceBetween = ParseDoubleFromText(tbMinDistanceBetween.Text.Replace('.', ','));
                double minDistanceFromEdges = ParseDoubleFromText(tbMinDistanceFromEdges.Text.Replace('.', ','));

                // if data could not be parsed or is invalid
                if (width <= 0 || length <= 0 || r <= 0 || minDistanceBetween < 0 || minDistanceFromEdges < 0)
                {
                    MessageBox.Show("Nepravilni vhodni podatki. Vrednosti morajo biti podane kot celoštevilske ali decimalne.", "Napaka", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }

                return new InputData { Width = width, Length = length, Radius = r, MinDistanceBetween = minDistanceBetween, MinDistanceFromEdges = minDistanceFromEdges };
            }
            catch (Exception ex)
            {
                _log.Error("Error getting input data.", ex);
                return null;
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

        private void DrawRondels()
        {
            try
            {
                // clear canvas
                cRondels.Children.Clear();
                double canvasHeight = cRondels.ActualHeight;
                double canvasWidth = cRondels.ActualWidth;

                // calculate ratio of canvas width & height and input data widt & height
                double ratioHeight = canvasHeight / _inputData.Width;
                double rationWidth = canvasWidth / _inputData.Length;

                double ratio = ratioHeight > rationWidth ? rationWidth : ratioHeight;

                // draw rectangle from input data
                Rectangle rectangle = new Rectangle()
                {
                    Width = _inputData.Length * ratio,
                    Height = _inputData.Width * ratio,
                    Stroke = Brushes.Blue,
                    Fill = Brushes.White
                };
                Canvas.SetLeft(rectangle, 0);
                Canvas.SetBottom(rectangle, 0);

                cRondels.Children.Add(rectangle);

                // draw ellipses (rondels) from result
                foreach (Rondel rondel in _rondels)
                {
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 2 * _inputData.Radius * ratio,
                        Height = 2 * _inputData.Radius * ratio,
                        Fill = Brushes.Red
                    };

                    ellipse.SetValue(Canvas.LeftProperty, rondel.X * ratio - ellipse.Width / 2);
                    ellipse.SetValue(Canvas.BottomProperty, rondel.Y * ratio - ellipse.Height / 2);

                    cRondels.Children.Add(ellipse);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error drawing rondels.", ex);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_rondels == null)
                return;

            DrawRondels();
        }
    }
}
