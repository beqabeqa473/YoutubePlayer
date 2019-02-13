﻿using Newtonsoft.Json.Linq;
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
            using (var wC = new WebClient())
            {
            wC.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateCompleted);
            await wC.DownloadFileTaskAsync(new Uri(update.Download), AppDomain.CurrentDomain.FriendlyName);
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
            DialogResult dialogResult = MessageBox.Show($"Возникла непредвиденная ошибка: {e.Message}. Нажмите Да для отправки сообщения об этой ошибке и продолжить работы, или Нет для выхода из приложения. Также вы можете отправить содержимое файла \"Error.log\" из окна \"Сообщить об ошибке\"", "Ошибка", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                Environment.Exit(0);
            }
            else if (dialogResult == DialogResult.Yes)
            {
                string body = $"Reported by Reporter bot {Environment.NewLine}{e.ToString()}";
                var values = new Dictionary<string, string>
{
{"method", "reportissue"},
{"title", e.Message},
{"body", body}
};
                string response = await HttpRequest.GetRequest(values);
                JObject json = JObject.Parse(response);
                if (json["number"] != null)
                {
                    MessageBox.Show($"Создана заявка с номером {json["number"].ToString()}. Мы постараемся устранить проблему или Воплотить ваше пожелание в реальность. Вы всегда можете отследить заявку по ссылке: \"{json["html_url"].ToString()}\". Пожалуйста, сохраните ссылку для последующего использования.", "Спасибо");
                }
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
    }
}
