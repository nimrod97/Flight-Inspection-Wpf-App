using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using OxyPlot;
using OxyPlot.Series;

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
        private double simulatorspeed;
        private float altitude;
        private float airSpeed;
        private float headingDeg;
        private float pitchDeg;
        private float rollDeg;
        private float yawDeg;
        private float aileron;
        private float elevator;
        private float rudder;
        private float throttle;
        private IList<DataPoint> pointsCurrentChoice;

        private string[] properties;

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

        public double SimulatorSpeed
        {
            get
            {
                return simulatorspeed;
            }
            set
            {
                simulatorspeed = value;
            }
        }

        public float Altitude
        {
            get
            {
                return altitude;
            }
            set
            {
                altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }
        public float AirSpeed
        {
            get
            {
                return airSpeed;
            }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public float HeadingDeg
        {
            get
            {
                return headingDeg;
            }
            set
            {
                headingDeg = value;
                NotifyPropertyChanged("HeadingDeg");
            }
        }
        public float PitchDeg
        {
            get
            {
                return pitchDeg;
            }
            set
            {
                pitchDeg = value;
                NotifyPropertyChanged("PitchDeg");
            }
        }
        public float RollDeg
        {
            get
            {
                return rollDeg;
            }
            set
            {
                rollDeg = value;
                NotifyPropertyChanged("RollDeg");
            }
        }
        public float YawDeg
        {
            get
            {
                return yawDeg;
            }
            set
            {
                yawDeg = value;
                NotifyPropertyChanged("YawDeg");
            }
        }

        public float Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public float Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public float Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public string[] Properties
        {
            get
            {
                return properties;
            }
        }

        public IList<DataPoint> PointsCurrentChoice
        {
            get
            {
                return pointsCurrentChoice;
            }
            set
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
            this.simulatorspeed = 1.00;
            createProperties();
            pointsCurrentChoice = new List<DataPoint>();
/*            pointsCurrentChoice.Add(new DataPoint(0, 0));
            pointsCurrentChoice.Add(new DataPoint(3, 3));*/


        }

        private void createProperties()
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            FileStream fs = new FileStream("playback_small.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            XmlNodeList xmlnode = xmldoc.DocumentElement.SelectNodes("/PropertyList/generic/output/chunk");
            int count = xmlnode.Count;
            properties = new string[count];
            for (int i = 0; i < count; i++)
            {
                properties[i] = xmlnode[i].SelectSingleNode("name").InnerText;
            }
            fs.Close();
        }
 
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
        }

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
            if(simulatorspeed!=0)
                isPaused = false;
        }

        public void stop()
        {
            isStopped = true;
            telnetClient.write(array[array.Count-1].ToString());
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
                        {
                            string line = array[currentLine].ToString();
                            string[] data = line.Split(",");
                            SliderValue = getSliderValue();
                            Aileron= (float)Convert.ToDouble(data[0]);
                            Elevator = (float)Convert.ToDouble(data[1]);
                            Rudder = (float)Convert.ToDouble(data[2]);
                            Throttle = (float)Convert.ToDouble(data[6]);
                            Altitude = (float)Convert.ToDouble(data[16]);
                            RollDeg= (float)Convert.ToDouble(data[17]);
                            PitchDeg=(float)Convert.ToDouble(data[18]);
                            HeadingDeg=(float)Convert.ToDouble(data[19]);
                            YawDeg=(float)Convert.ToDouble(data[20]);
                            AirSpeed=(float)Convert.ToDouble(data[21]);
                            if (!isPaused && !isStopped)
                            {
                                telnetClient.write(line);
                                Thread.Sleep((int)(100.0 / SimulatorSpeed));
                            }
                            else if (isPaused)
                            {
                                while (isPaused) { }
                            }
                            else // if (isStopped)
                            {
                                innerStopped = true;
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
            SliderValue = value;
            if (isPaused&&currentLine<array.Count)
            {
                telnetClient.write(array[currentLine].ToString());
                Thread.Sleep(100);
            }

        }

        public double getSliderValue()
        {
            return Convert.ToDouble((currentLine * 100) / array.Count);
        }

        public void moveSimulatorSpeed(double value)
        {
            if (value == 0)
            {
                pause();
            }
            else
                resume();
            SimulatorSpeed = value;
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    
    }
}