using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FlightSimulator
{
    interface IFlightGearModel: INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start(string path);
    }
}
