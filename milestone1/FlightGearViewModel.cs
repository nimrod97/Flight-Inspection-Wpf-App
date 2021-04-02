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

        //public void VM_disconnect()
        //{
        //    model.disconnect();
        //}

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


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        
    }
}
