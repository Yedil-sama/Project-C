# Clash Royale-like 3D Deck-building Game

**Clash Royale-like** is a 3D deck-building strategy game inspired by **Clash Royale**. The game features unique **races** with different **gimmicks** and a variety of **cards** including **units, buildings**, and **spells**. Players build and manage their decks to outsmart opponents in fast-paced, real-time strategic battles.

---

## 🎮 Game Overview

- **Genre:** Strategy / Deck-building
- **Engine:** Unity
- **Perspective:** 3D Top-Down
- **Core Loop:** Build and upgrade your deck, deploy units, and use spells to defeat opponents in tactical, real-time combat.

---

## ⚡ Key Features

### 🃏 Deck-building System
- Build and customize your deck with a combination of **units**, **buildings**, and **spells**.
- Every card has unique **abilities** and **synergies**.
- Decks are balanced with resource management and strategy.

### 🏰 Races with Unique Gimmicks
- Different **races** (e.g., humans, elves, orcs) with their own strengths, weaknesses, and abilities.
- Racial traits affect **unit stats**, **building effectiveness**, and **card synergies**.

### 🧙‍♂️ Units, Buildings & Spells
- **Units** with AI-controlled behavior, leveraging **Unity's NavMesh** for movement.
- **Buildings** that provide various buffs, passive effects, or summon units.
- **Spells** to deal damage, heal, or alter the battlefield.

### 🌍 Real-time Combat
- Units are deployed on the battlefield and automatically move and fight based on AI behavior.
- **Real-time** combat with strategic pauses for deploying cards and casting spells.

### 🏆 PvP & AI Challenges
- **Player vs Player** (PvP) mode for real-time multiplayer matches.
- **AI challenges** with different difficulty levels and strategies.

---

## 🧠 Game Mechanics

- **Deck management:** Players can customize their decks before each match.
- **Arena battles:** Players face off in strategic matches in a 3D arena.
- **Resource management:** Players need to manage resources to summon units, build structures, and cast spells.
- **Card upgrades:** Cards can be upgraded during battles to increase their strength.

---

## 🛠 Architecture & Principles

- **SOLID Principles** to ensure clean, scalable, and maintainable code.
- **Modular ability system** that allows easy addition of new abilities and card types.
- **ScriptableObject-based design** for managing card data and other game configurations.
- **Strategy** Pattern used for Units' Behavior.
- **Factory** Pattern used for Units, Spells, and Buildings, all are Cards, spawning.
- **Pool** Pattern used for Projectiles.
- **Singleton** Pattern used for GameManager (only 24 lines of code).

---

## 📂 Project Structure

```
ClashRoyaleLike
├── Assets
│   ├── Artwork
│   ├── Cards
│   ├── Maps
│   ├── Materials
│   ├── Models
│   ├── Plugins
|   ├── Prefabs
|   ├── Resources
|   ├── Scenes
│   └── Scripts
|       ├── Core
|           ├── Cards
|               ├── Buildings
|               ├── Spells
|               └── Units
|                   ├── Behaviors
|                   ├── Passives
|                   └── Races
|           └── Stats
├── ProjectSettings
├── Packages
└── README.md
```

---

## 🚀 Getting Started

1. Clone the repository to your local machine.
2. Open the project in Unity (version 2022.3.46f1 LTS or later recommended).
3. Press Play in the main scene (`Scenes/Game Scene.unity`).
4. Use the screen/mouse to interact with the game.
5. Build your deck and start battling!

---

## 🔮 Roadmap

- [ ] Expand **card pool** with new units, buildings, and spells.
- [ ] Implement **multiplayer support** for real-time player battles.
- [ ] Add **AI-controlled enemy** with different difficulty levels and strategies.
- [ ] Improve **UI/UX** for deck-building and in-game navigation.
- [ ] Integrate **leveling and progression system** for cards and player ranks.

---

## 📝 License

Custom academic license — see [LICENSE](./LICENSE) for details.
---

## 📫 Contact

For questions or collaboration:
- Telegram: [@dodgecar69]
- Email: sultanedil3@gmail.com
[Join the community on Telegram](https://t.me/yedilstudio)
> “Powered by Unity, Driven by Passion.”

---

> “Build, summon, and conquer in the most tactical deck-building game with real-time strategy.”
