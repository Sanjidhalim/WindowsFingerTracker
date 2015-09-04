using System;
using System.Collections.Generic;
using System.Diagnostics; //Remove maybe?
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FingerTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Test2 : Page
    {

        int[] positions = { 125,125,40, 430, 430, 20, 220, 220, 10, 500, 500, 15,
                          375, 281, 70, 600, 500, 55};
        int curItem = 0;
        int Touchpoints = 0;
        Stopwatch sw;

        public Test2()
        {
            this.InitializeComponent();
            sw = new Stopwatch();
            sw.Start();
            setEllipse1();
            Ellipse1.PointerEntered += Button1Entered; 
        }

        

        async private void Button2Entered(object sender, PointerRoutedEventArgs e)
        {
            await Task.Delay(500);
            Ellipse1.Visibility = Visibility.Collapsed;
            Ellipse2.Visibility = Visibility.Collapsed;

            if (curItem < positions.Length)
            {
                await Task.Delay(1000);
                setEllipse1();

                Debug.WriteLine("Entered Button 2. Decouping 2 and Coupling 1");
                Ellipse2.PointerEntered -= Button2Entered;
                Ellipse1.PointerEntered += Button1Entered;

            }
            else {
                curItem = 0;
                sw.Stop();
                this.Frame.Navigate(typeof(MainPage));
            }

        }

        private void Button1Entered(object sender, PointerRoutedEventArgs e)
        {
            Canvas.SetTop(Ellipse2, positions[curItem]);
            curItem++;
            Canvas.SetLeft(Ellipse2, positions[curItem]);
            curItem++;
            Ellipse2.Width = positions[curItem];
            Ellipse2.Height = positions[curItem];
            curItem++;
            Ellipse2.Visibility = Visibility.Visible;

            //couple and decouple event handlers
            Debug.WriteLine("Entered Button 1. Decouping 1 and Coupling 2");
            Ellipse1.PointerEntered -= Button1Entered;
            Ellipse2.PointerEntered += Button2Entered;
            
        }

        private void setEllipse1() {
            Canvas.SetTop(Ellipse1, positions[curItem]);
            curItem++;
            Canvas.SetLeft(Ellipse1, positions[curItem]);
            curItem++;
            Ellipse1.Width = positions[curItem];
            Ellipse1.Height = positions[curItem];
            curItem++;
            Ellipse1.Visibility = Visibility.Visible;

            //setEventHandler
            //Ellipse1.PointerEntered+=Button1Entered; 

        }

        private void backGndPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Touchpoints++;
            PointerPoint pt = e.GetCurrentPoint(canvas);
            double xPoint = pt.Position.X;
            double yPoint = pt.Position.Y;
            textbox.Text = Touchpoints.ToString() + " X:" + (int) xPoint + " Y:" + (int) yPoint
                + " Time: " + sw.ElapsedMilliseconds;
        }

        
    }
}
