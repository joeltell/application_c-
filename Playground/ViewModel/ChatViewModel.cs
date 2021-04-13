using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMDemo.Model;
using System.Collections.ObjectModel;

namespace MVVMDemo.ViewModel
{
    public class ChatViewModel : BindableBase
    {
        public MyICommand<String> SendCommand { get; set; }
        
        public ChatViewModel()
        {
            SendCommand = new MyICommand<String>(SendMsg);
            ObservableCollection<CurrentMessage> newMsg = new ObservableCollection<CurrentMessage>();
            newMsg.Add(new CurrentMessage { CurrentMsg = "Your messages goes here" });
            NewMsg = newMsg;
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

        public StudentViewModel SVM;
        public MainWindowViewModel MVM;
        public void getRef(ref StudentViewModel S, ref MainWindowViewModel M) { 
            SVM = S; MVM = M;
            System.Diagnostics.Debug.WriteLine("Get REF Chat!");
        }
        private void SendMsg(string m)
        {
            string msg = NewMsg[0].CurrentMsg;
            System.Diagnostics.Debug.WriteLine("Sending message");  
            
            SVM.mySocket.SendMessage(m);
        }
    }
}
