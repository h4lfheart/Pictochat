# Pictochat
A DSi Pictochat-Styled Chatroom created with C# and WPF.

<p>
    <img src="https://github.com/halfuwu/Pictochat/blob/master/.github/HealthAndSafety.png?raw=true" alt="PictochatLobby" width="216">
    <img src="https://github.com/halfuwu/Pictochat/blob/master/.github/Lobby.png?raw=true" alt="PictochatLobby" width="216">
    <img src="https://github.com/halfuwu/Pictochat/blob/master/.github/Chatroom.png?raw=true" alt="PictochatLobby" width="216">
</p>

## How It Works
The communication itself is entirely made up of UDP sockets. It uses a client-to-client structure so that anyone who is on the same network as you can use the chatrooms and each room has a dedicated port where all the relevant info goes through.

## Requirements
* [.NET 6.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime)
* [Python (Building Pictochat)](https://www.python.org/downloads)

## Build
 * Run the release.py file located in the root directory of this project
