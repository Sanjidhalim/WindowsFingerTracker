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
        uint pId = 0;
        int Touchpoints = 0;
        private DataStorage ds;
        Stopwatch sw;

        public Test2()
        {
            this.InitializeComponent();
            ds = new DataStorage();
            sw = new Stopwatch();
            sw.Start();
            setEllipse1();
            Ellipse3.PointerPressed += Button1Entered;
            Ellipse3.PointerEntered += Button1Entered;
        }

        

        async private void Button2Entered(object sender, PointerRoutedEventArgs e)
        {

            Debug.WriteLine("Entered the Secoond Button :");
            if (!CapturePointer(e.Pointer)) {
                Debug.WriteLine("Could not capture pointer.");
                e.Handled = true;
                return;
            }
            ds.iHaveEnteredAButton();
            blinkMethod(false);

            Ellipse4.PointerEntered -= Button2Entered;
            Ellipse4.PointerPressed -= Button2Entered;

            Ellipse3.PointerPressed += Button1Entered;
            Ellipse3.PointerEntered += Button1Entered;

            await Task.Delay(1000);
            Ellipse1.Visibility = Visibility.Collapsed;
            Ellipse2.Visibility = Visibility.Collapsed;

            if (!ds.endOfCircles())
            {
                await Task.Delay(1000);
                setEllipse1();

                Debug.WriteLine("Entered Button 2. Decouping 2 and Coupling 1");

            }
            else {
                sw.Stop();
                ds.writeToFile();
                await Task.Delay(2000);
                Application.Current.Exit();
            }

        }

        private void Button1Entered(object sender, PointerRoutedEventArgs e)
        {

            if (!CapturePointer(e.Pointer))
            {
                e.Handled = true;
                return;
            }
            else {
                ReleasePointerCapture(e.Pointer);
            }

            ds.iHaveEnteredAButton();
            blinkMethod(true);

            //couple and decouple event handlers
            Debug.WriteLine("Entered Button 1. Decouping 1 and Coupling 2");
            Ellipse3.PointerPressed -= Button1Entered;
            Ellipse3.PointerEntered -= Button1Entered;
            Ellipse4.PointerEntered += Button2Entered;
            Ellipse4.PointerPressed += Button2Entered;

 
        }

        private void setEllipse1() {
            int[] circles = ds.getNextCircle();

            int temp = circles[0];
            int temp2 = circles[1];
            int smallWidth = 30;

            circles[0] = circles[0] - circles[2] / 2;
            circles[1] = circles[1] - circles[2] / 2;

            Canvas.SetTop(Ellipse1, circles[0]);
            Canvas.SetLeft(Ellipse1, circles[1]);
            Ellipse1.Width = circles[2];
            Ellipse1.Height = circles[2];
            Ellipse1.Visibility = Visibility.Visible;

            Canvas.SetTop(Ellipse3, temp - smallWidth/2 );
            Canvas.SetLeft(Ellipse3, temp2 - smallWidth/2);
            Ellipse3.Width = smallWidth;
            Ellipse3.Height = smallWidth;
            Ellipse3.Visibility = Visibility.Visible;



            circles = ds.getNextCircle();
            temp = circles[0];
            temp2 = circles[1];

            circles[0] = circles[0] - circles[2] / 2;
            circles[1] = circles[1] - circles[2] / 2;

            Canvas.SetTop(Ellipse2, circles[0]);
            Canvas.SetLeft(Ellipse2, circles[1]);
            Ellipse2.Width = circles[2];
            Ellipse2.Height = circles[2];
            Ellipse2.Visibility = Visibility.Visible;

            Canvas.SetTop(Ellipse4, temp - smallWidth / 2);
            Canvas.SetLeft(Ellipse4, temp2 - smallWidth / 2);
            Ellipse4.Width = smallWidth;
            Ellipse4.Height = smallWidth;
            Ellipse4.Visibility = Visibility.Visible;
        }

        private void backGndPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            bool boolean = true; ;

            if (!CapturePointer(e.Pointer))
            {
                boolean = false;
            }
            else
            {
                ReleasePointerCapture(e.Pointer);
            }
            Touchpoints++;
            PointerPoint pt = e.GetCurrentPoint(canvas);
            double xPoint = pt.Position.X;
            double yPoint = pt.Position.Y;
            ds.addData(xPoint, yPoint, (int)sw.ElapsedMilliseconds, boolean);

            /*Rect x = Window.Current.Bounds;
            Debug.WriteLine("Top y: " + x.Top);
            Debug.WriteLine("Bottom y: " + x.Bottom);
            Debug.WriteLine("Left x: " + x.Left);
            Debug.WriteLine("Right x: " + x.Right);*/

            //textBox.Text = Touchpoints.ToString() + " X:" + (int) xPoint + " Y:" + (int) yPoint
            //    + " Time: " + sw.ElapsedMilliseconds;
        }

        async void blinkMethod(bool Bool) {
            if (Bool)
            {
                double height = Ellipse1.ActualHeight;
                double width = Ellipse1.ActualWidth;

                double left = Canvas.GetLeft(Ellipse1);
                double top = Canvas.GetTop(Ellipse1);

                Canvas.SetTop(Ellipse1, top- height/2 );
                Canvas.SetLeft(Ellipse1, left - width/2);
                Ellipse1.Height = height * 2;
                Ellipse1.Width = width * 2;

                await Task.Delay(100);

                Canvas.SetTop(Ellipse1, top);
                Canvas.SetLeft(Ellipse1, left);
                Ellipse1.Height = height;
                Ellipse1.Width = width;

            }
            else {
                double height = Ellipse2.ActualHeight;
                double width = Ellipse2.ActualWidth;

                double left = Canvas.GetLeft(Ellipse2);
                double top = Canvas.GetTop(Ellipse2);

                Canvas.SetTop(Ellipse2, top - height / 2);
                Canvas.SetLeft(Ellipse2, left - width / 2);
                Ellipse2.Height = height * 2;
                Ellipse2.Width = width * 2;

                await Task.Delay(100);

                Canvas.SetTop(Ellipse2, top);
                Canvas.SetLeft(Ellipse2, left);
                Ellipse2.Height = height;
                Ellipse2.Width = width;
            }
        } 

        
    }
}
