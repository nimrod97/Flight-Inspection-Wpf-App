using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace milestone1
{
    interface IFlightGearModel: INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start(string path);
        void moveSlider(double value);
        void pause();
        void resume();
    }
}
