using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubePlayer
{
    public partial class ReportIssue : Form
    {
        public ReportIssue()
        {
            InitializeComponent();
        }

        private async void btnSend_ClickAsync(object sender, EventArgs e)
        {
            string body = $"Reported by {txtName.Text}{Environment.NewLine}{txtComment.Text}";
            var values = new Dictionary<string, string>
{
{"method", "reportissue"},
{"title", txtTitle.Text},
{"body", body}
};
            string response = await HttpRequest.GetRequest(values);
            JObject json = JObject.Parse(response);
            if (json["number"] != null)
            {
                MessageBox.Show($"Создана заявка с номером {json["number"].ToString()}. Мы постараемся устранить проблему или Воплотить ваше пожелание в реальность. Вы всегда можете отследить заявку по ссылке: \"{json["html_url"].ToString()}\". Пожалуйста, сохраните ссылку для последующего использования.", "Спасибо");
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
                }

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}
