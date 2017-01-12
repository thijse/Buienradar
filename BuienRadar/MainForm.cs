using System;
using System.Globalization;
using System.Windows.Forms;
using Buienradar;
using Buienradar.Utils;
using GoogleMaps.LocationServices;

namespace BuienRadar
{
    public partial class MainForm : Form
    {
        private readonly BuienRadar _buienRadar;
        private readonly GoogleLocationService _googleLocationService;
        private readonly WeersInformatie _weersInformatie;

        public MainForm()
        {
            InitializeComponent();
            _googleLocationService = new GoogleLocationService();
            _buienRadar = new BuienRadar(TimeSpan.FromMinutes(1));
            _weersInformatie = new WeersInformatie(TimeSpan.FromMinutes(1));

            LookupAddress();

            listBoxRainFallData.Items.Add("All data shown here courtesy of Buienradar");
            listBoxRainFallData.Items.Add("When using buienradar data, please provide link to buienradar.nl");
            listBoxRainFallData.Items.Add("");
        }



        private void LookupAddress()
        {
            MapPoint latLong = null;
            try
            {
                latLong = _googleLocationService.GetLatLongFromAddress(textBoxLocation.Text);
            }
            catch
            {
                return;
            }
            if (latLong == null) return;

            textBoxLattitude.Text = latLong.Latitude.ToString(CultureInfo.InvariantCulture);
            textBoxLongitude.Text = latLong.Longitude.ToString(CultureInfo.InvariantCulture);
        }

       

        private bool GetLatitudeLongitude(out double lattitude, out double longitude)
        {
            var result = double.TryParse(textBoxLattitude.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                out lattitude);
            result &= double.TryParse(textBoxLongitude.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                out longitude);

            if (result) return false;
            MessageBox.Show("Latitude and/or longitude are not correct values.", "Incorrect location",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign,
                true);
            return true;
        }

        private void ButtonLookupClick(object sender, EventArgs e)
        {
            LookupAddress();
        }

        private async void ButtonFetchRainfallClick(object sender, EventArgs e)
        {
            buttonFetchRainfall.Enabled = false;

            double latitude;
            double longitude;
            if (GetLatitudeLongitude(out latitude, out longitude)) return;

            // Fetch rainfall data
            var rainPrediction = await _buienRadar.UpdateAsync(latitude, longitude);
            listBoxRainFallData.Items.Add("");
            listBoxRainFallData.Items.Add(string.Format("*** Rainfall {0:MM/dd/yy H:mm:ss} ***", DateTime.Now));
            listBoxRainFallData.Items.Add("");
            foreach (var rainresult in rainPrediction)
            {
                var line = string.Format("{0:HH:mm} - {1} ({2:0.00} mm)", rainresult.Time, rainresult.RainfallIndex,
                    rainresult.RainfallIndexmm);
                listBoxRainFallData.Items.Add(line);
            }

            // Create periods of rain
            var rainPeriods = new RainPeriods();
            var periods = rainPeriods.CreatePeriods(rainPrediction);

            // Create text from rain periods
            var rainDescription = BuienRadarToText.TextDescription(periods);
            listBoxRainFallData.Items.Add("");
            listBoxRainFallData.Items.Add(rainDescription);
            listBoxRainFallData.Items.Add("");

            listBoxRainFallData.TopIndex = listBoxRainFallData.Items.Count - 1;
            buttonFetchRainfall.Enabled = true;
        }


         private async void ButtonFetchWeatherStationDataClick(object sender, EventArgs e)
        {
            buttonFetchWeatherStationData.Enabled = false;

            double lattitude;
            double longitude;
            if (GetLatitudeLongitude(out lattitude, out longitude)) return;


            var weersInformatieBuienRadar = await _weersInformatie.UpdateAsync();
            var weatherStation = _weersInformatie.SelectClosestWeatherStation(lattitude, longitude,
                weersInformatieBuienRadar);

            listBoxRainFallData.Items.Add("");
            listBoxRainFallData.Items.Add(string.Format("*** Weather information {0:MM/dd/yy H:mm:ss} ***", DateTime.Now));
            listBoxRainFallData.Items.Add("");
            listBoxRainFallData.Items.AddRange(
                StringUtils.Wrap(weersInformatieBuienRadar.Weergegevens.VerwachtingVandaag.Formattedtekst, 98));

            listBoxRainFallData.Items.Add("");
            listBoxRainFallData.Items.Add("Dichtstbijzijnde weerstation :" + weatherStation.StationNaam.Text);
            listBoxRainFallData.Items.Add("Temperatuur grond : " + weatherStation.TemperatuurGrondCelcius + " c ");
            listBoxRainFallData.Items.Add("Temperatuur lucht : " + weatherStation.Temperatuur10cm + " c ");
            listBoxRainFallData.Items.Add(WeersInformatieToText.TemperatureToText(weatherStation));

            listBoxRainFallData.TopIndex = listBoxRainFallData.Items.Count - 1;
            buttonFetchWeatherStationData.Enabled = true;
            
        }
    }
}