// Group 1 
// Main server functionality file
// Project IV - Winter 2024 
// Project Stellar 

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Markup;


namespace ServerGUI
{
    public partial class MainWindow : Window
    {
        // set up the send and receive ports 
        private UdpClient server27000;
        private UdpClient server28000;
        private UdpClient server29000;
        private Thread listenThread27000;
        private Thread listenThread28000;
        private Thread listenThread29000;
        private const int port27000 = 27000;
        private const int port27500 = 27500;
        private const int port28000 = 28000;
        private const int port28500 = 28500;
        private const int port29000 = 29000;
        private const int port29500 = 29500;

        private int messagesReceived27000 = 0;
        private int messagesReceived28000 = 0;

        public MainWindow()
        {
            InitializeComponent();
            StartServers();
        }

        // function to validate the user's login information 
        private bool validateUser(byte[] data)
        {
            string loginInfo = Encoding.ASCII.GetString(data);
            Console.WriteLine(loginInfo);

            if (loginInfo == "1234")
                return true; 
           
            else
                return false; 
        }

        // function to start the servers and start listening 
        private void StartServers()
        {
            try
            {
                server27000 = new UdpClient(port27000);
                server28000 = new UdpClient(port28000);
                server29000 = new UdpClient(port29000);

                listenThread27000 = new Thread(ListenForClients27000);
                listenThread28000 = new Thread(ListenForClients28000);
                listenThread29000 = new Thread(ListenForClients29000);

                listenThread27000.Start();
                listenThread28000.Start();
                listenThread29000.Start();

                // update UI element indicating that the servers have started
                Dispatcher.Invoke(() =>
                {
                    serverStatusLabel.Content = "Servers started";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting servers: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // listen for connection from client1
        private void ListenForClients27000()
        {
            try
            {
                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] clientData = server27000.Receive(ref clientEndPoint);
          
                    Thread clientThread = new Thread(() => HandleClient27000(clientData, clientEndPoint));
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Error receiving data on port 27000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // listen for connection from client2
        private void ListenForClients28000()
        {
            try
            {
                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] clientData = server28000.Receive(ref clientEndPoint);
                  
                    Thread clientThread = new Thread(() => HandleClient28000(clientData, clientEndPoint));
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Error receiving data on port 28000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListenForClients29000() // listen for client log in 
        {
            try
            {
                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] clientData = server29000.Receive(ref clientEndPoint);

                    bool loginStatus = validateUser(clientData);

                    if (loginStatus == true) // if the login is sucessful
                    {
                        Console.WriteLine("Login has been approved");
                        Thread clientThread = new Thread(() => HandleClient29000_Success(clientEndPoint));
                        clientThread.Start();
                    }
                    else if (loginStatus == false) 
                    {
                        Console.WriteLine("Error with log in");
                        Thread clientThread = new Thread(() => HandleClient29000_Failure(clientEndPoint));
                        clientThread.Start();
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"Error receiving data on port 29000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // receive images and messages 
        // saves data to file 
        // updates server GUI elements 
        private void HandleClient27000(byte[] data, IPEndPoint clientEndPoint)
        {
            try
            {
                string dataReceived = Encoding.ASCII.GetString(data);

                // handle image transfers 
                if (dataReceived.StartsWith("[Image]"))
                {
                    byte[] imageData = Convert.FromBase64String(dataReceived.Substring(7)); // Remove [Image] prefix
                    File.WriteAllBytes("ReceivedImage27000.jpg", imageData);
                    Dispatcher.Invoke(() =>
                    {
                        latestMessage.Content = "Received image from Client 1 (Port 27000)";
                    });
                }

                // handle message transfers 
                else
                {
                    // append the received data to a text file
                    SaveDataToFile(dataReceived, "ChatHistory.txt", "Client1");

                    Dispatcher.Invoke(() =>
                    {
                        // update UI information 
                        serverStatusLabel.Content = "Client Connected (Port 27000)";
                        numUsersLabel.Content = "True";

                        latestMessageReceivedTime.Content = DateTime.Now.ToString(); // update received message time

                        messagesReceived27000++;
                        numReceivedMessages.Content = messagesReceived27000 + messagesReceived28000;

                        latestMessage.Content = dataReceived; // display latest message 

                        if (dataReceived == "[Disconnect]")
                        {
                            serverStatusLabel.Content = "Client Disconnected (Port 27000)";
                        }
                    });

                    // Forward the received data to port 28500
                    server28000.Send(data, data.Length, new IPEndPoint(clientEndPoint.Address, port28500)); // send this info to client 2 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client connection on port 27000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleClient28000(byte[] data, IPEndPoint clientEndPoint)
        {
            try
            {
                string dataReceived = Encoding.ASCII.GetString(data);

                
                if (dataReceived.StartsWith("[Image]"))
                {
                    byte[] imageData = Convert.FromBase64String(dataReceived.Substring(7)); // remove [Image] prefix
                    File.WriteAllBytes("ReceivedImage28000.jpg", imageData);
                    Dispatcher.Invoke(() =>
                    {
                        latestMessage.Content = "Received image from Client 2 (Port 28000)";
                    });
                }
                else 
                {
                    // append the received data to a text file
                    SaveDataToFile(dataReceived, "ChatHistory.txt", "Client2");

                    Dispatcher.Invoke(() =>
                    {
                        serverStatusLabel.Content = "Client Connected (Port 28000)";
                        numUsersLabel.Content = "True";

                        latestMessageReceivedTime.Content = DateTime.Now.ToString();

                        messagesReceived28000++;
                        numReceivedMessages.Content = messagesReceived28000 + messagesReceived27000;

                        latestMessage.Content = dataReceived;

                        if (dataReceived == "[Disconnect]")
                        {
                            serverStatusLabel.Content = "Client Disconnected (Port 28000)";
                        }
                    });

                    // forward the received data to port 27500
                    server27000.Send(data, data.Length, new IPEndPoint(clientEndPoint.Address, port27500));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client connection on port 28000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // handle the login if the provided credentials are correct 
        // sends the confirmation message back to client 
        // logs info using a function 
        private void HandleClient29000_Success(IPEndPoint clientEndPoint)  
        {
            try
            {
                // log the state to file 
                string successfulLogin = "Client login successful";
                string stateFile = "StateFile.txt";
                SaveStateToFile(successfulLogin, stateFile);

                // send login confirmation
                string returnMessage = "true";
                byte[] confirmation = System.Text.Encoding.UTF8.GetBytes(returnMessage);
                server29000.Send(confirmation, confirmation.Length, clientEndPoint); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client connection using HandleClient29000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // handles the login if credentials are incorrect 
        // sends failure message to client 
        private void HandleClient29000_Failure(IPEndPoint clientEndPoint) 
        {
            try
            {
                // log the state to file 
                string failureLogin = "Client login failed";
                string stateFile = "StateFile.txt";
                SaveStateToFile(failureLogin, stateFile);

                // send login failure message 
                string message = "false";
                byte[] confirmation = System.Text.Encoding.UTF8.GetBytes(message);
                server29000.Send(confirmation, confirmation.Length, clientEndPoint); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client connection using HandleClient29000: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // function to save the messages to file 
        // keeps track of client 1 and client 2
        private void SaveDataToFile(string data, string fileName, string currentClient)
        {
            try
            {
                string currentTime = DateTime.Now.ToString();
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("[" + currentTime + "] " + currentClient + ": " + data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data to file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // logs the attempted successful and unsucessful logins to a text file 
        private void SaveStateToFile(string message, string fileName)
        {
            try
            {
                string currentTime = DateTime.Now.ToString();
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine("[" + currentTime + "]" + ": " + message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data to file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // properly close the servers 
        private void StopServers()
        {
            server27000.Close();
            server28000.Close();
            server29000.Close(); 
        }

        // close the GUI and servers on close 
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            StopServers();
        }
    }
}
