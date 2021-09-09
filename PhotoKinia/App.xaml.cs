using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoKinia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0)
                return;

            var simplifiedArgs = string.Join(" ", e.Args);
            var language = "en-EN";
            if (simplifiedArgs.Equals("--language -en"))
                language = "en-EN";
            if (simplifiedArgs.Equals("--language -pl"))
                language = "en-EN";
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(language);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(language);
        }
    }
}
