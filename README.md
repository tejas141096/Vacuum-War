# Vacuum War
An alternative controller VR game project.

# Installation

## Requirements
[Unity 2020.3.26f](https://unity3d.com/get-unity/download/archive)

OpenXR compatible VR headset, Oculus Quest 2 + [Oculus Link](https://www.oculus.com/setup/) prefered.

VR-ready PC

Bluetooth on your PC

## Setup

### Oculus Setup
Only when you are using the Oculus Quest / Quest 2
1. Configure the guardian system in the Quest headset to make sure the floor height is correct.
2. Install [Oculus Link App](https://www.oculus.com/setup/).
3. Connect your Quest headset to the PC via USB cable.
4. Follow the instruction to setup the Oculus Link.
5. Download [Oculus Developer Hub](https://developer.oculus.com/documentation/unity/ts-odh/).
6. Connect your Quest headset to the PC via USB and configure to disable proximity sensor and guardian system.
7. If the device doesn't show up in the Oculus Developer Hub, disbale **Air Link** in Quest headset.

### VR Setup
1. Install [SteamVR](https://store.steampowered.com/app/250820/SteamVR/)
2. Set SteamVR as the default OpenXR Runtime: Open **SteamVR > Settings > Enable Advanced > Developer > Switch OpenXR runtime to SteamVR**.
3. Make sure your VR headset can be detected by SteamVR and run the SteamVR Home unless you disable it.  It's recommended to do so when developing.

### Bluetooth Setup
1. Plug in a power supply to the internal USB port of the controller chip.  Either PC, AC adapter or external battery are ok.
2. Once the red led turns on, try add Bluetooth device on your PC, then connect to “ESP32Test”.

### Project Setup
1. Clone the repository and open the `/VacuumWar` project folder in Unity.
2. Open the prototype scene `/VacuumWar/Assets/Scenes/Prototype/PrototypeScene.unity`
3. Hit Play button to start the game.

# How-to-play
1. Trigger - use the head's function.  Shooter head for shooting and vacuum roller head for vacuuming.
2. Hold A button to start scoring.
3. Attach the head to the front of the controller to switch between heads.
4. Use the vacuum roller head to vacuum screws on the floor an collect ammo.
5. Try to beat the high score.

### Developer's cheat list:
1. Double tap A: force change to vacuum roller head
2. Double tap B: force change to shooter head
3. Double tap thumbstick: force empty the head
4. Hold thumbstick: fill ammo

# Developer's document
`Vacuum-War/RawContent/Documents/PrototypeWarpKit/Documentation/Developer_s Document.docx`
