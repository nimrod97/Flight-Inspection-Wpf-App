# ADP2-Flight-Inspection-App

# <h1> This is an tag
  
This App has three main parts that run it, each part with its own designated responsibilities. The Model interacts with FlightGear via TCP connection, continuously send and read data request and notifies the relevant ViewModel when it's data changed. Next, the ViewModels process the changed data and notifies the Views about the changed data. the Views then react to the changed data accordingly, the data flows both from and to the Model. In the end, the aircraft inside FlightGear reacts to the joystick and handles being moved in our app.
