using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

namespace FlightSimulator
{
    class MyFlightGearModel : IFlightGearModel
    {
        ITelnetClient telnetClient;
        volatile Boolean isStopped;
        volatile Boolean isPaused;
        volatile Boolean isPlayed;
        private ArrayList array;

        public event PropertyChangedEventHandler PropertyChanged;

        
        public MyFlightGearModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.isStopped = false;
            this.isPaused = false;
        }
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
        }

        public void disconnect()
        {
            isStopped = true;
        }

        public void createLocalFile(string path)
        {
           
            this.array = new ArrayList();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        array.Add(currentLine);
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

        public void start(string path)
        {
            if (!isPlayed)
            {
                isPlayed = true;
                createLocalFile(path);
                new Thread(delegate ()
                {
                    foreach (string line in array)
                    {
                        if (!isPaused && !isStopped)
                        {
                            telnetClient.write(line);
                            Thread.Sleep(100);
                        }
                        else if (isPaused)
                        {
                            while (isPaused) { }
                        }
                        else if (isStopped)
                        {
                            telnetClient.disconnect();
                            break;
                        }
                    }
                    /*using (StreamReader sr = new StreamReader(path))
                    {
                        string currentLine;
                        // currentLine will be null when the StreamReader reaches the end of file
                        while ((currentLine = sr.ReadLine()) != null)
                        {
                            telnetClient.write(currentLine);
                            //byte[] messageSent = Encoding.ASCII.GetBytes(currentLine + "\r\n");

                            //int b = s.Send(messageSent);
                            Thread.Sleep(100);
                        }
                    
                    }*/

                }).Start();
            }
            else
            {

            }

        }
    }
}
