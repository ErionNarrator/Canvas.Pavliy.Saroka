using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Canvas.Pavliy.Saroka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private StreamReader reader;
        private Thread listeningThread;
        private DispatcherTimer updateUITimer;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToServer();
            SetupUIUpdateTimer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient(); //IP адрес сюда
                reader = new StreamReader(client.GetStream());
                listeningThread = new Thread(ListenForServerMessages);
                listeningThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к серверу: " + ex.Message);
            }
        }

        private void ListenForServerMessages()
        {
            while (client.Connected)
            {
                try
                {
                    string message = reader.ReadLine();
                    if (message != null)
                    {

                    }
                }
                catch (Exception)
                {
                   
                }
            }
        }

        private void SetupUIUpdateTimer()
        {
            updateUITimer = new DispatcherTimer();
            updateUITimer.Interval = TimeSpan.FromMilliseconds(30);
            updateUITimer.Tick += UpdateUI;
            updateUITimer.Start();
        }

        private void UpdateUI(object sender, EventArgs e)
        {

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (client != null)
            {
                client.Close();
            }
            if (listeningThread != null)
            {
                listeningThread.Abort();
            }
        }
    }
}