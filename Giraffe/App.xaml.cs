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
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            App.fixBrowserVersion();
        }

        public static void fixBrowserVersion() {
            String path = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            String appName = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            int version = App.getBrowserVersion();
            Microsoft.Win32.Registry.SetValue(path, appName + ".exe", version);
            Microsoft.Win32.Registry.SetValue(path, appName + ".vshost.exe", version);
        }

        private static int getBrowserVersion() {
            String path = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer";
            String[] keys = { "svcVersion", "svcUpdateVersion", "Version", "W2kVersion" };
            int version = 0;
            foreach (String key in keys) {
                String valStr = System.Convert.ToString(Microsoft.Win32.Registry.GetValue(path, key, null));
                if (valStr == null) { continue; }
                int idx, val;
                idx = valStr.IndexOf('.');
                if (idx > 0) { valStr = valStr.Substring(0, idx); }
                if (!int.TryParse(valStr, out val)) { continue; }
                if (val > version) { version = val; }
            }
            return version;
        }
    }
}
