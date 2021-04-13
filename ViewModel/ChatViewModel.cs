using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMDemo.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using Newtonsoft.Json;

namespace MVVMDemo.ViewModel
{
    public class ChatViewModel : BindableBase
    {
        public MyICommand<String> SendCommand { get; set; }

        public MyICommand<String> MainCommand { get; set; }

        public MyICommand<String> SearchCommand { get; set; }

        public MyICommand<CHATHISTORY> ChatCommand { get; set; }

        public MyICommand<String> Buzz { get; set; }


        public ChatViewModel()
        {
            MainCommand = new MyICommand<string>(Main);
            SendCommand = new MyICommand<String>(SendMsg);
            SearchCommand = new MyICommand<String>(Search);
            ChatCommand = new MyICommand<CHATHISTORY>(ViewChat);
            Buzz = new MyICommand<String>(SendBuzz);
            Chat_Search = new ObservableCollection<SEARCH>();
            Chat_Search.Add(new SEARCH { });
            ObservableCollection<CurrentMessage> newMsg = new ObservableCollection<CurrentMessage>();
            newMsg.Add(new CurrentMessage { CurrentMsg = "Your messages goes here" });
            NewMsg = newMsg;
            ObservableCollection<MyMessage> Msgs = new ObservableCollection<MyMessage>();
            Messages = Msgs;
            ObservableCollection<Display_Oldchat> Dchat = new ObservableCollection<Display_Oldchat>();
            dchat = Dchat;
            
        }
        public ObservableCollection<MyMessage> Messages 
        {
            get;
            set;
        }
        public ObservableCollection<CurrentMessage> NewMsg
        {
            get;
            set;
        }
        public ObservableCollection<CHATHISTORY> Chat_ChatHistory
        {
            get;
            set;
        }
        public ObservableCollection<Display_Oldchat> dchat
        {
            get;
            set;
        }
        public ObservableCollection<SEARCH> Chat_Search
        {
            get;
            set;
        }




        public class Display_Oldchat : INotifyPropertyChanged
        {
            private string display_oldchat;

            public event PropertyChangedEventHandler PropertyChanged;

            private void RaisePropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
            public string SelectedItem
            {
                get
                {
                    return display_oldchat;
                }

                set
                {
                    if (display_oldchat != value)
                    {
                        display_oldchat = value;
                        RaisePropertyChanged("display_oldchat");
                    }
                }
            }
        }

        public StudentViewModel SVM;
        public MainWindowViewModel MVM;
        public void getRef(ref StudentViewModel S, ref MainWindowViewModel M) { 
            SVM = S; MVM = M;
            Chat_ChatHistory = SVM.ChatHistory;
         
        }
        private void SendBuzz(string m)
        {
            SVM.mySocket.SendMessage(m, MyMessage.msgType.Buzz);
        }
        private void SendMsg(string m)
        {  
            SVM.mySocket.SendMessage(m,MyMessage.msgType.Chat);
        }
        private void Main(String s)
        {
            MVM.OnNav("main");
        }
        private void Search(String s)
        {

            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            string addPath = "../../History/";
            string path = Path.Combine(docPath, addPath);

            ObservableCollection<CHATHISTORY> history;
            history = Chat_ChatHistory;
            Chat_ChatHistory.Clear();
            FileInfo[] AllFiles = new DirectoryInfo(path).GetFiles();

            var sorted =
            from file in AllFiles
            where file.ToString().Contains(Chat_Search[0].Search)
            select file;
            foreach (var f in sorted)
            {
                Chat_ChatHistory.Add(new CHATHISTORY { Chathistory = f.ToString() });
            }
        }
        public void ViewChat(CHATHISTORY s)
        {
            if (s != null)
            {
                string docPath = AppDomain.CurrentDomain.BaseDirectory;
                string addPath = "../../History/";
                string path = Path.Combine(docPath, addPath);
                Messages.Clear();
                string all = File.ReadAllText(path + s.Chathistory);
                string[] words = all.Split('}');
                for (int i = 0; i < words.Length-1; i++)
                {
                    MyMessage mm = new MyMessage { };
                    words[i] += '}';
                    mm = JsonConvert.DeserializeObject<MyMessage>(words[i]);
                    Messages.Add(mm);

                }
            }
        }
    }
}
