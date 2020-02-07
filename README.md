# RockPaperSpell

A PC port of the board game **Rock Paper Wizard** by **Wizard of the Coast**.

## MVC Pattern

For the multiplayer connectivity there is an additional component called Network that acts as an interface in between the View and the Controller for receiving input from the players and to send the data to the players' views.

**Model** <- **Controller** <-> Network* <-> **View**

Arrows indicate interaction.

- Model component is self contained. It stores the current state of the match.
- Controller component is the core. 
  * It listens to the input from the View. 
  * Makes changes on the Model.
  * Updates the View.
- View component is the UI elements that show the status of the match to the players.
- Network is an optional component required for multiplayer matches. It mimics the behaviour of both the View and the Controller so that it is transparent for both. Handles the communication between the server and clients.
  * It acts as a View component from the Controller.
  * It acts as a Controller component from the View.

This allows to test the game logic and the multiplayer connectivity independently. 
OfflineScene uses the standard MVC pattern.
OnlineScene adds the Network component and the other components are not affected.

## Roadmap

- LAN connectivity (**Done**)
- Steam connectivity for PC
- Bluetooth connectivity for Android

## Dependencies

Mirror by vis2k https://github.com/vis2k/Mirror/releases/tag/v8.2.0
