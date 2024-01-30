# Cagan_Final
# Bomb Escape Game

# Welcome to the Bomb Escape Game, a text-based adventure game created in C#.
# Will you be able to escape the room before the bomb explodes? Be careful, you only have 10 minutes!


# Introduction
You find yourself trapped in a mysterious building with a bomb counting down. Your goal is to navigate through different rooms, solve puzzles, and interact with characters to find clues, keys, and items that will help you escape before the bomb explodes.

# Getting Started
To play the game, follow these steps:
Open the project in Visual Studio or your preferred C# IDE.
Build and run the Game.cs file to start the game.

# Gameplay
# Controls
W/A/S/D: Walking buttons in the lobby.
F: Interact with the environment or characters.
R: Start a fight with the enemy.
G: Leave the current room.
E: Save the game and exit.
Q: Quit the game.

# Rooms and Items
Lobby: The starting room and central hub.
Room 1: Locked room; find the Room 1 Key to unlock.
Room 2: Locked room; defeat the enemy to get the Room 2 Key.
NPC Room: Room with an NPC; answer the riddle correctly to win.
Item Search Room: Search for items like swords, shields, potions, keys, and maps.

# Enemy
Name: Enemy
Health: 50
Attack Power: 25
Drop Item: Room 2 Key
Winning the Game
Solve puzzles, find keys, defeat enemies, and successfully navigate through the rooms to escape before the bomb explodes.

# Information about the content of the game


# Saving and Loading
E: Save the game and exit. The game state is saved in game_save.txt.
If you restart the game, you can use the LoadGame method to resume from your last save.

When you first enter the game, it is indicated that you approach the lobby and are asked whether you want to enter.

As soon as you enter the lobby, the bomb in the game becomes active, and the countdown begins.

While in the game, every time you return to the lobby, you can see how much time is left for the bomb to explode.

When you want to enter the NPC room, you will be asked a question about whether you are sure, as giving the wrong answer can be fatally dangerous. If you decide to enter the room, you must interact with the NPC by pressing the F key to learn the puzzle you need to solve to escape from the room. Then, by entering the number next to the answer you believe is correct and pressing the enter key, the NPC will see your answer and confirm its correctness. Be careful, as giving the wrong answer twice will result in your death without the bomb exploding!

With the inventory system in the game, you can see the items you have.

When entering rooms where items are located, you should press the F key to interact and be able to see and interact with the items. This way, you can examine the items. To collect the desired item, simply enter the number next to the item as a command and press the enter key. The item will then appear in your inventory.

No matter where you are, pressing the G key will return you to the lobby.

To save the game and return to the beginning of the game, simply press the E key.

You can easily close the game at any time by pressing the Q key.

To challenge a warrior and engage in combat at any time, press the R key.

You can move using the W-A-S-D keys and navigate towards rooms with the desired items.

When you first start the game, you will notice that some rooms are locked. To unlock these rooms, you must find the keys hidden in certain places in the game.

To escape from the room before the bomb explodes

Detailed Information about the Content:
The key to Room 1 is found in the item search room.

To win the key to Room 2, you must press the R key to fight with the enemy and win. After winning, you will have the key that unlocks Room 2.

To find the answer to the NPC's question, you should look at the items in the rooms, and you will see that the weirdest item in these rooms is the Batman costume. Take this and talk to the NPC again, and the correct answer will appear. Choose the correct answer, and congratulations, you won!


# Creator
This game was created by [Çağan Erdem Üstündağ] as a fun programming project. Feel free to contribute, report issues, or provide feedback.
