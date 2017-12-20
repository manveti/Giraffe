using CefSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Giraffe {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public readonly String dataDir;
        public readonly String cacheDir;

        public App() {
            this.dataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            this.dataDir = System.IO.Path.Combine(this.dataDir, "Giraffe");
            this.cacheDir = System.IO.Path.Combine(this.dataDir, "cache");

            CefSettings settings = new CefSettings();
            settings.CachePath = this.cacheDir;
            Cef.Initialize(settings);
        }
    }
}
