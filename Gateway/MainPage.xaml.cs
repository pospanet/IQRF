using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gateway
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string SPI_CONTROLLER_NAME = "SPI0";
        private const int SPI_CHIP_SELECT_LINE = 0;

        private SpiDevice iqrf;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task<SpiDevice> InitSPI()
        {
            SpiConnectionSettings settings = new SpiConnectionSettings(SPI_CHIP_SELECT_LINE);
            settings.ClockFrequency = 5000000;
            settings.Mode = SpiMode.Mode0;
            string spiAqs = SpiDevice.GetDeviceSelector(SPI_CONTROLLER_NAME);
            DeviceInformationCollection devicesInfo = await DeviceInformation.FindAllAsync(spiAqs);
            return await SpiDevice.FromIdAsync(devicesInfo[0].Id, settings);
        }

        private async void btnInitSPI_Click(object sender, RoutedEventArgs e)
        {
            iqrf = await InitSPI();
        }
    }
}
