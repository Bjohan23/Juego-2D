# 🎮 Juego 2D Platformer - Proyecto Final de Computación Gráfica

## 📋 Descripción del Proyecto

Este proyecto es un juego de plataformas 2D desarrollado en Unity como proyecto final para la materia de Computación Gráfica. El juego implementa mecánicas clásicas de plataformas con gráficos 2D modernos, sistema de recolección de objetos, efectos de partículas y controles tanto para PC como para dispositivos móviles.

## 🎯 Objetivos del Proyecto

- **Objetivo Principal**: Crear un juego 2D funcional que demuestre el dominio de conceptos de computación gráfica
- **Objetivos Específicos**:
  - Implementar un sistema de movimiento fluido para el personaje
  - Desarrollar mecánicas de gameplay tradicionales (salto, doble salto, recolección)
  - Integrar efectos visuales y de partículas
  - Crear un sistema de gestión de niveles y UI
  - Optimizar el rendimiento para diferentes plataformas

## 🛠 Tecnologías Utilizadas

### Motor de Juego
- **Unity 2022.3.13f1** - Motor principal de desarrollo
- **Universal Render Pipeline (URP)** - Pipeline de renderizado 2D optimizado
- **C#** - Lenguaje de programación para la lógica del juego

### Sistemas Gráficos Implementados
- **Tilemap System** - Sistema de tiles para construcción de niveles
- **Sprite Animation** - Animaciones de personajes por frames
- **Particle Systems** - Efectos visuales (pisadas, impactos, recolección)
- **UI Canvas** - Interfaz de usuario responsiva
- **2D Physics** - Sistema de física para colisiones y movimiento

## 🎮 Características del Juego

### Mecánicas de Gameplay
- **Movimiento del Personaje**: Control fluido con aceleración y desaceleración
- **Sistema de Salto**: Salto simple y doble salto con detección de suelo
- **Recolección de Objetos**: Sistema de monedas y gemas con efectos visuales
- **Sistema de Muerte y Respawn**: Mecánica de zonas mortales y reaparición
- **Detección de Final de Nivel**: Triggers para completar niveles

### Controles Multiplataforma
- **PC**: Controles de teclado (WASD/Flechas + Espacio)
- **Móvil**: Controles táctiles con botones virtuales
- **Cambio Dinámico**: El sistema detecta automáticamente el tipo de control

### Efectos Visuales
- **Partículas de Pisadas**: Efecto visual al correr
- **Efectos de Impacto**: Partículas al aterrizar después de saltar
- **Efectos de Recolección**: Animaciones al recoger objetos
- **Transiciones de Pantalla**: Fade in/out entre estados del juego

## 🏗 Arquitectura del Código

### Patrones de Diseño Implementados
```
GameManager (Singleton)
├── Gestión de Estado del Juego
├── Sistema de Puntuación
└── Control de Niveles

PlayerController
├── Sistema de Input Multiplataforma
├── Física de Movimiento
└── Integración con Animaciones

UIManager (Singleton)
├── Control de Interfaz
├── Transiciones de Pantalla
└── Controles Móviles
```

### Scripts Principales
- **`GameManager.cs`** - Gestión central del juego y estados
- **`PlayerController.cs`** - Control del personaje y mecánicas de movimiento
- **`UIManager.cs`** - Manejo de la interfaz de usuario
- **`pickup.cs`** - Sistema de objetos recolectables
- **`ExitTrigger.cs`** - Detección de salida de nivel

## 📁 Estructura del Proyecto

```
Assets/
├── Scenes/
│   ├── Menu.unity          # Escena del menú principal
│   └── Level.unity         # Escena del nivel de juego
├── Scripts/
│   ├── GameManager.cs      # Lógica principal del juego
│   ├── PlayerController.cs # Control del jugador
│   ├── UIManager.cs        # Gestión de UI
│   └── pickup.cs          # Sistema de recolección
├── Prefabs/               # Objetos prefabricados
├── Tilemap/              # Tiles para construcción de niveles
├── cat/ & dog/           # Sprites de personajes
└── png/                  # Recursos gráficos
```

## 🎨 Recursos Gráficos

### Sprites y Animaciones
- **Personajes**: Sprites de gato y perro con múltiples estados de animación
  - Idle (reposo)
  - Run (correr)
  - Jump (saltar)
  - Fall (caer)
  - Dead (muerte)
  - Hurt (daño)

### Entorno
- **Tileset Completo**: 18 tiles diferentes para construcción de niveles
- **Objetos Ambientales**: Árboles, señales, iglús, cajas
- **Elementos Coleccionables**: Monedas, gemas, corazones

## 🔧 Configuración del Proyecto

### Requisitos del Sistema
- Unity 2022.3.13f1 o superior
- Visual Studio o VS Code para edición de código
- 2GB de espacio libre en disco

### Capas de Física (Physics Layers)
- **Ground Layer**: Para detección del suelo
- **Player Layer**: Para el personaje principal
- **Killzone Layer**: Para zonas mortales

### Configuración de Input
```csharp
public enum Controls { mobile, pc }
```

## 📊 Métricas del Proyecto

### Líneas de Código
- **Total**: ~500 líneas de código C#
- **Scripts**: 7 scripts principales
- **Clases**: 6 clases personalizadas

### Recursos Gráficos
- **Sprites**: 100+ frames de animación
- **Tiles**: 18 tiles únicos
- **Efectos de Partículas**: 3 sistemas diferentes

## 🎯 Conceptos de Computación Gráfica Aplicados

### Renderizado 2D
- **Sprite Rendering**: Optimización de sprites con atlas de texturas
- **Layer Ordering**: Sistema de capas para profundidad visual
- **Culling**: Optimización de rendimiento con frustum culling

### Animación
- **Frame Animation**: Animaciones basadas en secuencias de sprites
- **State Machines**: Máquinas de estado para transiciones de animación
- **Interpolación**: Suavizado de movimientos con Lerp

### Efectos Visuales
- **Particle Systems**: Simulación de efectos ambientales
- **Screen Effects**: Transiciones fade to black/white
- **UI Animations**: Animaciones de interfaz de usuario

## 🚀 Funcionalidades Destacadas

### Sistemas Avanzados
1. **Detección de Plataformas**: Raycast para detección precisa del suelo
2. **Control Dual**: Soporte nativo para PC y móvil en el mismo código
3. **Gestión de Estado**: Sistema robusto de estados del juego
4. **Persistencia Visual**: Efectos que persisten entre estados

### Optimizaciones
- **Object Pooling**: Para efectos de partículas
- **Efficient Collision Detection**: Usando layers específicos
- **Performance Profiling**: Optimización para 60 FPS

## 📱 Compatibilidad

### Plataformas Soportadas
- **Windows** (Compilación principal)
- **Android** (Con controles táctiles)
- **WebGL** (Para distribución web)

## 👥 Información del Equipo

**Materia**: Computación Gráfica  
**Institución**: [Nombre de la Universidad]  
**Fecha de Entrega**: [Fecha]  
**Desarrollador**: [Tu Nombre]

## 🎯 Demostración para la Exposición

### Puntos Clave a Presentar
1. **Arquitectura del Código**: Explicación de patrones Singleton y organización
2. **Sistemas Gráficos**: Demostración de efectos de partículas y animaciones
3. **Mecánicas de Juego**: Showcase de controles y física
4. **Optimización**: Explicación de técnicas de rendimiento aplicadas
5. **Multiplataforma**: Demostración de controles PC vs móvil

### Escenarios de Demostración
1. **Inicio del Juego**: Transición de menú a gameplay
2. **Movimiento**: Demostración de todas las mecánicas de movimiento
3. **Recolección**: Sistema de puntuación y efectos visuales
4. **Muerte y Respawn**: Ciclo completo de muerte y reaparición
5. **Finalización**: Pantalla de nivel completado

## 📈 Posibles Mejoras Futuras

- **Sistema de Vidas**: Implementación de múltiples vidas
- **Múltiples Niveles**: Expandir a varios niveles con progresión
- **Sistema de Audio**: Integración de efectos de sonido y música
- **Enemigos**: Implementación de IA básica para oponentes
- **Power-ups**: Elementos que modifiquen las habilidades del jugador

## 📄 Licencia

Este proyecto está desarrollado con fines educativos para la materia de Computación Gráfica. 

---

*Este README ha sido creado como guía para la exposición del proyecto final. Incluye todos los aspectos técnicos y conceptuales relevantes para la presentación académica.*