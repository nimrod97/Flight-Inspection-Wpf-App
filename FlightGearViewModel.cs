using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace milestone1
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel model;
        private double sliderValue;
     

        public double VM_Slider
        {
            get
            {
                return sliderValue;
            }
            set
            {
                sliderValue = value;
                model.moveSlider(sliderValue);
            }
        }

        public FlightGearViewModel(IFlightGearModel model)
        {
            this.model = model;
            //model.PropertyChanged
        }

        public void VM_connect(string ip, int port)
        {
            model.connect(ip, port);
        }

        public void VM_disconnect()
        {
            model.disconnect();
        }

        public void VM_start(string path)
        {
            model.start(path);
        }

        public void VM_pause()
        {
            model.pause();
        }

        public void VM_play()
        {
            model.resume();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
