using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Series;

namespace milestone1
{
    interface IFlightGearModel: INotifyPropertyChanged
    {
        double Altitude { set; get; }
        double AirSpeed { set; get; }
        double HeadingDeg { set; get; }
        double PitchDeg { set; get; }
        double RollDeg { set; get; }
        double YawDeg { set; get; }
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        double Throttle { set; get; }

        double SliderValue { set; get; }
        double SimulatorSpeed { set; get; }

        string[] Properties { get; }

        void connect(string ip, int port);
        //void disconnect();
        void start(string path);
        void moveSlider(double value);
        void pause();
        void resume();
        void stop();
        void moveSimulatorSpeed(double value);
        void initializingComponentsByPath(string path);

        PlotModel PlotModelCurrent { get; set; }
        PlotModel PlotModelRegression { get; set; }
        PlotModel PlotModelCurrentCorrelation { get; set; }

        public string CurrerntChoice { get; set; }

/*        IList<DataPoint> PointsCurrentChoice { get; set; }
*/    }
}
