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
        private double altitude;
        private double airSpeed;
        private double headingDeg;
        private double pitchDeg;
        private double rollDeg;
        private double yawDeg;
        private double aileron;
        private double elevator;
        private double rudder;
        private double throttle;
        private PlotModel plotModelCurrent;
        private PlotModel plotModelRegression;
        private PlotModel plotModelCurrentCorrelation;
        private string currerntChoice;
        private string correlatedChoice;

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
        public double Altitude
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
        public double AirSpeed
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
        public double HeadingDeg
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
        public double PitchDeg
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
        public double RollDeg
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
        public double YawDeg
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
        public double Aileron
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
        public double Elevator
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
        public double Rudder
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
        public double Throttle
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

        
        public PlotModel PlotModelCurrentCorrelation
        {
            get { return plotModelCurrentCorrelation; }
            set { plotModelCurrentCorrelation = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
            this.simulatorspeed = 1.00;
            createProperties();
            SetUpGraphOfCurrent();
            SetUpGraphOfCorrelated();
            SetUpGraphOfRegression();
            this.currerntChoice = null;
            this.correlatedChoice = null;
        }

        private void SetUpGraphOfCurrent()
        {
            plotModelCurrent = new PlotModel();
            plotModelCurrent.TitleFontSize = 14;
            TimeSpanAxis timeAxis = new TimeSpanAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10, Title = "Time" };
            plotModelCurrent.Axes.Add(timeAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left };
            plotModelCurrent.Axes.Add(valueAxis);


        }

        private void SetUpGraphOfCorrelated()
        {
            plotModelCurrentCorrelation = new PlotModel();
            plotModelCurrentCorrelation.TitleFontSize = 14;
            TimeSpanAxis timeAxis = new TimeSpanAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10, Title = "Time" };
            plotModelCurrentCorrelation.Axes.Add(timeAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left };
            plotModelCurrentCorrelation.Axes.Add(valueAxis);


        }




        private void SetUpGraphOfRegression()
        {
            plotModelRegression = new PlotModel();
            LineSeries l = new LineSeries();
            plotModelRegression.Series.Add(l);
            LinearAxis xAxis = new TimeSpanAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10};
            plotModelCurrentCorrelation.Axes.Add(xAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left };
            plotModelCurrentCorrelation.Axes.Add(valueAxis);
        }
        private void initializeDictionary()
        {
            dict = new Dictionary<string, ArrayList>();
            int len = array.Count;
            for (int i = 0; i < properties.Length; i++)
            {
                ArrayList arr = new ArrayList();
                int j = 0;
                while (j < len)
                {
                    string line = array[j].ToString();
                    string[] data = line.Split(",");
                    arr.Add(Convert.ToDouble(data[i]));
                    j++;
                }
                if (dict.ContainsKey(properties[i]))
                    dict.Add(String.Concat(properties[i], "1"), arr);

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
                sum += (double)x[i];
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

        public void initializingComponentsByPath(string path)
        {
            createLocalFile(path);
            initializeDictionary();
        }
        string correlatedProperty(string property)
        {
            double maxCorrl = 0;
            double corrl;
            string toRet = "";
            foreach (string key in dict.Keys)
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
        // performs a linear regression and returns the line equation
        Func<double, double> linearReg(ArrayList points)
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
            double a = cov(x, y) / var(x);
            double b = avg(y) - a * avg(x);
            Func<double, double> func = x => a * x + b;
            /*            LineSeries line = new LineSeries();
                        for (int i = 0; i < size; i++)
                        {
                            double maor = (double)x[i];
                            double newY = a * i + b;
                            line.Points.Add(new DataPoint(i, newY));
                        }*/
            return func;
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
            currentLine = 0;
            string lastChoice = null;
            int lastLine = 0;

            new Thread(delegate ()
                {
                    int len = array.Count;
                    Boolean innerStopped=false;
                    //FunctionSeries fs = new();
                    while (!isStopped)
                    {
                        for (; currentLine < len; currentLine++)
                        {
                            string line = array[currentLine].ToString();
                            string[] data = line.Split(",");
                            SliderValue = getSliderValue();

                            Aileron = (double)dict["aileron"][currentLine];
                            Elevator = (double)dict["elevator"][currentLine];
                            Rudder = (double)dict["rudder"][currentLine];
                            Throttle = (double)dict["throttle"][currentLine];
                            Altitude = (double)dict["altitude-ft"][currentLine];
                            RollDeg = (double)dict["roll-deg"][currentLine];
                            PitchDeg = (double)dict["pitch-deg"][currentLine];
                            HeadingDeg = (double)dict["heading-deg"][currentLine];
                            YawDeg = (double)dict["side-slip-deg"][currentLine];
                            AirSpeed = (double)dict["airspeed-kt"][currentLine];
                            if (currerntChoice != null && currentLine % 10 == 0)
                            {
                                buildAlldGraphs(len, ref lastChoice, ref lastLine);
                            }
                            if (!isPaused && !isStopped)
                            {
                                telnetClient.write(line);
                                Thread.Sleep((int)(100.0 / SimulatorSpeed));
                            }
                            else if (isPaused)
                            {
                                while (isPaused)
                                {
                                    if (currerntChoice != null && (!currerntChoice.Equals(lastChoice) || !currentLine.Equals(lastLine)))
                                    {
                                        buildAlldGraphs(len, ref lastChoice, ref lastLine);
                                    }
                                }
                            }
                            else // if (isStopped)
                            {
                                innerStopped = true;
                                break;
                            }
                        }
                        if (innerStopped)
                            break;
                        if (currerntChoice != null && !currerntChoice.Equals(lastChoice))
                        {
                            buildAlldGraphs(len, ref lastChoice, ref lastLine);
                        }
                    }
                }).Start();
        }

        private void buildAlldGraphs(int len, ref string lastChoice, ref int lastLine)
        {
            // build current Graph
            correlatedChoice = correlatedProperty(currerntChoice);
            LineSeries l = new LineSeries();
            buildGraph(plotModelCurrent,l, currerntChoice);

            if (!correlatedChoice.Equals(""))
            {
                // build correlatedChoice
                LineSeries line = new LineSeries();
                buildGraph(plotModelCurrentCorrelation,line, correlatedChoice);
            
                // build linearReg
                ArrayList x = dict[currerntChoice];
                ArrayList y = dict[correlatedChoice];
                ArrayList points = new ArrayList();
                double maxX = 0, minX = 0;

                for (int i = 0; i < len; i++)
                {
                    double currentX = (double)x[i];
                    double currentY = (double)y[i];
                    maxX = Math.Max(maxX, currentX);
                    minX = Math.Min(minX, currentX);
                    DataPoint p = new DataPoint(currentX, currentY);
                    points.Add(p);
                }

                Func<double, double> func = linearReg(points);
                FunctionSeries fs = new FunctionSeries(func, minX, maxX, len, null);
                LineSeries lineOfLastSeconds = new();
                lineOfLastSeconds.LineStyle = LineStyle.None;
                lineOfLastSeconds.MarkerType = MarkerType.Circle;
                lineOfLastSeconds.MarkerSize = 2;
                lineOfLastSeconds.MarkerFill = OxyColors.Black;
                int start = Math.Max(0, currentLine - 300);
                int end = currentLine;
                for (int i = start; i < end; i++)
                {
                    lineOfLastSeconds.Points.Add(new DataPoint((double)dict[currerntChoice][i], (double)dict[correlatedChoice][i]));
                }
                plotModelRegression.Series.Clear();
                plotModelRegression.Series.Add(fs);
                plotModelRegression.Series.Add(lineOfLastSeconds);
                plotModelRegression.InvalidatePlot(true);
            }
            else
            {
                plotModelCurrentCorrelation.Series.Clear();
                plotModelRegression.Series.Clear();
                plotModelCurrentCorrelation.Title = "No correlated feature";

            }
            plotModelCurrentCorrelation.InvalidatePlot(true);
            plotModelRegression.InvalidatePlot(true);
            lastLine = currentLine;
            lastChoice = currerntChoice;
        }

        private void buildGraph(PlotModel plotModel,LineSeries l, string choice)
        {
            for (int i = 0; i <= currentLine - 10; i += 10)
            {
                l.Points.Add(new DataPoint(i, (double)dict[choice][i]));
            }
            plotModel.Series.Clear();
            plotModel.Series.Add(l);
            plotModel.Title = choice;
            plotModel.InvalidatePlot(true);
        }
        private void buildLine(int start, int end, LineSeries l, ArrayList px, ArrayList py, int step)
        {
            for (int i = start; i <= end; i += step)
            {
                l.Points.Add(new DataPoint((double)px[i], (double)py[i]));
            }
        }

        public void moveSlider(double value)
        {
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
