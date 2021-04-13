using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMDemo.Model;
using System.Collections.ObjectModel;
using System.IO;


namespace MVVMDemo.ViewModel
{

    public class StudentViewModel : BindableBase
    {
        
        public MyICommand<String> InviteCommand { get; set; }
        public MyICommand<String> ListenCommand { get; set; }
        public MyICommand<CHATHISTORY> ChatCommand { get; set; }
        public MyICommand<String> SearchCommand { get; set; }
        
        public MySocket mySocket = new MySocket();
        public ChatViewModel CVM;
        public MainWindowViewModel MVM;
        public void getRef(ref ChatViewModel C, ref MainWindowViewModel M) { 
            CVM = C; MVM = M; mySocket.getRef(ref M, ref C);
            if (CVM == null) System.Diagnostics.Debug.WriteLine("C null");
            if (MVM == null) System.Diagnostics.Debug.WriteLine("M null");
            if (mySocket == null) System.Diagnostics.Debug.WriteLine("Socket null");
        }

        public StudentViewModel()
        {
           
            LoadValues();
            
            InviteCommand = new MyICommand<String>(Invite);
            ListenCommand = new MyICommand<String>(Listen);
            ChatCommand = new MyICommand<CHATHISTORY>(Chat);
            SearchCommand = new MyICommand<String>(Search);
            
        }
        public ObservableCollection<IP> Ips
        {
            get;
            set;
        }
        public ObservableCollection<PORT> Ports
        {
            get;
            set;
        }
        public ObservableCollection<CHATHISTORY> ChatHistory
        {
            get;
            set;
        }

        public ObservableCollection<USERNAME> Usernames
        {
            get;
            set;
        }
        public ObservableCollection<SEARCH> Searches
        {
            get;
            set;
        }

        public void Search(string s)
        {
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            string addPath = "../../History/";
            string path = Path.Combine(docPath, addPath);

            ObservableCollection<CHATHISTORY> history;
            history = ChatHistory;
            ChatHistory.Clear();
            FileInfo[] AllFiles = new DirectoryInfo(path).GetFiles();
         
            var sorted =
            from file in AllFiles
             where file.ToString().Contains(Searches[0].Search)
             select file;
            foreach (var f in sorted)
            {
                ChatHistory.Add(new CHATHISTORY { Chathistory = f.ToString() });
            }
            
        }
        private void Invite(string s)
        {
            mySocket.SendInvite(Ips[0].Ipaddress, Ports[0].Portnumber,Usernames[0].Username,MyMessage.msgType.Connect);
        }
        private void Listen(string s)
        {
            mySocket.StartListen(Ips[0].Ipaddress, Ports[0].Portnumber,Usernames[0].Username, MyMessage.msgType.Connect);
        }

        private void Chat(CHATHISTORY s)
        {
            if (s != null)
            {
                CVM.ViewChat(s);
                MVM.OnNav("chat");
            }
        }

        public void LoadValues()
        {
            ObservableCollection<IP> ips = new ObservableCollection<IP>();
            ObservableCollection<PORT> prt = new ObservableCollection<PORT>();
            ObservableCollection<USERNAME> usr = new ObservableCollection<USERNAME>();
            ObservableCollection<SEARCH> srh = new ObservableCollection<SEARCH>();

            ips.Add(new IP { Ipaddress = "127.0.0.1" });
            prt.Add(new PORT { Portnumber = "8080"});
            usr.Add(new USERNAME { Username = "John Doe" });
            srh.Add(new SEARCH { });

            Ips = ips;
            Ports = prt;
            Usernames = usr;
            Searches = srh;
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            string addPath = "../../History/";  
            string path = Path.Combine(docPath, addPath);
            ObservableCollection<CHATHISTORY> history = new ObservableCollection<CHATHISTORY>();
        
            FileInfo[] AllFiles = new DirectoryInfo(path).GetFiles();
            var sorted =
            from cust in AllFiles
            orderby cust.LastWriteTime descending
            select cust;
            foreach (var f in sorted)
            {
                history.Add(new CHATHISTORY { Chathistory = f.ToString() });
            }
            ChatHistory = history;
        }

       

    
    }
}