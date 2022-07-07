using System;
using System.Windows;
using DiscordRPC;
using Newtonsoft.Json;
using System.IO;
namespace Discord_RPC_Maker
{
    public partial class MainWindow : Window
    {
        private DiscordRpcClient client;
        private ApplicationSaving applicationSave;

        private string savingPath = Application.Current.MainWindow + "config.json";

        private bool isClicked = false;

        public MainWindow()
        {
            InitializeComponent();
            ApplicationSaving jsonConvert = new ApplicationSaving();
            if (!File.Exists(savingPath))
            {
                string json = JsonConvert.SerializeObject(jsonConvert);
                File.WriteAllText(savingPath, json);
            }
            string jsonLoad = File.ReadAllText(savingPath);
            applicationSave = JsonConvert.DeserializeObject<ApplicationSaving>(jsonLoad);

            NameTextBox.Text = applicationSave.ApplicationID;
            DetailsTextBox.Text = applicationSave.Details;
            StateTextBox.Text = applicationSave.State;
            LargeImageLinkTextBox.Text = applicationSave.LargeImageKey;
            LargeImageTextTextBox.Text = applicationSave.LargeImageText;
            SmallImageLinkTextBox.Text = applicationSave.SmallImageKey;
            SmallImageTextTextBox.Text = applicationSave.SmallImageText;
            ButtonLabelTextBox.Text = applicationSave.ButtonLabelText;
            ButtonLabelTextBox1.Text = applicationSave.ButtonLabelText1;
            ButtonLinkTextBox.Text = applicationSave.ButtonLabelLink;
            ButtonLinkTextBox1.Text = applicationSave.ButtonLabeLink1;
        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                isClicked = !isClicked;
                if (isClicked)
                {

                    applicationSave.ApplicationID = NameTextBox.Text;
                    applicationSave.Details = DetailsTextBox.Text;
                    applicationSave.State = StateTextBox.Text;
                    applicationSave.LargeImageKey = LargeImageLinkTextBox.Text;
                    applicationSave.LargeImageText = LargeImageTextTextBox.Text;
                    applicationSave.SmallImageKey = SmallImageLinkTextBox.Text;
                    applicationSave.SmallImageText = SmallImageTextTextBox.Text;
                    applicationSave.ButtonLabelLink = ButtonLinkTextBox.Text;
                    applicationSave.ButtonLabelText = ButtonLabelTextBox.Text;
                    applicationSave.ButtonLabeLink1 = ButtonLinkTextBox1.Text;
                    applicationSave.ButtonLabelText1 = ButtonLabelTextBox1.Text;
                    string json = JsonConvert.SerializeObject(applicationSave);
                    File.WriteAllText(savingPath, json);
                    client = new DiscordRpcClient(NameTextBox.Text);
                    client.Initialize();
                    client.SetPresence(new RichPresence()
                    {

                        Details = DetailsTextBox.Text,
                        State = StateTextBox.Text,
                        Assets = new Assets
                        {
                            LargeImageKey = LargeImageLinkTextBox.Text,
                            LargeImageText = LargeImageTextTextBox.Text,
                            SmallImageKey = SmallImageLinkTextBox.Text,
                            SmallImageText = SmallImageTextTextBox.Text,
                        },
                        Buttons = new DiscordRPC.Button[]
                        {
                        new DiscordRPC.Button { Label = ButtonLabelTextBox.Text, Url = ButtonLinkTextBox.Text },
                        new DiscordRPC.Button { Label = ButtonLabelTextBox1.Text, Url = ButtonLinkTextBox1.Text },
                        },
                        Timestamps = Timestamps.Now,

                    });
                    var timer = new System.Timers.Timer(150);
                    timer.Elapsed += (sender, args) => { client.Invoke(); };
                    timer.Start();
                    MessageBox.Show("Discord RPC, loaded and working great!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    client.Dispose();
                    MessageBox.Show("Turned off!", "Information!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Looks like you forget something to put. Check clearly. Common issue: Assets in Application ID doesn't have an image that you typed. System Error: {ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
    }

    public class ApplicationSaving
    {
        public string ApplicationID { get; set; }
        public string Details { get; set; }
        public string State { get; set; }
        public string LargeImageKey { get; set; }
        public string LargeImageText { get; set; }
        public string SmallImageKey { get; set; }
        public string SmallImageText { get; set; }
        public string ButtonLabelText { get; set; }
        public string ButtonLabelLink { get; set; }
        public string ButtonLabelText1 { get; set; }
        public string ButtonLabeLink1 { get; set; }


        public ApplicationSaving()
        {
            ApplicationID = "Application ID";
            Details = "Details";
            State = "State";
            LargeImageKey = "LargeImageLink";
            LargeImageText = "LargeImageText";
            SmallImageKey = "SmallImageLink";
            SmallImageText = "SmallImageText";
            ButtonLabelText = "ButtonLabelText";
            ButtonLabelLink = "ButtonLabelLink";
            ButtonLabelText1 = "ButtonLabelText1";
            ButtonLabeLink1 = "ButtonLabeLink1";
        }

    }

}
