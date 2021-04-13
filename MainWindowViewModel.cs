using MVVMDemo.ViewModel;
using MVVMDemo.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemo
{

    public class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel()
        {

            NavCommand = new MyICommand<string>(OnNav); 
            System.Diagnostics.Debug.WriteLine("KONSTRUKTOR");
        }


        private StudentViewModel studentViewModel;
        
        private ChatViewModel chatViewModel;

        private BindableBase _CurrentViewModel;
        public void getRef(ref ChatViewModel C, ref StudentViewModel S)
        {
            chatViewModel = C; studentViewModel = S; CurrentViewModel = studentViewModel; System.Diagnostics.Debug.WriteLine("Get REF MAIN!");
        }
        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public MyICommand<string> NavCommand { get; private set; }
        
        public void OnNav(string destination)
        {

            switch (destination)
            {
                case "main":
                    CurrentViewModel = studentViewModel;
                    break;
                case "chat":
                default:
                    CurrentViewModel = chatViewModel;
                    break;
            }
        }
    
    }
}