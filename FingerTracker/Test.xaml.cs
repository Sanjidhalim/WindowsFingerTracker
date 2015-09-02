using FingerTracker.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace FingerTracker
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Test : Page
    {

        
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Button btn;
        private Button btn2;
        private int[,] points =new int[,] {{100,150,10},{20,70,10}};


        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Test()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        
         void startTest(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 1; i++) {
                createButtons();
            }
            
        }

         public void createButtons() {
             btn = new Button();
             myCanvas.Children.Add(btn);
             btn.Width = 130;
             btn.Height = 66;
             btn.ClickMode = ClickMode.Press; 
             Canvas.SetTop(btn, 45);
             Canvas.SetLeft(btn, 45);
             btn.Click += new RoutedEventHandler(firstButtonHandler);
         }


         private void firstButtonHandler(object sender, RoutedEventArgs e)
         {
             Button testBtn= (Button)FindName("Button2");
             if (testBtn == null)
             {
                 Canvas canvas = (Canvas)FindName("myCanvas");
                 btn2 = new Button();
                 btn2.Name = "Button2";
                 btn2.Width = 130;
                 btn2.Height = 66;
                 Canvas.SetTop(btn2, 160);
                 Canvas.SetLeft(btn2, 160);
                 canvas.Children.Add(btn2);
                 btn2.ClickMode = ClickMode.Press;
                 btn2.Click += new RoutedEventHandler(secondButtonHandler);
             }
         }

         private void secondButtonHandler(object sender, RoutedEventArgs e)
         {
             myCanvas.Children.Remove(btn);
             myCanvas.Children.Remove(btn2);
         }

        
    }
}
