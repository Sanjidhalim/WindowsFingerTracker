using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FingerTracker
{
    public class DataStorage
    {
        private List<Node> touchData= new List<Node>();
        private int[] circleData;
        private List<int> splitCircleData= new List<int>();
        int curPos;

        public DataStorage(){
            touchData = new List<Node>();
            readFromFile();
            Debug.WriteLine("reading from file");
            curPos = 0;
        }

        public void addData(double x, double y, int time){
            touchData.Add(new Node(x,y,time));
        }

        public int[] getNextCircle() {
            int[] sub = new int[3];
            Array.Copy(circleData, curPos, sub, 0, 3);
            curPos += 3;

            return sub;
        }

        public bool endOfCircles() {
            return  (curPos == circleData.Length);
        }

        public void iHaveEnteredAButton() {
            splitCircleData.Add(touchData.Count);
            }


        public async void writeToFile() {

            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("Results.txt", CreationCollisionOption.ReplaceExisting);

            StringBuilder strBld = new StringBuilder();
            int start, end;
            int pair = 1;

            for (int i = 0; i < splitCircleData.Count; i += 2) {
                strBld.Append("\nData for pair " + pair + "\n");
                pair++;

                start = splitCircleData[i]-1;
                end = splitCircleData[i + 1]-1;

                for (int j = start; j < end; j++) {
                    strBld.Append(touchData[j].customToString());
                    strBld.Append('\n');
                }

            }
            //Debug.WriteLine(strBld.ToString());
            await Windows.Storage.FileIO.WriteTextAsync(file, strBld.ToString());
        }

        async public void readFromFile() {
            Debug.WriteLine("At the start  of reading files");
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync("circleData.txt");

            String data = await Windows.Storage.FileIO.ReadTextAsync(file);
            Debug.WriteLine("Read File");


            String[] parsedData = data.Split('\n');
            circleData = new int[parsedData.Length];
            int temp = 0;

            foreach (String s in parsedData) {
                circleData[temp] = Int32.Parse(s);
                temp++;
            }
            Debug.WriteLine("At the end of reading files");
        }
        
    }

    
    
    public struct Node {
        public double xPos;
        public double yPos;
        public int timeStamp;

        public Node(double x, double y, int t) {
            xPos = x;
            yPos = y;
            timeStamp = t;
        }

        public string customToString() {
            return (xPos + ", " + yPos + ", " + timeStamp);
        }
    }
}
