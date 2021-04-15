
# Flight Inspection App
A WPF application that shows data from real flight and allow the user to inspect the information by distinguish different parameters.</br>
This app made for flight researchers or pilots who want to view this information from certain flight.</br>
The flight data is basically a CSV file that includes feature as speed, altitude, direction, etc which should be loaded by the user.</br>
Our app will display the inforamtion from the beginning of the file till the end, just like a movie and also in a graphically display.</br></br>
<img src = "https://github.com/bartawil/milestone1/blob/master/Capture.PNG" width="650" height="400"></br>


## Features
* Open button - upload csv the flight data that we want to investigate.
* Play button - start the simulation.
* Media player - allows you to jump, stop, pause just like real media players.
* Flight slider - allows the user to skip to any time on the flight by scrolling controller as usually designd on media players.
* Speed slider - changes the speed of the simulator.
* Graphs - by choosing a flight feature from the list you can see its progress on a graph and also view the most correlative feature next to him </br>
* In addition a we will see the two corallated features regression line. 
* Joystick - displayes the movment of the Elevator and Aileron.
* Upload file button - upload csv file to learn how normal flight should be.
* Dll button - upload Dynamic-Link Library of anomaly detector algoritem.
* Anomaly detection graph - after uploading the dll files and choosing a specific feature a graph will display the anomalies that found in the flight. 
* Anomaly detection slider - you can see the current time step that anomaly occurred next to the Flight slider.

## Project files
* Main folder - includes the sln project file that openes the whole project</br>
              - the CSV's flight files</br>
              - palyback_small</br>
              - c++ dll files</br>
              - UML diagram</br>
              - README</br>
*  milestone1 folder - all project src files.
*  plugins folder - includes 2 dll (for circle/simple detect algoritems).

## Installation and Running
* Open the FlightGear API or download it right [here](https://www.flightgear.org). </br>
* On the setting add to the addional settings this commend: </br>
  --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small </br>
  --fdm=null. </br>
* Add the file [playback_small.xml](https://github.com/bartawil/milestone1/blob/master/playback_small.xml) to "C:\Program Files\FlightGear 2020.3.6\data\Protocol"
* Download the project by git clone
* Make sure you already have the Oxplot lib on your IDE.
* Compile with x86 Dll.
* Run and Upload 2 CSV file: </br>
  * normal flight file
  * test flight file 
* Dll: </br>
  In addition, if we want to use an anomaly detection algorithm we need to upload an anomaly detection DLL from the plagin folder. </br>
  After loading the files, we can press the play button to run the flight. </br>
  To detect a particular feature, we can click on it from the list on the left side of the screen. When we click on the data, we can see the following graphs:
  * The upper left graph - shows the data of the feature that we selected.
  * The upper right graph - shows (if there is one) the data of the most correlative feature to the feature we selected.
  * The graph in the center of the screen - shows the linear regression line of the feature we selected and the feature that is most correlative to it.  In addition to the   line, we can see the points of the last 30 second on which the line was built.
  - The graph that shows the data we received from the anomaly detection algorithm - The graph shows the anomalies points and mark them. We can see on the slider the times that these points have occurred.


## Implementation
This App based on MVVM software architectural pattern that has three main parts that run it, each part with its own designated responsibilities. </br>
The Model interacts with FlightGear via TCP connection, continuously send and read data request and notifies the relevant ViewModel when it's data changed. </br>
Next, the ViewModels process the changed data and notifies the Views about the changed data. </br>
The View then react to the changed data accordingly and shows them.

### [UML](https://github.com/bartawil/milestone1/blob/master/Untitled%20Diagram.jpg)
<img src="https://github.com/bartawil/milestone1/blob/master/Untitled%20Diagram.jpg" width="650" height="850">

### Create your own Dll 
* Implement your own anomalies algorithm.
* make sure your Dll return a graph.
* add the new dll path in the right place inside the app.
* choose some feature to detect and here you go! (:


## Collaborators
Developed by Nimrod Gabbay, Noa Miara, Maor Malekan and Bar Tawil.</br>


## Explanation video
[https://www.youtube.com/watch?v=nBGUOMIWBaI](https://www.youtube.com/watch?v=nBGUOMIWBaI)
