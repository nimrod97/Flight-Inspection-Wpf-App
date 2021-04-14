using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Runtime.InteropServices;
using System.Linq;

namespace milestone1
{

    //class Point
    //{
    //    double x, y;
    //    public Point(double x, double y) { 
    //        this.x =x;
    //        this.y = y;
    //    }
    //};

    //class Line
    //{

    //    double a, b;
    //    Line() {
    //        a = 0;
    //        b = 0;
    //    }
    //    public Line(double a, double b) {
    //        this.a = a;
    //        this.b = b;
    //    }
    //    double f(double x) { return a * x + b; }
    //    // finding the intersection point between 2 lines
    //    Point intersection(Line l1)
    //    {
    //        double x = this.a - l1.a;
    //        double w = this.b - l1.b;
    //        double xPoint = (w * -1) / x;
    //        double yPoint = f(xPoint);
    //        return new Point(xPoint, yPoint);
    //    }
    //};


    public struct correlatedFeatures
    {
        public string feature1, feature2;
        public double correlation;
    };

    public struct AnomalyReport
    {
        public int timeStep;
        public string feature1;
        public string feature2;
    }


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
<<<<<<< HEAD
=======

        private PlotModel plotModelCurrent;
        private PlotModel plotModelRegression;

        private string currerntChoice;
        private string correlatedChoice;

>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
        private Dictionary<string, ArrayList> dict;
        private string[] properties;
        private List<correlatedFeatures> SimpleCorrelatedFeaturesArr;
        private List<AnomalyReport> SimpleAnomalyReportArr;
        private List<correlatedFeatures> CircleCorrelatedFeaturesArr;
        private List<AnomalyReport> CircleAnomalyReportArr;

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

        public string CurrerntChoice
        {
            get
            {
                return currerntChoice;
            }
            set
            {
                currerntChoice = value;
            }
        }

        public PlotModel PlotModelCurrent
        {

            get { return plotModelCurrent; }
            set { plotModelCurrent = value;}
        }

        public PlotModel PlotModelRegression
        {
            get { return plotModelRegression; }
            set { plotModelRegression = value; }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
            this.simulatorspeed = 1.00;
            createProperties();
<<<<<<< HEAD

        }

=======
            SetUpGraphOfCurrent();
            SetUpGraphOfRegression();
            this.currerntChoice = null;
            this.correlatedChoice = null;
        }

        private void SetUpGraphOfCurrent()
        {
            plotModelCurrent = new PlotModel();
            LineSeries l = new LineSeries();
            plotModelCurrent.Series.Add(l);
        }
        private void SetUpGraphOfRegression()
        {
            plotModelRegression = new PlotModel();
            LineSeries l = new LineSeries();
            plotModelRegression.Series.Add(l);
        }

        public void initializingComponentsByPath(string path)
        {
            createLocalFile(path);
            initializeDictionary();
        }


>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
        private void initializeDictionary()
        {
            dict = new Dictionary<string, ArrayList>();
            int len = array.Count;
            for (int i = 0; i < properties.Length; i++)
            {
<<<<<<< HEAD
                ArrayList arr=new ArrayList();
=======
                ArrayList arr = new ArrayList();
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
                int j = 0;
                while (j < len)
                {
                    string line = array[j].ToString();
                    string[] data = line.Split(",");
<<<<<<< HEAD
                    arr.Add(data[i]);
                    j++;
                }
                if(dict.ContainsKey(properties[i]))
                    dict.Add(String.Concat(properties[i],"1"), arr);
=======
                    arr.Add(Convert.ToDouble(data[i]));
                    j++;
                }
                if (dict.ContainsKey(properties[i]))
                    dict.Add(String.Concat(properties[i], "1"), arr);

>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
                else
                    dict.Add(properties[i], arr);
            }
        }
<<<<<<< HEAD

=======
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
        double avg(ArrayList x)
        {
            double sum = 0;
            int size = x.Count;
            for (int i = 0; i < size; i++)
            {
<<<<<<< HEAD
                sum += (float)x[i];
=======
                sum += (double)x[i];
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
<<<<<<< HEAD

=======
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
<<<<<<< HEAD


=======
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
        string correlatedProperty(string property)
        {
            double maxCorrl = 0;
            double corrl;
<<<<<<< HEAD
            string toRet="";
            foreach(string key in dict.Keys)
=======
            string toRet = "";
            foreach (string key in dict.Keys)
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
<<<<<<< HEAD

                }
            }
            return toRet;
=======
                }
            }
            return toRet;
        }

        // performs a linear regression and returns the line equation
        LineSeries linearReg(ArrayList points)
        {
            int size = points.Count;
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();
            for (int i = 0; i < size; i++)
            {
                DataPoint p1 = (DataPoint)points[i];
                DataPoint p2 = (DataPoint)points[i];
                x.Add((double)p1.X);
                y.Add((double)p2.Y);
            }
            double a = cov(x, y);
            double b = avg(y) - a * (avg(x));
            LineSeries line = new LineSeries();
            for (int i = 0; i < size; i ++)
            {
                double maor = (double)x[i];
                double newY = a * i + b;
                line.Points.Add(new DataPoint(i, newY));
            }
            return line;
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
                string name = xmlnode[i].SelectSingleNode("name").InnerText;
                int num = numOfInstances(properties, name);
                if (num == 0)
                {
                    properties[i] = name;
                }
                else
                {
                    properties[i] = String.Concat(name, num);
                }
            }
            fs.Close();
        }

        private int numOfInstances(string[] arr, string str)
        {
            int count = 0;
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                if (arr[i] == null)
                    break;
                if (arr[i].Equals(str))
                    count++;
            }
            return count;
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
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        array.Add(line);
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
            if (simulatorspeed != 0)
                isPaused = false;
        }

        public void stop()
        {
            isStopped = true;
            telnetClient.write(array[array.Count - 1].ToString());
            telnetClient.disconnect();
        }

        public void start()
        {
<<<<<<< HEAD
            createLocalFile(path);
            initializeDictionary();
                new Thread(delegate ()
=======
            currentLine = 0;
            string lastChoice = null;
            int lastLine = 0;

            new Thread(delegate ()
>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
                {
                    int len = array.Count;
                    Boolean innerStopped = false;
                    while (!isStopped)
                    {
                        for (; currentLine < len; currentLine++)
                        {
                            string line = array[currentLine].ToString();
                            string[] data = line.Split(",");
                            SliderValue = getSliderValue();
<<<<<<< HEAD
=======

>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
<<<<<<< HEAD
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
=======

>>>>>>> 23640bab86b6c0591f932ae2499580958ae27240
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
            new Thread(delegate ()
            {
                buildGraph(ref lastChoice, ref lastLine, ref currerntChoice, ref plotModelCurrent);
            }).Start();
            /* new Thread(delegate ()
            {
                while (!isStopped)
                {
                    if (currerntChoice != null && correlatedChoice != null)
                    {
                        if (lastChoice == null || !lastChoice.Equals(currerntChoice)) // the user changed the his the first choice
                        {
                            ArrayList x = dict[currerntChoice];
                            ArrayList y = dict[correlatedChoice];
                            ArrayList points = new ArrayList();
                            int len = x.Count;
                            for (int i = 0; i < len; i++)
                            {
                                points.Add(new DataPoint((double)Convert.ToDouble(x[i]), (double)Convert.ToDouble(y[i])));
                            }
                            LineSeries lineOfLinearReg = linearReg(points);
                            if (plotModelRegression.Series[0] != null)
                                plotModelRegression.Series.RemoveAt(0);
                            plotModelRegression.Series.Add(lineOfLinearReg);

                            LineSeries line = new();
                            for (int i = currentLine - (currentLine % 10); i >= 0 && i >= currentLine - 30; i -= 10)
                            {

                            }

                            plotModelRegression.InvalidatePlot(true);
                        }
                        else // the user didn't change his choice
                        {

                        }
                    }
                }
            }).Start();*/
        }

        public void SimpleAnomalyDetector(string learnFile, string testFile)
        {
            new Thread(delegate ()
            {
                //learn

                IntPtr vec = DllSimple.CreateSimpleAnomalyDetector();
                List<string> prop = new List<string>(dict.Keys);
                DllSimple.SimpleLearnNormal(vec, learnFile, prop.ToArray(), prop.Count);
                int vecSize = DllSimple.SimpleVectorCorrelatedFeaturesSize(vec);
                correlatedFeatures cf;
                SimpleCorrelatedFeaturesArr = new List<correlatedFeatures>(vecSize);
                for (int i = 0; i < vecSize; i++)
                {
                    cf.feature1 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllSimple.getFeature1(vec, i));
                    cf.feature2 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllSimple.getFeature2(vec, i));
                    cf.correlation = DllSimple.getCorrelationValue(vec, i);
                    SimpleCorrelatedFeaturesArr.Add(cf);

                }

                // detect
                IntPtr vec1 = DllSimple.CreateSimpleAnomalyDetector();
                DllSimple.SimpleDetect(vec1, testFile, prop.ToArray(), properties.Length);
                int vecSize1 = DllSimple.SimpleVectorAnomalyReportSize(vec1);
                AnomalyReport ar;
                SimpleAnomalyReportArr = new List<AnomalyReport>(vecSize1);
                for (int i = 0; i < vecSize1; i++)
                {
                    ar.feature1 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllSimple.getFeature1(vec1, i));
                    ar.feature2 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllSimple.getFeature2(vec1, i));
                    ar.timeStep = DllSimple.getTimeStep(vec1, i);
                    SimpleAnomalyReportArr.Add(ar);
                }

            }).Start();

        }

        public void CircleAnomalyDetector(string learnFile, string testFile)
        {
            new Thread(delegate ()
            {
                //learn normal

                IntPtr vec = DllCircle.CreateCircleAnomalyDetector();
                List<string> prop = new List<string>(dict.Keys);
                DllCircle.CircleLearnNormal(vec, learnFile, prop.ToArray(), prop.Count);
                int vecSize = DllCircle.CircleVectorCorrelatedFeaturesSize(vec);
                correlatedFeatures cf;
                CircleCorrelatedFeaturesArr = new List<correlatedFeatures>(vecSize);
                for (int i = 0; i < vecSize; i++)
                {
                    cf.feature1 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllCircle.getFeature1(vec, i));
                    cf.feature2 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllCircle.getFeature2(vec, i));
                    cf.correlation = DllCircle.getCorrelationValue(vec, i);
                    CircleCorrelatedFeaturesArr.Add(cf);
                }
                //detect

                IntPtr vec1 = DllCircle.CreateCircleAnomalyDetector();
                DllCircle.CircleDetect(vec1, testFile, prop.ToArray(), prop.Count);
                int vecSize1 = DllCircle.CircleVectorAnomalyReportSize(vec1);
                AnomalyReport ar;
                CircleAnomalyReportArr = new List<AnomalyReport>(vecSize1);
                for (int i = 0; i < vecSize1; i++)
                {
                    ar.feature1 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllCircle.getFeature1(vec1, i));
                    ar.feature2 = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(DllCircle.getFeature2(vec1, i));
                    ar.timeStep = DllCircle.getTimeStep(vec1, i);
                    CircleAnomalyReportArr.Add(ar);
                }
            }).Start();
        }

        private void buildGraph(ref string lastChoice, ref int lastLine, ref string choice, ref PlotModel plotModel)
        {
            while (!isStopped)
            {
                if (choice != null)
                {
                    if ((lastChoice == null || lastChoice.Equals(currerntChoice)) && (currentLine >= lastLine))
                    {
                        LineSeries l = (LineSeries)(plotModel.Series[0] as LineSeries);
                        if (currentLine - lastLine > 10)
                        {
                            buildLine(lastLine, currentLine, l, ref lastLine, ref choice);
                            lastChoice = choice;
                        }
                        else if (currentLine % 10 == 0 && currentLine != lastLine)
                        {
                            l.Points.Add(new DataPoint(currentLine / 10, (float)Convert.ToDouble(dict[choice][currentLine])));
                            plotModel.InvalidatePlot(true);
                            lastLine = Math.Max(lastLine, currentLine);
                            lastChoice = choice;
                        }
                    }
                    else
                    {
                        plotModel.Series.RemoveAt(0);
                        LineSeries l = new LineSeries();
                        buildLine(0, currentLine, l, ref lastLine, ref choice);
                        plotModel.Series.Add(l);
                        plotModel.InvalidatePlot(true);
                        lastChoice = choice;
                        lastLine = currentLine;
                    }
                }
            }
        }

        private void buildLine(int start, int end, LineSeries l, ref int lastLine, ref string choice)
        {
            for (int i = start; i <= end; i += 10)
            {
                l.Points.Add(new DataPoint(i / 10, (float)Convert.ToDouble(dict[choice][i])));
                lastLine = i;
            }
        }

        public void moveSlider(double value)
        {
            currentLine = Convert.ToInt32((value / 100.0) * array.Count);
            SliderValue = value;
            if (isPaused && currentLine < array.Count)
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
