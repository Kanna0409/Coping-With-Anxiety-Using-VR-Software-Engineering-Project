# Coping-With-Anxiety-Using-VR-Software-Engineering-Project

Links to Apk Files since Files were to large to upload to github
https://drive.google.com/drive/folders/1kHS40T3MU61BvcKHELO58momA1SPAK6c

Playlist Link:
[https://youtube.com/playlist?list=PL7cITzoLfhbzPYVjYnW0R0CW5o3dEt35_](https://www.youtube.com/playlist?list=PLcidgIOd7ToGSBdMl8tyOB5KsyeRnpsCV)


# VR Nature Escape – Safari & Beach in Unity:
A VR experience to reduce anxiety and bring peace of mind through immersive nature environments.

# About the Project:
We created a Virtual Reality (VR) application using Unity and the Google Cardboard XR Plugin. The project features two relaxing environments:
Safari – An immersive forest trail with animated animals.
Beach (Boating) – A peaceful boat ride through calm waters.
Designed to offer users a mental escape, this app helps reduce anxiety and promotes calmness and mindfulness through guided navigation, ambient visuals, and gentle animation.

# Built With:
Unity (Game Engine)
C# (Scripting)
Google Cardboard XR Plugin
Firebase (Authentication)
NavMesh (Animal Navigation)
Unity UI Canvas

# Environments Overview:
Safari (Forest Trail)
Imported assets: ADG Textures, ithappy, URP Tree Models
Sculpted terrain using Raise/Lower Terrain for mountains
Applied green terrain textures and carved a road path
Planted trees using Paint Trees option
Added animal prefabs like lions and elephants
Used built-in animations like idle, walk, and run using Animator
Configured NavMesh Surface and animals as NavMesh Agents
XR Rig placed inside a capsule that moves along waypoints

# Beach (Boating Scene):
Created a new terrain and added a Water Plane
Painted with green and dirt textures
Inserted boat prefab with XR Rig inside
Sculpted terrain edges to form a shoreline
Created waypoints in the water for guided boat movement
Wrote movement script using C# to follow waypoints

# XR Integration:
Integrated Google Cardboard XR Plugin
Converted cameras to XR Rigs
Edited mainTemplate.gradle and gradle.properties
Updated Player Settings
Movement scripts created in C# for capsule and boat


# User Interface & Authentication:
Built 4 panels using Unity Canvas:
Login
Signup
Account Recovery
Environment Selection
Panels rendered by a dedicated UI Camera
Login required before accessing environments
Selected XR Rig activated based on user’s choice
Firebase Authentication handled via FirebaseController.cs


# Project Settings:
Minimum API Level: 26
Target API Level: Highest Installed
Texture Compression: ETC2
Graphics API: OpenGLES3
Scripting Backend: IL2CPP
Target Architectures: ARMv7, ARM64

