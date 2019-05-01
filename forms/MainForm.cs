using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubePlayer
{
    public partial class MainForm : Form
    {
        private int stream = 0;
        private int curIndex = 0;
        private readonly YoutubeClient client;
        private List<Video> videos;
        private SYNCPROC syncCallback;

        public MainForm()
        {
            InitializeComponent();
            BassNet.Registration("beqaprogger@gmail.com", "2X11233721152222");
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_DEV_DEFAULT, 1);
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            InitialiseDeviceCombo();
            client = new YoutubeClient();
            RegisterHotKey(this.Handle, 1, (uint)0, (uint)Keys.MediaPreviousTrack);
            RegisterHotKey(this.Handle, 2, (uint)0, (uint)Keys.MediaNextTrack);
            RegisterHotKey(this.Handle, 3, (uint)0, (uint)Keys.MediaPlayPause);
        }

        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                // MessageBox.Show(string.Format("Hotkey #{0} pressed", id));
                switch (id)
                {
                    case 1:
                        if (lstResults.Items.Count == 0 || curIndex== 0)
                            return;
                        curIndex -= 1;
                        lstResults.SelectedIndex = curIndex;
                        PlayFileAsync(curIndex);
                        break;
                    case 2:
                        if (lstResults.Items.Count == 0 || curIndex + 1 == lstResults.Items.Count)
                            return;
                        curIndex += 1;
                        lstResults.SelectedIndex = curIndex;
                        PlayFileAsync(curIndex);
                        break;
                    case 3:
                        BASSActive isActive = default(BASSActive);
                        isActive = Bass.BASS_ChannelIsActive(stream);
                        if (isActive == BASSActive.BASS_ACTIVE_PLAYING)
                        {
                            Bass.BASS_ChannelPause(stream);
                        }
                        else if (isActive == BASSActive.BASS_ACTIVE_PAUSED)
                        {
                            Bass.BASS_ChannelPlay(stream, false);
                        }
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void InitialiseDeviceCombo()
        {
            int defaultDevice = Bass.BASS_GetDevice();
            for (int deviceId = 1; deviceId < Bass.BASS_GetDeviceCount(); deviceId++)
            {
                BASS_DEVICEINFO device = Bass.BASS_GetDeviceInfo(deviceId);
                cbDevice.Items.Add(device.name);
            }
            cbDevice.SelectedIndex = defaultDevice - 1;
        }

        private void OnSongFinished(int handle, int channel, int data, IntPtr user)
        {
            if (curIndex + 1 == videos.Count)
                return;
            curIndex += 1;
                lstResults.SelectedIndex = curIndex;
                PlayFileAsync(curIndex);
            }

        private async void txtSearch_KeyDownAsync(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtSearch.Text == "")
                    return;
                e.SuppressKeyPress = true;
                txtSearch.SelectAll();
                lstResults.Items.Clear();
                videos = new List<Video>();
for (var page = 1; page <= int.MaxValue; page++) {
                    var tempVideos = await client.SearchVideosAsync(txtSearch.Text, page);
                if (page == 1 && tempVideos.Count == 0)
                {
                MessageBox.Show("Ничего не найдено", "Ошибка");
                break;
                }
                    var countDelta = 0;
                    foreach (var video in tempVideos)
                {
                        videos.Add(video);
                        string data = $"{video.Title} - {video.Duration.ToString()}";
                lstResults.Items.Add(data);
                        countDelta++;
                    }
                    if (page == 1 && videos.Count > 0)
                    {
                        Console.Beep(2000, 100);
                        lstResults.SelectedIndex = 0;
                        lstResults.Focus();
                    }

                    if (countDelta <= 0)
                                            break;
                                }
            }
        }

private async void lstResults_KeyDownAsync(object sender, KeyEventArgs e)
        {
switch (e.KeyData) {
                case Keys.Enter:
                    if (lstResults.Items.Count == 0)
                        return;
                        curIndex = lstResults.SelectedIndex;
                    lstResults.SelectedIndex = curIndex;
                    await PlayFileAsync(curIndex);
                break;
                case Keys.Space:
                    BASSActive isActive = default(BASSActive);
                    isActive = Bass.BASS_ChannelIsActive(stream);
                    if (isActive == BASSActive.BASS_ACTIVE_PLAYING)
                    {
                    Bass.BASS_ChannelPause(stream);
                    }
                    else if (isActive == BASSActive.BASS_ACTIVE_PAUSED)
                    {
                    Bass.BASS_ChannelPlay(stream, false);
                    }
                    break;
                case Keys.Control | Keys.B:
                    e.Handled = e.SuppressKeyPress = true;
                    cmOpenInBrowser.PerformClick();
                    break;
                case Keys.Control | Keys.C:
                    e.Handled = e.SuppressKeyPress = true;
                    cmCopyToClipboard.PerformClick();
                    break;
                case Keys.Control | Keys.D:
                    e.Handled = e.SuppressKeyPress = true;
                    cmDownloadMp3.PerformClick();
                    break;
                case Keys.Control | Keys.Q:
                    e.Handled = e.SuppressKeyPress = true;
                    mbExit.PerformClick();
                    break;
            }
        }

private async Task PlayFileAsync(int currentIndex)
    {
        Bass.BASS_StreamFree(stream);
            try
            {
                var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videos[currentIndex].Id);
                var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
                syncCallback = new SYNCPROC(OnSongFinished);
                stream = Bass.BASS_StreamCreateURL(streamInfo.Url, 0, 0, null, IntPtr.Zero);
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, tbVolume.Value / 100f);
                Bass.BASS_ChannelSetSync(stream, BASSSync.BASS_SYNC_END, 0, syncCallback, IntPtr.Zero);
                Bass.BASS_ChannelPlay(stream, false);
            }
            catch (YoutubeExplode.Exceptions.VideoUnplayableException ex)
            {
                MessageBox.Show($"Видео не может быть воспроизведено, {ex.Message}", "Ошибка");
                return;
            }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bass.BASS_StreamFree(stream);
            Bass.BASS_Free();
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
            UnregisterHotKey(this.Handle, 3);
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Down:
                    try
                    {
                        tbVolume.Value -= 1;
                        return true;
                    }
                    catch (Exception ex) when (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                    {
                        return true;
                    }
                case Keys.Control | Keys.Up:
                    try
                    {
                        tbVolume.Value += 1;
                        return true;
                    }
                    catch (Exception ex) when (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                    {
                        return true;
                    }
                case Keys.Control | Keys.Right:
                    if (txtSearch.Focused || txtDescription.Focused)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            double length = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));
                            double curPos = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream));
                            Bass.BASS_ChannelSetPosition(stream, curPos + Convert.ToDouble(2));
                            return true;
                        }
                        catch (Exception ex) when (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                        {
                            return true;
                        }
                    }
                case Keys.Control | Keys.Left:
                    if (txtSearch.Focused || txtDescription.Focused)
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            double curPos = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream));
                            Bass.BASS_ChannelSetPosition(stream, curPos - Convert.ToDouble(2));
                            return true;
                        }
                        catch (Exception ex) when (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                        {
                            return true;
                        }
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, tbVolume.Value / 100F);
        }

        private void tbVolume_ValueChanged(object sender, EventArgs e)
        {
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, tbVolume.Value / 100F);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            double len = Bass.BASS_ChannelGetLength(stream);
            double pos = Bass.BASS_ChannelGetPosition(stream);
            tbSeek.Value = Math.Min(tbSeek.Maximum, (int)(100 * pos / len));
        }

        private void tbSeek_Scroll(object sender, EventArgs e)
        {
            double length = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));
            Bass.BASS_ChannelSetPosition(stream, length * tbSeek.Value / 100);
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deviceIndex = cbDevice.SelectedIndex + 1;
            Bass.BASS_Init(deviceIndex, 44100, (BASSInit)0, IntPtr.Zero);
            Bass.BASS_SetDevice(deviceIndex);
            Bass.BASS_ChannelSetDevice(stream, deviceIndex);
        }

        private void mbReportIssue_Click(object sender, EventArgs e)
        {
            ReportIssue report = new ReportIssue();
            report.Show();
        }

        private void mbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmOpenInBrowser_Click(object sender, EventArgs e)
        {
            if (lstResults.Items.Count == 0)
                return;
                System.Diagnostics.Process.Start($"https://youtube.com/watch?v={videos[lstResults.SelectedIndex].Id}");
        }

        private void cmCopyToClipboard_Click(object sender, EventArgs e)
        {
            if (lstResults.Items.Count == 0)
                return;
            Clipboard.SetText($"https://youtube.com/watch?v={videos[lstResults.SelectedIndex].Id}");
        }

        private async void cmDownloadMp3_ClickAsync(object sender, EventArgs e)
        {
            if (lstResults.Items.Count == 0)
                return;
            Console.Beep(500, 500);
            var values = new Dictionary<string, string>
            {
            {"method", "getmp3"},
            {"id", videos[lstResults.SelectedIndex].Id.ToString()}
            };
            string response = await HttpRequest.GetRequest(values);
            var json = JObject.Parse(response);
            if (!(bool)json["error"])
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "Audio (*.mp3)|*.mp3",
                    FileName = Path.GetInvalidFileNameChars().Aggregate($"{videos[lstResults.SelectedIndex].Title}.mp3", (current, c) => current.Replace(c.ToString(), string.Empty)),
                    RestoreDirectory = true,
                    AddExtension = true
                };
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                using (var wC = new WebClient())
                {
                    wC.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompletedAsync);
                        await wC.DownloadFileTaskAsync(new Uri(json["file"].ToString()), dialog.FileName);
                    }
                }
            }
    }

        private async void DownloadCompletedAsync(object sender, AsyncCompletedEventArgs e)
        {
            Console.Beep(1000, 500);
            var values = new Dictionary<string, string>
            {
            {"method", "delete"},
            {"id", videos[lstResults.SelectedIndex].Id.ToString()}
            };
            string response = await HttpRequest.GetRequest(values);
        }

        private async void lstResults_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            var vid = await client.GetVideoAsync(videos[lstResults.SelectedIndex].Id);
            txtDescription.Text = vid.Description;
        }
    }
}
