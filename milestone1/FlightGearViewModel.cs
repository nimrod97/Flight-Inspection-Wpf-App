using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace milestone1
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel model;
        //private double sliderValue;
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

        public FlightGearViewModel(IFlightGearModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
            
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        
    }
}
