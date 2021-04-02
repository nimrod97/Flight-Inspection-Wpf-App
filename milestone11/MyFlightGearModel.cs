using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

namespace milestone1
{
    class MyFlightGearModel : IFlightGearModel
    {
        ITelnetClient telnetClient;
        volatile Boolean isStopped;
        volatile Boolean isPaused;
        private ArrayList array;
        private int currentLine;
        private double sliderValue;


        public double SliderValue
        {
            get
            {
                return sliderValue;
            }
            set
            {
                sliderValue = value;
                NotifyPropertyChanged("SliderValue");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
        }
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
        }

        //public void disconnect()
        //{
        //    isStopped = true;
        //}

        public void createLocalFile(string path)
        {
            this.array = new ArrayList();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        array.Add(currentLine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void pause()
        {
            isPaused = true;
        }

        public void resume()
        {
            isPaused = false;
        }

        public void stop()
        {
            isStopped = true;
            telnetClient.disconnect();
        }

        public void start(string path)
        {
                createLocalFile(path);
                new Thread(delegate ()
                {

                    currentLine = 0;
                    int len = array.Count;
                    Boolean innerStopped=false;
                    while (!isStopped)
                    {
                        for (; currentLine < len; currentLine++)
                        // foreach (string line in array)
                        {
                            string line = array[currentLine].ToString();
                            SliderValue = getSliderValue();
                            if (!isPaused && !isStopped)
                            {
                                telnetClient.write(line);
                                Thread.Sleep(100);
                            }
                            else if (isPaused)
                            {
                                while (isPaused) { }

                            }
                            else // if (isStopped)
                            {
                                innerStopped = true;
                                telnetClient.disconnect();
                                break;
                            }
                        }
                        if (innerStopped)
                            break;
                    }
                   


                }).Start();
            


        }
        
        public void moveSlider(double value)
        {
            // isPaused = true;
            currentLine = Convert.ToInt32((value / 100.0) * array.Count);
        }

        public double getSliderValue()
        {
            return Convert.ToDouble((currentLine * 100) / array.Count);
        }



        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    
    }
}