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
using System.Reflection;
using System.Windows.Controls;

namespace milestone1
{
    class MyFlightGearModel : IFlightGearModel
    {
        ITelnetClient telnetClient;
        volatile Boolean isStopped;
        volatile Boolean isPaused;
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
        private PlotModel plotModelForDll;
        private PlotModel plotModelAnomalies;
        private string currerntChoice;
        private string correlatedChoice;
        private string lastChoice;

        private ArrayList detectionFlight;
        private ArrayList properFlight;
        private Dictionary<string, ArrayList> detectDict;
        private Dictionary<string, ArrayList> properDict;

        private string[] properties;

        private Assembly assembly;
        private string detectFilePath;
        private string properFilePath;

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
            set { plotModelCurrent = value; }
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

        public PlotModel PlotModelForDll
        {
            get { return plotModelForDll; }
            set { plotModelForDll = value;
                NotifyPropertyChanged("PlotModelForDll");
            }
        }

        public PlotModel PlotModelAnomalies
        {
            get
            {
                return plotModelAnomalies;
            }
            set
            {
                plotModelAnomalies = value;
                NotifyPropertyChanged("PlotModelAnomalies");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = true;
            this.isPaused = false;
            this.simulatorspeed = 1.00;
            createProperties();
            SetUpGraphOfCurrent();
            SetUpGraphOfCorrelated();
            SetUpGraphOfRegression();
            this.currerntChoice = null;
            this.correlatedChoice = null;
            this.assembly = null;
        }

        private void SetUpGraphOfCurrent()
        {
            plotModelCurrent = new PlotModel();
            plotModelCurrent.TitleFontSize = 11;
            TimeSpanAxis timeAxis = new TimeSpanAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10, IsZoomEnabled = false };
            plotModelCurrent.Axes.Add(timeAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left, IsZoomEnabled = false };
            plotModelCurrent.Axes.Add(valueAxis);


        }

        private void SetUpGraphOfCorrelated()
        {
            plotModelCurrentCorrelation = new PlotModel();
            plotModelCurrentCorrelation.TitleFontSize = 11;
            TimeSpanAxis timeAxis = new TimeSpanAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10, IsZoomEnabled = false };
            plotModelCurrentCorrelation.Axes.Add(timeAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left, IsZoomEnabled = false };
            plotModelCurrentCorrelation.Axes.Add(valueAxis);


        }

        private void SetUpGraphOfRegression()
        {
            plotModelRegression = new PlotModel() { TitleFontSize = 11, Title = "Linear Regression" };
            LineSeries l = new LineSeries();
            plotModelRegression.Series.Add(l);
            LinearAxis xAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Bottom, TitleFontSize = 10, IsZoomEnabled = false };
            plotModelRegression.Axes.Add(xAxis);
            LinearAxis valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Position = AxisPosition.Left, IsZoomEnabled = false };
            plotModelRegression.Axes.Add(valueAxis);
        }


        private void initializeDictionary(ref Dictionary<string, ArrayList> dict, ArrayList flight)
        {
            dict = new Dictionary<string, ArrayList>();
            int len = flight.Count;
            for (int i = 0; i < properties.Length; i++)
            {
                ArrayList arr = new ArrayList();
                int j = 0;
                while (j < len)
                {
                    string line = flight[j].ToString();
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

        public void initializingComponentsByPath(string path, string mode)
        {
            if (mode.Equals("detect"))
            {
                createLocalFile(path, ref detectionFlight);
                initializeDictionary(ref detectDict, detectionFlight);
            }
            else if (mode.Equals("learn"))
            {
                createLocalFile(path, ref properFlight);
                initializeDictionary(ref properDict, properFlight);
            }
        }
        string correlatedProperty(string property)
        {
            double maxCorrl = 0;
            double corrl;
            string toRet = "";
            foreach (string key in properDict.Keys)
            {
                if (property.Equals(key))
                    continue;
                else
                {
                    corrl = Math.Abs(pearson(properDict[property], properDict[key]));
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

        public void createLocalFile(string path, ref ArrayList flight)
        {
            flight = new ArrayList();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        flight.Add(line);
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
            telnetClient.write(detectionFlight[detectionFlight.Count - 1].ToString());
            telnetClient.disconnect();
        }

        public void start()
        {
            currentLine = 0;
            lastChoice = null;
            int lastLine = 0;
            isStopped = false;

            new Thread(delegate ()
            {
                int len = detectionFlight.Count;
                Boolean innerStopped = false;
                while (!isStopped)
                {
                    for (; currentLine < len; currentLine++)
                    {
                        string line = detectionFlight[currentLine].ToString();
                        SliderValue = getSliderValue();

                        updateFlightData();

                        if (currerntChoice != null && currentLine % 10 == 0)
                        {
                            buildAllGraphs(len, ref lastChoice, ref lastLine);
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
                                if (currentLine != lastLine)
                                {
                                    updateFlightData();
                                    if (currerntChoice != null)
                                    {
                                        buildAllGraphs(len, ref lastChoice, ref lastLine);
                                    }
                                }
                                else if (currerntChoice != null && !currerntChoice.Equals(lastChoice))
                                {
                                    buildAllGraphs(len, ref lastChoice, ref lastLine);
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
                        buildAllGraphs(len, ref lastChoice, ref lastLine);
                    }
                }
            }).Start();
        }

        private void updateFlightData()
        {
            Aileron = (double)detectDict["aileron"][currentLine];
            Elevator = (double)detectDict["elevator"][currentLine];
            Rudder = (double)detectDict["rudder"][currentLine];
            Throttle = (double)detectDict["throttle"][currentLine];
            Altitude = (double)detectDict["altitude-ft"][currentLine];
            RollDeg = (double)detectDict["roll-deg"][currentLine];
            PitchDeg = (double)detectDict["pitch-deg"][currentLine];
            HeadingDeg = (double)detectDict["heading-deg"][currentLine];
            YawDeg = (double)detectDict["side-slip-deg"][currentLine];
            AirSpeed = (double)detectDict["airspeed-kt"][currentLine];
        }

        private void buildAllGraphs(int len, ref string lastChoice, ref int lastLine)
        {
            if (!currerntChoice.Equals(lastChoice))
                correlatedChoice = correlatedProperty(currerntChoice);
            // build current Graph
            LineSeries l = new LineSeries();
            buildOneGraph(plotModelCurrent, l, currerntChoice);

            if (!correlatedChoice.Equals(""))
            {
                // build correlatedChoice
                LineSeries line = new LineSeries();
                buildOneGraph(plotModelCurrentCorrelation, line, correlatedChoice);

                // build linearReg
                buildLinearRegressionGraph(len);
            }
            else
            {
                plotModelCurrentCorrelation.Series.Clear();
                plotModelRegression.Series.Clear();
                plotModelCurrentCorrelation.Title = "No correlated feature";
                plotModelCurrentCorrelation.InvalidatePlot(true);
                plotModelRegression.InvalidatePlot(true);
            }
            lastLine = currentLine;
            lastChoice = currerntChoice;
        }

        private void buildOneGraph(PlotModel plotModel, LineSeries l, string choice)
        {
            for (int i = 0; i <= currentLine - 10; i += 10)
            {
                l.Points.Add(new DataPoint(i, (double)detectDict[choice][i]));
            }
            plotModel.Series.Clear();
            plotModel.Series.Add(l);
            plotModel.Title = choice;
            plotModel.InvalidatePlot(true);
        }

        private void buildLinearRegressionGraph(int len)
        {
            FunctionSeries fs = buildLinearRegressionLine(len);
            LineSeries lineOfLastSeconds = buildLineforLastSeconds();
            plotModelRegression.Series.Clear();
            plotModelRegression.Series.Add(fs);
            plotModelRegression.Series.Add(lineOfLastSeconds);
            plotModelRegression.InvalidatePlot(true);
        }

        private FunctionSeries buildLinearRegressionLine(int len)
        {
            // build linearReg
            ArrayList x = detectDict[currerntChoice];
            ArrayList y = detectDict[correlatedChoice];
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
            return new FunctionSeries(func, minX, maxX, len, null);
        }

        private LineSeries buildLineforLastSeconds()
        {
            LineSeries lineOfLastSeconds = new();
            lineOfLastSeconds.LineStyle = LineStyle.None;
            lineOfLastSeconds.MarkerType = MarkerType.Circle;
            lineOfLastSeconds.MarkerSize = 2;
            lineOfLastSeconds.MarkerFill = OxyColors.Black;
            int start = Math.Max(0, currentLine - 300);
            int end = currentLine;
            for (int i = start; i < end; i++)
            {
                lineOfLastSeconds.Points.Add(new DataPoint((double)detectDict[currerntChoice][i], (double)detectDict[correlatedChoice][i]));
            }
            return lineOfLastSeconds;
        }

        public void moveSlider(double value)
        {
            if (!isStopped)
            {
                currentLine = Convert.ToInt32((value / 100.0) * detectionFlight.Count);
                SliderValue = value;
                if (isPaused && currentLine < detectionFlight.Count)
                {
                    telnetClient.write(detectionFlight[currentLine].ToString());
                }
            }
        }

        public double getSliderValue()
        {
            return Convert.ToDouble((currentLine * 100) / detectionFlight.Count);
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

        public void initAssembly(Assembly assembly, string detectFilePath, string properFilePath)
        {
            this.assembly = assembly;
            this.detectFilePath = detectFilePath;
            this.properFilePath = properFilePath;
            executeDll();
        }
        public void executeDll()
        {
            new Thread(delegate ()
            {
                string name = assembly.FullName.Split(",")[0];
                var type = assembly.GetType(name + ".model");
                var obj = Activator.CreateInstance(type);
                var method = type.GetMethod("execute");
                PlotModelForDll = (PlotModel)method.Invoke(obj, new object[] { (string)detectFilePath, (string)properFilePath, detectDict, CurrerntChoice });
                var method2 = type.GetMethod("createAnomalyPoints");
                PlotModelAnomalies = (PlotModel)method2.Invoke(obj, new object[] { });
                PlotModelForDll.InvalidatePlot(true);
                PlotModelAnomalies.InvalidatePlot(true);
            }).Start();
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
