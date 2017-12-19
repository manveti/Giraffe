using CefSharp;
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

namespace Giraffe {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisplayHandler {
        public MainWindow() {
            InitializeComponent();
            this.browser.DisplayHandler = this;
        }


        //menu handlers
        //navbar handlers

        public void addressBarKeyUp(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) { return; }
            this.browser.Address = this.addressBar.Text;
        }


        // browser event handlers
        // IDisplayHandler
        public void OnAddressChanged(IWebBrowser sender, AddressChangedEventArgs e) {
            this.addressBar.Dispatcher.Invoke(new Action(() => { this.addressBar.Text = e.Address; }));
        }

        public bool OnConsoleMessage(IWebBrowser sender, ConsoleMessageEventArgs e) { return false; }
        public void OnFaviconUrlChange(IWebBrowser sender, IBrowser b, IList<String> urls) { }
        public void OnFullscreenModeChange(IWebBrowser sender, IBrowser b, bool fullscreen) { }
        public void OnStatusMessage(IWebBrowser sender, StatusMessageEventArgs e) { }
        public void OnTitleChanged(IWebBrowser sender, TitleChangedEventArgs e) { }
        public bool OnTooltipChanged(IWebBrowser sender, String text) { return false; }
    }
}
