using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubePlayer
{
    static class Program
    {
        private static Update update;

        [STAThread]
        static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledExceptionAsync);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("error.log")
                .CreateLogger();
            Log.Information($"Using os: {getOSInfo()}");
            Log.Information($"Software version: {Version.Parse(Application.ProductVersion).ToString()}");
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.FriendlyName + ".back")) File.Delete(AppDomain.CurrentDomain.FriendlyName + ".back");
            }
            catch (System.UnauthorizedAccessException)
            {
            }



            var values = new Dictionary<string, string>
            {
            {"method", "checkupdate"}
            };
            string response = await HttpRequest.GetRequest(values);
            update = JToken.Parse(response).ToObject<Update>();
            if (Version.Parse(Application.ProductVersion) < Version.Parse(update.Version))
            {
            string value = update.Changes;
            DialogResult changesDialog = InputBox.Show("Доступно обновление", $"новая версия {update.Version} {Application.ProductName} доступна для скачивания. нажмите ok для скачивания и установки данного обновления, или отмену для последующей установки.", ref value, true, true);
            if (changesDialog == DialogResult.OK)
            {
                    File.Move(AppDomain.CurrentDomain.FriendlyName, AppDomain.CurrentDomain.FriendlyName + ".back");
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    using (var wC = new WebClient())
            {
            wC.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateCompleted);
                        try
                        {
                            await wC.DownloadFileTaskAsync(new Uri(update.Download), AppDomain.CurrentDomain.FriendlyName);
                        }
                        catch (System.Net.WebException) {
                            MessageBox.Show("По некоторым причинам не удалось автоматически обновить программы. Откроется браузер для скачивания файла.", "Ошибка");
                            System.Diagnostics.Process.Start(update.Download);
}
                        }
            }
            else
            {
            Environment.Exit(0);
            }
            }
            else
            {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadExceptionAsync);
        Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            }
        }

        static async void Application_ThreadExceptionAsync(object sender, ThreadExceptionEventArgs e)
        {
            await HandleExceptionsAsync(e.Exception);
        }

        static async void CurrentDomain_UnhandledExceptionAsync(object sender, UnhandledExceptionEventArgs e)
        {
            await HandleExceptionsAsync((e.ExceptionObject as Exception));
        }

        static async Task HandleExceptionsAsync(Exception e)
        {
            Log.Error(e.ToString());
            DialogResult dialogResult = MessageBox.Show($"Возникла непредвиденная ошибка: {e.Message}. Нажмите Да что бы продолжить работу, или Нет для выхода из приложения. Пожалуйста отправьте отчет из окна \"Сообщить об ошибке\", подробно описав ошибку и приложив содержимое файла \"Error.log\"", "Ошибка", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                Environment.Exit(0);
            }
        }

        private static void UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (GetMD5HashFromFile(AppDomain.CurrentDomain.FriendlyName) == update.CheckSum)
            {
                Application.Restart();
            }
            else
            {
                MessageBox.Show("контрольные суммы файлов не совпадают. будет возвращена старая версия. пожалуйста попробуйте еще раз.", "Ошибка");
                File.Delete(AppDomain.CurrentDomain.FriendlyName);
                File.Move(AppDomain.CurrentDomain.FriendlyName + ".back", AppDomain.CurrentDomain.FriendlyName);
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadExceptionAsync);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        private static string GetMD5HashFromFile(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLowerInvariant();
                }
            }
        }

        static string getOSInfo()
        {
            OperatingSystem os = Environment.OSVersion;            Version vs = os.Version;
            string operatingSystem = "";
            if (os.Platform == PlatformID.Win32Windows)
            {
                switch (vs.Minor)
                {
                    case 0:
                        operatingSystem = "95";
                        break;
                    case 10:
                        if (vs.Revision.ToString() == "2222A")
                            operatingSystem = "98SE";
                        else
                            operatingSystem = "98";
                        break;
                    case 90:
                        operatingSystem = "Me";
                        break;
                    default:
                        break;
                }
            }
            else if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else if (vs.Minor == 1)
                            operatingSystem = "7";
                        else if (vs.Minor == 2)
                            operatingSystem = "8";
                        else
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        break;
                    default:
                        break;
                }
            }
            if (operatingSystem != "")
            {
                operatingSystem = "Windows " + operatingSystem;
                if (os.ServicePack != "")
                {
                    operatingSystem += " " + os.ServicePack;
                }
                //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";
            }
            return operatingSystem;
        }


    }
}
