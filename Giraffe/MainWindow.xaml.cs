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
        WindowState previousWindowState = WindowState.Normal;

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

        public void OnFullscreenModeChange(IWebBrowser sender, IBrowser b, bool fullscreen) {
            this.Dispatcher.Invoke(new Action(() => {
                if (fullscreen) { this.enterFullscreen(); }
                else { this.exitFullscreen(); }
            }));
        }

        public void OnStatusMessage(IWebBrowser sender, StatusMessageEventArgs e) {
            this.statusBox.Dispatcher.Invoke(new Action(() => { this.statusBox.Text = e.Value; }));
        }

        public void OnTitleChanged(IWebBrowser sender, TitleChangedEventArgs e) {
            this.Dispatcher.Invoke(new Action(() => {
                if ((e.Title != null) && (e.Title.Length > 0)) {
                    this.Title = e.Title + " - Giraffe";
                }
                else {
                    this.Title = "Giraffe";
                }
            }));
        }

        public bool OnTooltipChanged(IWebBrowser sender, String text) { return false; }


        // helper functions
        public void enterFullscreen() {
            this.browserBorder.Child = null;
            this.Content = this.browser;
            this.previousWindowState = this.WindowState;
            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
        }

        public void exitFullscreen() {
            this.Content = this.mainGrid;
            this.browserBorder.Child = this.browser;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = this.previousWindowState;
        }
    }
}
