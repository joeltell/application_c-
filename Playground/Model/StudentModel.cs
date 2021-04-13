using System;
using System.ComponentModel;
using System.Net.Sockets;
using MVVMDemo.ViewModel;
using System.Net;
using System.Windows;
using Newtonsoft.Json;

namespace MVVMDemo.Model
{

    public class StudentModel {
        
    }
    public class IP : INotifyPropertyChanged
    {
        private string ipaddress;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public string Ipaddress
        {
            get
            {
                return ipaddress;
            }

            set
            {
                if (ipaddress != value)
                {
                    ipaddress = value;
                    RaisePropertyChanged("Ipaddress");
                }
            }
        }
    }
    public class CurrentMessage : INotifyPropertyChanged
    {
        private string msg;
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public string CurrentMsg
        {
            get
            {
                return msg;
            }
            set
            {
                if (msg != value)
                {
                    msg = value;
                    RaisePropertyChanged("CurrentMessage");
                }
            }
        }
    }

    public class MyMessage
    {

        public string msg  { get; set;}
        public string username { get; set; }
        public string date_time { get; set; }
    }
    public class PORT : INotifyPropertyChanged
    {
        private string port;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public string Portnumber
        {
            get
            {
                return port;
            }

            set
            {
                if (port != value)
                {
                    port = value;
                    RaisePropertyChanged("Portnumber");
                }
            }
        }
    }
    public class MySocket : INotifyPropertyChanged
    {    

        private TcpClient client;
        private TcpListener listen;
        private MainWindowViewModel MVM;
        private ChatViewModel CVM;
        public event PropertyChangedEventHandler PropertyChanged;
        public void getRef(ref MainWindowViewModel M, ref ChatViewModel C) { MVM = M; CVM = C; if (MVM == null) System.Diagnostics.Debug.WriteLine("Socket M null"); }
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public async void StartListen(string ip_in, string port_in )
        {
            Int32 port = Int32.Parse(port_in);
            if (MVM == null) System.Diagnostics.Debug.WriteLine("LISTEN M null");
            listen = new TcpListener(IPAddress.Parse(ip_in), port);
            listen.Start();
            Byte[] bytes = new Byte[256];
            while (true)
            {
                System.Diagnostics.Debug.WriteLine("Waiting!");

                client = await listen.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                string message = null;
                if (MessageBox.Show("Accept invitation?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    System.Diagnostics.Debug.WriteLine("invite not accept");
                    System.Diagnostics.Debug.WriteLine("NEJ");

                    //popupruta för att feedback till användare att försöka igen bla bla

                    message = "NO";
                    // Translate the passed message into ASCII and store it as a Byte array.
                    bytes = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    //  Stream stream = client.GetStream();
                    System.Diagnostics.Debug.WriteLine("Got a Stream!");


                    // Send the message to the connected TcpServer.
                    stream.Write(bytes, 0, bytes.Length);

                    System.Diagnostics.Debug.WriteLine("Sent: {0}", message);
                    listen.Stop();
                    break;


                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("invite accepted");
                    System.Diagnostics.Debug.WriteLine("CHAT INCOMING");

                    //popuprutan + värdet in i message
                     Window2 inputDialog = new Window2("Please enter your name:", "John Doe");
                     if (inputDialog.ShowDialog() == true)
                        message = inputDialog.Answer;
                   

                    // Translate the passed message into ASCII and store it as a Byte array.
                    bytes = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    //  Stream stream = client.GetStream();
                    System.Diagnostics.Debug.WriteLine("Got a Stream!");


                    // Send the message to the connected TcpServer.
                    stream.Write(bytes, 0, bytes.Length);

                    System.Diagnostics.Debug.WriteLine("Sent: {0}", message);
                    int i;
                    string data = null;
                    System.Diagnostics.Debug.WriteLine("listen  readasync ");
                    i = await stream.ReadAsync(bytes, 0, bytes.Length);
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    System.Diagnostics.Debug.WriteLine("Received: {0}", data);
                    //ropa på onnav som skickar oss till chatsidan
                    
                    MVM.OnNav("chat");
                    listen.Stop();
                    ListenForMessage();

                    break;


                }


            }

        }
        public async void SendInvite(string ip_in, string port_in)
        {
            Int32 port = Int32.Parse(port_in);
            TcpClient client = new TcpClient();
            await client.ConnectAsync(ip_in, port);
            int i;
            string data = null;
            if (MVM == null) System.Diagnostics.Debug.WriteLine("SEND M null");

            Byte[] bytes = new Byte[256];

            NetworkStream stream = client.GetStream();
            // Get a stream object for reading and writing


            System.Diagnostics.Debug.WriteLine("inv_click readasync ");
            i = await stream.ReadAsync(bytes, 0, bytes.Length);


            // Translate data bytes to a ASCII string.
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            System.Diagnostics.Debug.WriteLine("Received: {0}", data);

            if (data != "NO")
            {
                string message = null;
                //popupruta + värdet av rutan skall in i message
                 Window2 inputDialog = new Window2("Please enter your name:", "John Doe");
                 if (inputDialog.ShowDialog() == true)
                    message = inputDialog.Answer;
              

                bytes = System.Text.Encoding.ASCII.GetBytes(message);

                stream.Write(bytes, 0, bytes.Length);
                //this.NavigationService.Navigate(new TDDD49ERP(data, client));
                System.Diagnostics.Debug.WriteLine("SVAR: JA");
                MVM.OnNav("chat");
                ListenForMessage();
            }
            else
            {
                //popuprutan för feedback till användare att försöka igen
                System.Diagnostics.Debug.WriteLine("SVAR: NEJ");
            }

        }

        public async void SendMessage(string msg)
        {
            string mm = CVM.NewMsg[0].CurrentMsg;
            MyMessage myMessage = new MyMessage { msg = mm, username = "joel", date_time ="jan-2020 03:00" };
        

            string jsonString;
            jsonString = JsonConvert.SerializeObject(myMessage);

            Byte[] bytes = new Byte[256];
            NetworkStream stream = client.GetStream();
            // Get a stream object for reading and writing
            System.Diagnostics.Debug.WriteLine("send_msg_async ");
            bytes = System.Text.Encoding.ASCII.GetBytes(jsonString);
            await stream.WriteAsync(bytes, 0, bytes.Length);     


        }
        public async void ListenForMessage()
        {
            Byte[] bytes = new Byte[256];
            System.Diagnostics.Debug.WriteLine("Listen for Messages!");
            NetworkStream stream = client.GetStream();
            MyMessage mm = new MyMessage { };
            while (true)
            {
                int i;
                string data = null;
                i = await stream.ReadAsync(bytes, 0, bytes.Length);
                
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                mm = JsonConvert.DeserializeObject<MyMessage>(data);
                System.Diagnostics.Debug.WriteLine("Received: {0}", data);
                System.Diagnostics.Debug.WriteLine("msg:" + mm.msg + " username:" + mm.username + " date_time:" + mm.date_time );
                //CVM.Messages.Add(mm.msg);
            }
            System.Diagnostics.Debug.WriteLine("Stopped Listening for Messages!");
        }
    }

}
