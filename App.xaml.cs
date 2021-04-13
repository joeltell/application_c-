using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVMDemo.ViewModel;
using MVVMDemo.Views;

namespace MVVMDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow app = new MainWindow();
            
            MainWindowViewModel context = new MainWindowViewModel();
            
            StudentViewModel SVM = new StudentViewModel();
            ChatViewModel CVM = new ChatViewModel();
            SVM.getRef(ref CVM, ref context);
            CVM.getRef(ref SVM, ref context);
            context.getRef(ref CVM, ref SVM);
            app.DataContext = context;
            
            
            app.Show();
            System.Diagnostics.Debug.WriteLine("INITIAL LOAD!");
        }
    }
}
