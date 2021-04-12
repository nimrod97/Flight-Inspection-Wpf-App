# Flight Inspection App
A WPF application that Shows data from real flight and allow the user to inspect the information by distinguish different parameters.</br>
This app made for flight researchers or pilots who want to view this information from certain flight.</br>
The flight data is basically a CSV file that includes feature as speed, altitude, direction, etc which should be loaded by the user.</br>
Our app will display the inforamtion from the beginning of the file till the end, just like a movie and also in a graphically display.</br></br>
<img src = "https://github.com/bartawil/check/blob/main/Capture.PNG" width="650" height="350"></br>

## Installation

* [Download](https://www.flightgear.org) FlightGear Simulator.


### Running 


#### Features

* Csv :
* Dll : 
* Graphs: 
* Joystick:
* Media-Player:
  ....


#### Implementing new dll 

- Customize your dll file...


### UML

<img src="https://github.com/bartawil/check/blob/main/Capture3.PNG" width="650" height="300">

### About the implementation process
MVVM.....
(Maor)
This App has three main parts that run it, each part with its own designated responsibilities. The Model interacts with FlightGear via TCP connection, continuously send and read data request and notifies the relevant ViewModel when it's data changed. Next, the ViewModels process the changed data and notifies the Views about the changed data. the Views then react to the changed data accordingly, the data flows both from and to the Model. In the end, the aircraft inside FlightGear reacts to the joystick and handles being moved in our app.
