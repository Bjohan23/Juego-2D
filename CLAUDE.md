# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a 2D platformer game built with Unity 2022.3.13f1. The project demonstrates classic side-scrolling platformer mechanics with Unity's 2D features, including tilemap integration, animated player movement, particle effects, and game management systems.

## Development Commands

### Unity Project Commands
- **Open Project**: Open the project folder in Unity Hub or directly in Unity 2022.3.13f1
- **Build**: Use Unity's Build Settings (File > Build Settings) to build for target platforms
- **Run**: Press Play button in Unity Editor to test the game
- **Scene Management**: Main scenes are located in `Assets/Scenes/` (Level.unity and Menu.unity)

### C# Development
- **Solution File**: `Juego-2D.sln` can be opened in Visual Studio or other C# IDEs
- **Scripts Location**: All game scripts are in `Assets/Scripts/`
- **Build Assembly**: Unity automatically compiles C# scripts when changes are detected

## Code Architecture

### Core Systems

**GameManager (Singleton)**
- `Assets/Scripts/GameManager.cs` - Central game state management
- Handles coin/gem counting, death/respawn system, level completion
- Manages UI updates and scene transitions
- Key methods: `Death()`, `LevelComplete()`, `IncrementCoinCount()`

**PlayerController**
- `Assets/Scripts/PlayerController.cs` - Main player movement and input
- Supports both PC (keyboard) and mobile touch controls via `Controls` enum
- Features: double-jump mechanics, grounded detection, sprite flipping, particle effects
- Integrates with Unity's Animator for character animations
- Mobile control methods: `MobileMove()`, `MobileJump()`, `MobileShoot()`

**UIManager (Singleton)**
- `Assets/Scripts/UIManager.cs` - UI state and screen transitions
- Handles mobile control visibility and screen fade effects
- Key methods: `EnableMobileControls()`, `DisableMobileControls()`

### Supporting Systems

**Pickup System**
- `Assets/Scripts/pickup.cs` - Collectible items (coins, gems, health)
- Uses trigger detection for player interaction
- Spawns particle effects on collection

**Level Management**
- `Assets/Scripts/ExitTrigger.cs` - Level completion triggers
- `Assets/Scripts/HealthManager.cs` - Player health system
- `Assets/Scripts/Events.cs` - Custom event system

### Asset Organization

**Visual Assets**
- Character sprites: `Assets/cat/` and `Assets/dog/` with animation frames
- Tilemap tiles: `Assets/Tilemap/` and `Assets/png/Tiles/`
- Environmental objects: `Assets/png/Object/` and `Assets/Objects/`
- UI elements and effects: `Assets/Prefabs/`

**Technical Assets**
- Physics materials: `Assets/NoFriction.physicsMaterial2D`
- Render pipeline: Universal Render Pipeline configuration in `Assets/Settings/`
- Animation controllers: Located in character folders (e.g., `Assets/dog/Animations/`)

## Unity-Specific Considerations

- Project uses Unity's 2D Tilemap system for level design
- Universal Render Pipeline (URP) for 2D rendering
- TextMeshPro for UI text rendering
- Physics2D with custom materials and layers
- Particle systems for visual effects (footsteps, impact, pickups)
- Animator-based character animation system

## Development Notes

- The project supports both PC and mobile input through the `Controls` enum
- Player respawning reloads the entire scene (Scene index 1)
- Ground detection uses raycasting from a `groundCheck` transform
- Mobile controls are dynamically enabled/disabled based on control mode
- Particle effects are carefully managed with emission rate controls
- The game uses singleton pattern for managers (GameManager, UIManager)

## Common Development Tasks

When modifying player mechanics, work with `PlayerController.cs` and ensure both PC and mobile input methods are updated. For UI changes, coordinate between `UIManager.cs` and `GameManager.cs`. Level design involves Unity's Tilemap tools and prefab placement in scenes.