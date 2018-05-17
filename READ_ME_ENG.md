This program on the .NET platform implements the Sea Battle game in the user-computer mode, and in the network mode the user-user mode.
The basic rules of the classic version of the game: the first player "A", places the ships on a square playing field of n cells. 
The ships are divided into classes: single-celled, two-celled, three-celled and four-celled. Player "B" in his field puts his ships. 
Ships should not touch each other. The game consists in the fact that the players take turns calling the coordinates of the cells in which, as they assume, the enemy ships are located, that is, as if they are making a shot at the selected cell. 
A player is hit or missed after a shot. The game continues until one of the players will not be destroyed all the ships.
In order to more convenient design, customization, and separation of individual executable operations, the game "Sea Battle" is built on modules, which in turn are built on classes interacting with each other. 
The use of modules and classes provides an improved understanding of the code, and thus setting and subsequent modification of the source code of the program. 
This game consists of the following modules:
1) Program - the main entry point into the game application.
2) Form1 - the main form of the game application, the whole game takes place in this form.
3) Chatter - module for providing game chat, help log and info line.
4) Connection - a form of a network game application for creating a client-server connection.
5) Server - a module designed for PC as a server in a network game.
6) Client - a module designed for PC as a client in a network game.
7) Settings - the module is a stub for maintaining the work of the Settings.Designers module necessary for synchronizing a network game.
8) AssemblyInfo - module of parameters of assembly and identification of the type library of COM components.
9) Resources.Designer - created automatically by the StronglyTypedResourceBuilder class
using Visual Studio.
10) Settings.Designers - created automatically by the StronglyTypedResourceBuilder class using Visual Studio, contains synchronization functions for a network game.

The Program module is the main entry point into the game application and launches the main form of the Form1 application as well as the helper configuration functions Resources.
Designer and Settings.Designers.
The main form module Form1 contains the following main classes:
1) static class ProgramDetect - the main buttons on the forms that launch other modules, triggering the cheat written in the chat;
2) public partial class Form1: Form - the class of the application form in which the “Battleship” game takes place. This class contains the following main functions:
1) private void MakeAllUserShipsList - fixing the addition and destruction of ships, which is accompanied by a stroke of the ship;
2) private void ControlEnablerUser - check whether all ships are rastavleny;
3) private void ControlEnablerRandom - ships will be randomly or manually checked;
4) private void Form1_Load - check whether all ships are placed according to the rules vertically or horizontally, also tips on how to start the game;
5) private void ShipAdd - Verification by the rules of ships vertically and horizontally, including random arrangement, counting the number and deck of ships;
6) public void ReadyToGame - after the location of all ships, the ability to activate the start button of the game;
7) private void updateStatusLabels - memorization and highlighting of all destroyed ships;
8) private void ClearAllShips - clearing the playing field when a new game starts;
9) public void TotalClearForNewLan - initial form settings when starting a game over the network with another player;
10) private void new IGRA ComputerToolStripMenuItem_Click - dialog boxes and a hint to the player on the placement of ships at the beginning of the game with AI;
11) private void chkBoxUser_CheckedChanged - select the method for locating ships manually or randomly;
12) private void RandomShipsAdd - randomly arranged ships;
13) private void LabelInvoke - a constantion of facts about the course;
14) private void PrBarAnimation - accompaniment of shots with characteristic sounds when the sound is on;
15) private void dataGridView2_CellClick - the implementation of the shot when you click on a cell of the playing field;
16) public void CellUpdate - updating data on the playing field;
17) private Point SmartComputer - output whether the ship was hit or destroyed or not;
18) private void AddQueue - shooting at a player by angry rebel AI, who is trying to find the largest ship;
19) private void ShipsObvodkaAndChek - killing off AI of a wounded player’s player; when a ship is destroyed, activation of the stroke;
20) private void ComputerShootsEvent - tracking whose course and stanzaatsiya facts going in that turn;
21) private void PlayerShoot - help for the player, if he makes a mistake in the game, also checking for a hit or a slip by the player and a statement of fact;
22) private void ToWin - the fact that a player or AI wins when all the enemy ships are destroyed, if the enemy ship is wounded, another turn is given to the player or the AI;
23) private void newPlayNameToolStripMenuItem_Click - call the Connection form when starting a network game;
24) private void ChatWriteMessage - chat support in a network game in which you can activate a cheat to win;
25) private void textBox1_KeyPress - a player can enter his nickname to play over the network;
26) public void LanForm - synchronization of moves in a network game, the prohibition of firing before synchronization;
27) public void SendShootAnswer - constantion of the fact of hitting the ship in a network game;
28) public void WriteGM - confirmation of the readiness of the game from the players;
29) private void timer2_Tick - allowable waiting time for synchronization during a network game;
30) private void LanChekWin - announcement of the victory to one of the players;
31) public void DisableAll - game termination if synchronization timeout is exceeded or if some player has stopped the game;
32) private void developerToolStripMenuItem_Click - information.
The Chatter module is launched by the main Form1 module, for functions 1 and 2, and the form module for the network game Connection, for functions 3 and 4, contains the following main functions:
1) public static Chatter SelfRef - reads information about what is happening, to show it to the player;
2) public void UpdateStatusLbl1 - input in the line of information whose turn is now;
3) private void GameWriteNotificationFunc - sends an ip message to the chat during a network game;
4) public void GameWriteNotification - chat synchronization in a network game.
The network game form module Connection is started by the main Form1 module when the network mode is selected and contains the following main classes:
1) static class Players - produces output of the entered names of players playing in the current time until the end of the game;
2) static class OkChek - connects to the Client and Server modules when assigning PC roles, respectively;
3) public partial class Connection: Form - a form of a network game application for creating a client-server connection. This class contains the following main functions:
1) public void LabelUpdate - recording the ip address of the PC as a server or connecting to the server using the recorded address;
2) public Connection - definition of a PC in the role of a client or server using information in a form;
3) private void PrepareForm - checking everything recorded on the form for compliance with the minimum filling requirements;
4) private void timer1_Tick - a timer for each turn of players in a network game;
5) private void textBox_name_KeyDown - copy-paste mode is supported using hotkeys.

The Server module is launched by the network game module Connection for PC as a server and contains the following main class:
1) public class Server - the main class combining server functions for an application. This class contains the following main functions:
1) public bool ServerOk - confirms the property of the server to create the possibility of calls;
2) public void ServerStart - creating a server for connecting to it a client via ip via a socket port or displaying an error message;
3) public void Listen - connection of a third-party client via ip to the created server;
4) private Point GetPoint - transferring the values ​​of cell shots on the network during the course;
5) public void ReceiveData - check and maintain the connection, in case of error notification;
6) public void SendData - transmission and recording of data about shots per turn;
7) public void CloseConnection - if the connection is lost, or the intentional disconnection of the connection by the server or the client, the game is terminated.
The Client module is launched by the Connection form game for the PC as a client and contains the following main classes:
1) public class Client - the main class combining client functions for an application. This class contains the following main functions:
1) public bool ClientOk - confirms the property of the client to create the ability to access the server;
2) private void RunMe - timeout for authentication with the server;
3) private void OnTimedEvent - testing connection to the server;
4) public void SendMessage - receiving and recording data on shots per turn;
5) private Point GetPoint - transferring the values ​​of cell shots on the network during the course;
6) public void ReceiveMessage - check and maintain the connection, in case of error notification;
7) public void CloseConnection - if the connection is lost or the connection with the server is intentionally disconnected, the game is terminated.
The Settings module is launched by the Settings.Designers module for synchronization during a network game. The main class internal sealed partial class Settings - allows you to save and change the values ​​of parameters.
The Settings.Designers module contains the following main functions not automatically created by the StronglyTypedResourceBuilder class:
1) private static Settings defaultInstance - synchronization during a network game;
2) public bool Sound - on / off sound in the settings.
Instructions for playing "Sea Battle"
There are 2 modes available for playing sea battle: a game with a computer and an online game with another player, where the first player will be in the role of server and the second in the role of client.
At the beginning of the game, if you play online with another player:
1) this item is only for the player who is in the role of "Server". You need to enter the server name and select the "Server" option, then say your IP address to another player who is in the "Client" role to connect to you. After you need to click on the "Connect" button;
2) this item is only for the player who is in the role of "Client". You need to enter the name of the already created server, which must fully coincide with the name of the server of the player with whom you want to play sea battle. 
After selecting the option "Client", then enter the IP address of the player whose server name has already been entered in order to connect to it in the role of "Client". The last step is to click on the "Connect" button.
In all other respects, the game "Battleship" in the online game mode against another player - completely repeats the mode of the sea battle against the computer. 
At the beginning of the game, if you play with a computer, then an invitation to the placement of ships is immediately displayed, where the placement of ships can be done manually or by accident. 
If a player has performed an unacceptable action, for example, he tried to "put" the ship on the ship, then he will receive a warning message about his error. 
For the convenience of the game, a chat window has been added for players on the network, which every turn displays a message about the occurrence of an event per turn, a log window with tips to the player and a line with express information
If the placement of the ships is over, the player presses the “I am ready!” Button, which becomes available only after all ships have been placed, if the ships are placed manually. Further the invitation to the beginning of the game is displayed.