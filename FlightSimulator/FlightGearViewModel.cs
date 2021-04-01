using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimulator
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel model;

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
        
        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
