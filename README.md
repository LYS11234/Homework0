# Homework0
 
Project: Vampire Survivors-Like Game (Homework0)
Overview
This is a game of Vampire Survivors-Like Game.
It is a project for study 'Observer Pattern'

Tech Stack
Engine: Unity
Language: C#
Key Technologies Used:
Observer Pattern
ScriptableObject (for Data Management)
Addressable Asset System (for Asynchronous Loading)
Object Pooling (Conceptual Implementation)
Unity UI System
Virtual Joystick (FloatingJoystick)
Project Structure (Key Scripts)
GameManager.cs: Core manager responsible for the overall game flow, enemy spawning, UI management, and acting as an observer.
PlayerManager.cs: Handles player movement, HP, and weapon control.
Enemy.cs: Manages enemy AI, spawning, HP, and death.
Database.cs (ScriptableObject): Stores and manages key game data and settings.
SpinningWeapons.cs: Controls the logic for the player's rotating weapons.
BoxManager.cs: Handles logic related to item boxes.
InfiniteTilemap.cs: Manages the infinitely scrolling background.
IObserver.cs, ISubject.cs: Interface definitions for the Observer Pattern.
Observer.cs, Subject.cs: (If used, base or utility classes for Observer/Subject)