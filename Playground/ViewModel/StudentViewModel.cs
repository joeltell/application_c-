using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMDemo.Model;
using System.Collections.ObjectModel;

namespace MVVMDemo.ViewModel
{

    public class StudentViewModel : BindableBase
    {
        
        public MyICommand<String> InviteCommand { get; set; }
        public MyICommand<String> ListenCommand { get; set; }
        
        public MySocket mySocket = new MySocket();
        public ChatViewModel CVM;
        public MainWindowViewModel MVM;
        public void getRef(ref ChatViewModel C, ref MainWindowViewModel M) { 
            CVM = C; MVM = M; mySocket.getRef(ref M, ref C);
            System.Diagnostics.Debug.WriteLine("Get REF Stud!");
            if (CVM == null) System.Diagnostics.Debug.WriteLine("C null");
            if (MVM == null) System.Diagnostics.Debug.WriteLine("M null");
            if (mySocket == null) System.Diagnostics.Debug.WriteLine("Socket null");
        }

        public StudentViewModel()
        {
           
            LoadValues();
            
            InviteCommand = new MyICommand<String>(Invite);
            ListenCommand = new MyICommand<String>(Listen);
            
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

        

        private void Invite(string s)
        {
            System.Diagnostics.Debug.WriteLine("invite");
            System.Diagnostics.Debug.WriteLine(Ips[0].Ipaddress);
            System.Diagnostics.Debug.WriteLine(Ports[0].Portnumber);
            //ropa invite bla bla bla
            mySocket.SendInvite(Ips[0].Ipaddress, Ports[0].Portnumber);


        }
        private void Listen(string s)
        {
            System.Diagnostics.Debug.WriteLine("listen");
            System.Diagnostics.Debug.WriteLine(Ips[0].Ipaddress);
            System.Diagnostics.Debug.WriteLine(Ports[0].Portnumber);
            //ropa på listen socket_kod
            mySocket.StartListen(Ips[0].Ipaddress, Ports[0].Portnumber);


        }

        public void LoadValues()
        {
            ObservableCollection<IP> ips = new ObservableCollection<IP>();
            ObservableCollection<PORT> prt = new ObservableCollection<PORT>();

            ips.Add(new IP { Ipaddress = "127.0.0.1" });
            prt.Add(new PORT { Portnumber = "8080"});

            Ips = ips;
            Ports = prt;
        }

       

    
    }
}