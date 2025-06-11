# VR-Based Digital Twin of a Greenhouse

![VR Greenhouse](/Docs/Img/Thesis_cover.png)

## Description

This project is a VR based digital twin of a greenhouse. The project is developed using Unity and C# in order to simulate the greenhouse environment. The project is developed as a part of the courses "Master Preparatory project" and "Master Thesis" in the Master's program in Informatics with a specialization in Software Engineering at the Norwegian University of Science and Technology (NTNU).

## Pre-requisites

- Unity 6000.0.24f1
- Visual Studio Code with the following extensions:
  - C# extension
  - CSharpier extension
- Oculus 2 Quest
- Oculus Link (for development, optional)

## Installation

1. Clone the repository
2. Open the project in Unity
3. Wait for the project to load and initialize
4. Connect the Oculus Quest 2 to the computer
5. Go to `File -> Build Profiles` and select the platform as `Android` (if not already selected)
6. Select your oculus device from the `Run Device` dropdown
7. Click on `Build and Run` to build the project and run it on the Oculus Quest 2

## Development

### Coding Style

- Use CSharpier vscode extension to format the code according to the C# coding standards. Configure the IDE to format the code on save.

## Demo

![Lights On](/Docs/Img/results_lights_on_1.png)
![Lights Off](/Docs/Img/results_lights_off_1.png)
![Airflow Visualization](/Docs/Img/airflow_result_2.png)
![Plant Comparison](/Docs/Img/plant_model_quality_compare.png)
![Water Droplets](/Docs/Img/water_droplets.png)
![Upper Fan Particles](/Docs/Img/upper_fan_particle.png)
![Both Fans Particles](/Docs/Img/airflow_particles.png)
![User Interface](/Docs/Img/UI%20right%20live.png)

## Project Structure

```text
Assets
├── Art
│   ├── Materials
│   ├── Models
│   └── Textures
├── Multimedia
│   ├── Audio
│   │   ├── Music
│   │   └── Sound
│   └── Video
├── Code
│   ├── Scripts # C# scripts
│   └── Shaders # Shader files and shader graphs
├── Level  # Anything related to game design in Unity
│   ├── Prefabs
│   ├── Scenes
│   └── UI
├── Samples  # Any sample assets or scenes
├── Settings
├── Plugins  # Any third-party plugins or assets
└── Docs  # Wiki, documentation, and other project related files
```
