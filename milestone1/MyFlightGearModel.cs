using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

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
        private Dictionary<string, ArrayList> dict;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
            this.simulatorspeed = 1.00;
            createProperties();

        }

        private void initializeDictionary()
        {
            dict = new Dictionary<string, ArrayList>();
            int len = array.Count;
            for (int i = 0; i < properties.Length; i++)
            {
                ArrayList arr=new ArrayList();
                int j = 0;
                while (j < len)
                {
                    string line = array[j].ToString();
                    string[] data = line.Split(",");
                    arr.Add(data[i]);
                    j++;
                }
                if(dict.ContainsKey(properties[i]))
                    dict.Add(String.Concat(properties[i],"1"), arr);
                else
                    dict.Add(properties[i], arr);
            }
        }

        double avg(ArrayList x)
        {
            double sum = 0;
            int size = x.Count;
            for (int i = 0; i < size; i++)
            {
                sum += (float)x[i];
            }
            return sum / size;
        }
        double var(ArrayList x)
        {
            double sum1 = 0, avg1 = 0, avg2 = 0;
            int size = x.Count;
            for (int i = 0; i < size; i++)
            {
                sum1 += Math.Pow((double)x[i], 2);
            }
            avg1 = sum1 / size;
            avg2 = avg(x);
            return avg1 - Math.Pow((double)avg2, 2);
        }

        double cov(ArrayList x, ArrayList y)
        {
            double cov = 0, sum = 0;
            int size = x.Count;
            // getting the average of X and Y
            double Ex = avg(x);
            double Ey = avg(y);
            for (int i = 0; i < size; i++)
            {
                sum += ((double)x[i] - Ex) * ((double)y[i] - Ey);
            }
            cov = sum / size;
            return cov;
        }
        double pearson(ArrayList x, ArrayList y)
        {
            double sigmaX = Math.Sqrt((double)var(x));
            double sigmaY = Math.Sqrt((double)var(y));
            return cov(x, y) / (sigmaX * sigmaY);
        }


        string correlatedProperty(string property)
        {
            double maxCorrl = 0;
            double corrl;
            string toRet="";
            foreach(string key in dict.Keys)
            {
                if (property.Equals(key))
                    continue;
                else
                {
                    corrl = Math.Abs(pearson(dict[property], dict[key]));
                    if (corrl > maxCorrl)
                    {
                        maxCorrl = corrl;
                        toRet = key;
                    }

                }
            }
            return toRet;
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
            initializeDictionary();
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
                            Aileron = (float)Convert.ToDouble(dict["aileron"][currentLine]);
                            Elevator = (float)Convert.ToDouble(dict["elevator"][currentLine]);
                            Rudder = (float)Convert.ToDouble(dict["rudder"][currentLine]);
                            Throttle = (float)Convert.ToDouble(dict["throttle"][currentLine]);
                            Altitude = (float)Convert.ToDouble(dict["altitude-ft"][currentLine]);
                            RollDeg = (float)Convert.ToDouble(dict["roll-deg"][currentLine]);
                            PitchDeg = (float)Convert.ToDouble(dict["pitch-deg"][currentLine]);
                            HeadingDeg = (float)Convert.ToDouble(dict["heading-deg"][currentLine]);
                            YawDeg = (float)Convert.ToDouble(dict["side-slip-deg"][currentLine]);
                            AirSpeed = (float)Convert.ToDouble(dict["airspeed-kt"][currentLine]);
                            //Aileron = (float)Convert.ToDouble(data[0]);
                            //Elevator = (float)Convert.ToDouble(data[1]);
                            //Rudder = (float)Convert.ToDouble(data[2]);
                            //Throttle = (float)Convert.ToDouble(data[6]);
                            //Altitude = (float)Convert.ToDouble(data[16]);
                            //RollDeg= (float)Convert.ToDouble(data[17]);
                            //PitchDeg=(float)Convert.ToDouble(data[18]);
                            //HeadingDeg=(float)Convert.ToDouble(data[19]);
                            //YawDeg=(float)Convert.ToDouble(data[20]);
                            //AirSpeed=(float)Convert.ToDouble(data[21]);
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