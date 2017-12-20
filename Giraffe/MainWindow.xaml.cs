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
    public partial class MainWindow : Window, IDisplayHandler, ILoadHandler {
        WindowState previousWindowState = WindowState.Normal;

        public MainWindow() {
            InitializeComponent();
            this.browser.DisplayHandler = this;
            this.browser.LoadHandler = this;
        }


        // menu handlers
        // File
        public void newWindow(object sender, RoutedEventArgs e) {
            MainWindow w = new MainWindow();
            w.Show();
        }

        public void newPrivateWindow(object sender, RoutedEventArgs e) {
            MainWindow w = new MainWindow();
            w.browser.RequestContext = new RequestContext();
            w.Show();
        }

        //open file
        //close window
        //save page as
        //page setup
        //print preview
        //print
        //exit

        //other menu handlers

            
        // navbar handlers
        public void goBack(object sender, RoutedEventArgs e) {
            this.browser.Back();
        }

        //back menu

        public void goForward(object sender, RoutedEventArgs e) {
            this.browser.Forward();
        }

        //forward menu

        public void doReload(object sender, RoutedEventArgs e) {
            this.browser.Reload();
        }

        public void doStop(object sender, RoutedEventArgs e) {
            this.browser.Stop();
        }

        //home
        //history
        //config

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

        // ILoadHandler
        public void OnFrameLoadEnd(IWebBrowser sender, FrameLoadEndEventArgs e) { }
        public void OnFrameLoadStart(IWebBrowser sender, FrameLoadStartEventArgs e) { }
        public void OnLoadError(IWebBrowser sender, LoadErrorEventArgs e) { }

        public void OnLoadingStateChange(IWebBrowser sender, LoadingStateChangedEventArgs e) {
            this.Dispatcher.Invoke(new Action(() => {
                this.backButton.IsEnabled = e.CanGoBack;
                this.forwardButton.IsEnabled = e.CanGoForward;
                this.reloadButton.IsEnabled = e.CanReload;
                this.stopButton.IsEnabled = e.IsLoading;
            }));
        }


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
