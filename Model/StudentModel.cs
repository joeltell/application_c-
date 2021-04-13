using System;
using System.ComponentModel;
using System.Net.Sockets;
using MVVMDemo.ViewModel;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Media;
using System.Text;

namespace MVVMDemo.Model
{

    public class StudentModel
    {

    }
    public class CHATHISTORY : INotifyPropertyChanged
    {
        private string chathistory;
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public string Chathistory
        {
            get
            {
                return chathistory;
            }
            set
            {
                if (chathistory != value)
                {
                    chathistory = value;
                    RaisePropertyChanged("CurrentMessage");
                   
                }
            }
        }
    }
    public class SEARCH : INotifyPropertyChanged
    {
        private string search;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public string Search
        {
            get
            {
                return search;
            }

            set
            {
                if (search != value)
                {
                    search = value;
                    RaisePropertyChanged("Search");
                }
            }
        }
    }
    public class USERNAME : INotifyPropertyChanged
        {
            private string username;

            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
            public string Username
            {
                get
                {
                    return username;
                }

                set
                {
                    if (username != value)
                    {
                        username = value;
                        RaisePropertyChanged("Ipaddress");
                    }
                }
            }
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
            public enum msgType
            {
                Chat,
                Connect,
                Buzz

            }

            public string msg { get; set; }
            public string username { get; set; }
            public string date_time { get; set; }
            public msgType msg_type { get; set; }
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
            private string username;
            private string otherUser;
            private StreamWriter chatlog;
            public event PropertyChangedEventHandler PropertyChanged;
            public void getRef(ref MainWindowViewModel M, ref ChatViewModel C) { MVM = M; CVM = C; if (MVM == null) System.Diagnostics.Debug.WriteLine("Socket M null"); }
            private void RaisePropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
            public async void StartListen(string ip_in, string port_in, string usr,MyMessage.msgType t)
            {
            MyMessage myMessage = new MyMessage {};
                username = usr;
                Int32 port = Int32.Parse(port_in);
                if (MVM == null) System.Diagnostics.Debug.WriteLine("LISTEN M null");
                listen = new TcpListener(IPAddress.Parse(ip_in), port);
                listen.Start();
                Byte[] bytes = new Byte[256];
                while (true)
                {
                    client = await listen.AcceptTcpClientAsync();
                    NetworkStream stream = client.GetStream();
                    int i;
                    i = await stream.ReadAsync(bytes, 0, bytes.Length);
                    string data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    myMessage = JsonConvert.DeserializeObject<MyMessage>(data);
                    otherUser = myMessage.username;

                    string message = null;
                    if (MessageBox.Show("Accept invitation?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        myMessage.msg = "NO";
                        myMessage.username = username;
                        myMessage.date_time = "";
                        myMessage.msg_type = MyMessage.msgType.Connect;
                        message = JsonConvert.SerializeObject(myMessage);
                        bytes = System.Text.Encoding.UTF8.GetBytes(message);
                        stream.Write(bytes, 0, bytes.Length);
                        listen.Stop();
                        break;


                    }
                    else
                    {
                        myMessage.msg = "yes";
                        myMessage.username = username;
                        myMessage.date_time = "";
                        myMessage.msg_type = MyMessage.msgType.Connect;
                        message = JsonConvert.SerializeObject(myMessage);
                        bytes = System.Text.Encoding.UTF8.GetBytes(message);
                        stream.Write(bytes, 0, bytes.Length);
                        string docPath = AppDomain.CurrentDomain.BaseDirectory;
                        string addPath = "../../History/" + otherUser + " ";
                        addPath = addPath + DateTime.Now.ToString("yy-dd-M--HH");
                        addPath = addPath + ".txt";
                        addPath = Path.Combine(docPath, addPath);
                        chatlog = new StreamWriter(new FileStream(addPath, FileMode.Append), Encoding.UTF8);
                        MVM.OnNav("chat");
                        listen.Stop();
                        ListenForMessage();
                        break;


                    }


                }

            }
            public async void SendInvite(string ip_in, string port_in, string usr,MyMessage.msgType t)
            {
                MyMessage myMessage = new MyMessage {username = usr,msg_type = t };
                username = usr;
                Int32 port = Int32.Parse(port_in);
                client = new TcpClient();
                await client.ConnectAsync(ip_in, port);
                int i;
                string data = null;
                if (MVM == null) System.Diagnostics.Debug.WriteLine("SEND M null");
                Byte[] bytes = new Byte[256];
                NetworkStream stream = client.GetStream();
                string jsonString;
                jsonString = JsonConvert.SerializeObject(myMessage);

                bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
                stream.Write(bytes, 0, bytes.Length);
                i = await stream.ReadAsync(bytes, 0, bytes.Length);
                data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                myMessage = JsonConvert.DeserializeObject<MyMessage>(data);

            if (myMessage.msg != "NO")
                {
                    otherUser = myMessage.username;
                    string docPath = AppDomain.CurrentDomain.BaseDirectory;
                    string addPath = "../../History/" + otherUser + " ";
                    // Write the specified text asynchronously to a new file.
                    addPath = addPath + DateTime.Now.ToString("yy-dd-M--HH");
                    addPath = addPath + ".txt";
                    addPath = Path.Combine(docPath, addPath);
                    chatlog = new StreamWriter(addPath, append: true, Encoding.UTF8);
                    MVM.OnNav("chat");
                    ListenForMessage();
                   
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SVAR: NEJ");
                }

            }

            public async void SendMessage(string msg,MyMessage.msgType t)
            {
            
                string mm = CVM.NewMsg[0].CurrentMsg;
                string localDate = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
                MyMessage myMessage = new  MyMessage { msg = mm, username = username, date_time = localDate, msg_type = t };
                if(myMessage.msg_type == MyMessage.msgType.Chat)
                {
                     CVM.Messages.Add(new MyMessage { msg = CVM.NewMsg[0].CurrentMsg, username = username, date_time = localDate });
                }
                string jsonString;
                jsonString = JsonConvert.SerializeObject(myMessage);



                Byte[] bytes = new Byte[256];
                NetworkStream stream = client.GetStream();
                bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
                await stream.WriteAsync(bytes, 0, bytes.Length);
                await chatlog.WriteAsync(jsonString);
                await chatlog.FlushAsync();

            }
            public async void ListenForMessage()
            {

                try
                {
                    Byte[] bytes = new Byte[256];
                    NetworkStream stream = client.GetStream();
                    MyMessage mm = new MyMessage { };
                    while (true)
                    {
                        int i;
                        string data = null;
                        i = await stream.ReadAsync(bytes, 0, bytes.Length);
                        data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                       
                        mm = JsonConvert.DeserializeObject<MyMessage>(data);
                    if(mm.msg_type == MyMessage.msgType.Chat)
                    {
                        await chatlog.WriteAsync(data); // write to file!
                        await chatlog.FlushAsync();
                        CVM.Messages.Add(mm);
                    }
                    else if(mm.msg_type == MyMessage.msgType.Buzz)
                    {
                        SystemSounds.Exclamation.Play();
                    }

                }
                }
                catch (SocketException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ErrorCode + "User disconnected");
                }
                catch (Exception i)
                {
                    System.Diagnostics.Debug.WriteLine(i.Message + "User disconnected E");
                    CVM.NewMsg[0].CurrentMsg = "User disconnected";
                    CVM.Messages.Add(new MyMessage { msg = "User disconnect", username = "", date_time = "jan-2020 13:37" });

                }
            }
        }

    }
