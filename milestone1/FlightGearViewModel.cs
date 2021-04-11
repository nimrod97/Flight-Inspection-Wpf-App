using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace milestone1
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel model;
        public PlotModel VM_PlotModelCurrent
        {
            get { return model.PlotModelCurrent; }
            set { model.PlotModelCurrent = value; NotifyPropertyChanged("PlotModel"); }
        }
        public PlotModel VM_PlotModelRegression
        {
            get { return model.PlotModelRegression; }
            set { model.PlotModelRegression = value; NotifyPropertyChanged("PlotModel"); }
        }
        public PlotModel VM_PlotModelCurrentCorrelation
        {
            get { return model.PlotModelCurrentCorrelation; }
            set { model.PlotModelCurrentCorrelation = value; NotifyPropertyChanged("PlotModel"); }
        }


        public double VM_SliderValue
        {
            get
            {
                return model.SliderValue;
            }
            set
            {
                model.moveSlider(value);
            }
        }

        public double VM_SimulatorSpeed
        {
            get
            {
                return model.SimulatorSpeed;
            }
            set
            {
                model.moveSimulatorSpeed(value);
            }
        }

        public FlightGearViewModel(IFlightGearModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
/*
        public IList<DataPoint> VM_PointsCurrentChoice
        {
            get
            {
                return this.model.PointsCurrentChoice;
            }
            set
            {

            }
        }*/

        public void VM_connect(string ip, int port)
        {
            model.connect(ip, port);
        }


        public void VM_start(string path)
        {
            model.start(path);
        }

        public void VM_pause()
        {
            model.pause();
        }

        public void VM_resume()
        {
            model.resume();
        }

        public void VM_stop()
        {
            VM_goToEnd();
            model.stop();
        }
        
        public void VM_goRight()
        {
            if (VM_SliderValue < 96)
                VM_SliderValue += 4;
            else
                VM_SliderValue = 99.5;
        }

        public void VM_goLeft()
        {
            if (VM_SliderValue > 4)
                VM_SliderValue -= 4;
            else
                VM_SliderValue = 0;
        }

        public void VM_goToStart()
        {
            VM_SliderValue = 0;
        }
        public void VM_goToEnd()
        {
            VM_SliderValue = 99.5;
        }

        public float VM_Altitude
        {
            get { return model.Altitude; }
            set {; }
        }

        public float VM_AirSpeed
        {
            get { return model.AirSpeed; }
            set {; }

        }

        public float VM_HeadingDeg
        {
            get { return model.HeadingDeg; }
            set {; }

        }

        public float VM_PitchDeg
        {
            get { return model.PitchDeg; }
            set {; }

        }

        public float VM_RollDeg
        {
            get { return model.RollDeg; }
            set {; }

        }

        public float VM_YawDeg
        {
            get { return model.YawDeg; }
            set {; }

        }
        public float VM_Aileron
        {
            get { return model.Aileron; }
            set {; }
        }
        public float VM_Elevator
        {
            get { return model.Elevator; }
            set {; }
        }
        public float VM_Rudder
        {
            get { return model.Rudder; }
            set {; }
        }
        public float VM_Throttle
        {
            get { return model.Throttle; }
            set {; }
        }

        public string[] VM_Properties
        {
            get
            {
                return model.Properties;
            }
        }

        public string VM_CurrerntChoice
        {
            get
            {
                return model.CurrerntChoice;
            }
            set
            {
                model.CurrerntChoice = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }        
    }
}
