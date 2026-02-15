# CityBuilder
**Scalable Game Architecture with Persistent State Management**

A production-ready city building game demonstrating enterprise-grade architecture patterns for persistent game state, resource management, and extensible building systems. Built with patterns proven across 10+ years developing strategy titles at Ubisoft and other studios.

## 🎥 Demo
[![Watch Demo](http://img.youtube.com/vi/LrCwR09Xdm0/0.jpg)](http://www.youtube.com/watch?v=LrCwR09Xdm0 "City Builder Demo")

## 🎮 Core Features

**Resource Management** – Automated TownHall production with manual collection for secondary buildings  
**Dynamic Building System** – Expandable building types via static data configuration  
**State Persistence** – Robust game state serialization and recovery  
**Interactive Modes** – Build and move modes with intuitive placement system

## 🏗️ Technical Architecture

**Production-Grade Patterns:**
- **MVP (Model-View-Presenter)** – Enforced separation of concerns for maintainable codebase
- **Dependency Injection (Zenject)** – Loosely coupled components for scalability
- **Reactive Programming (UniRx)** – Efficient event-driven state updates
- **State Machine** – Clean mode transitions (Build/Move/Regular states)
- **Generic Object Pooling** – Optimized memory allocation for mobile performance
- **Promises (C#)** – Non-blocking asynchronous operations

**Data Architecture:**
- Metadata-driven building system via ScriptableObjects
- Persistent game state with JSON serialization
- Separation of static data and runtime state for clean save/load

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── city/
│   │   ├── view/          # Scene-specific UI and presentation
│   │   ├── model/         # Data, RemoteData, and Models
│   │   └── command/       # Game actions and operations
│   └── core/              # Shared generic abstractions (submodule)
├── Resources/             # Scenes and prefabs
└── StreamingAssets/       # Persistent state (GameState.json)
```

## 🔧 Tech Stack

| Technology | Purpose |
|-----------|---------|
| **Unity 2019.4.0f1** | Game engine |
| **Zenject** | Dependency injection framework |
| **UniRx** | Reactive extensions for Unity |
| **C# Promises** | Asynchronous operation handling |

## 🚀 Quick Start

1. Clone the repository
2. Open with Unity 2019.4.0f1+
3. Enable **Always Start from Startup Scene** in `/MainMenu/Game/`
4. Press Play

*Note: Delete `StreamingAssets/GameState.json` and `MetaData.json` to reset default settings after modifying ScriptableObjects.*

## 💼 About the Developer

Senior Game Developer with 10+ years specializing in strategy game architecture and multiplayer systems. Experience includes:
- **Ubisoft** – Clash of Beasts (war strategy), Captain LaserHawk (multiplayer arena)
- Persistent state systems supporting millions of players
- 30-40% performance optimizations in production environments
- Custom editor tools and data-driven design workflows

## 📫 Connect
Interested in scalable architecture for strategy games or persistent state systems? Let's discuss.

[LinkedIn](https://linkedin.com/in/mohsansaleem) | [Portfolio](https://github.com/mohsansaleem)

---

## Topics
`unity3d` `mvc` `zenject` `unirx` `reactive-programming` `promises` `city-builder` `gamestate` `pooling` `generics`
