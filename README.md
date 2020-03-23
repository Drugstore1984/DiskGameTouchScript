Touch script for Disk throw 2D game C# Unity. 
Create 2D scene.
Create HookPlace gameobject.
Put on gameobject Rigidbody 2D (Body Type: Static).
Create Disk gameobject.
Put on gameobject Circle Collider 2D.
Put on gameobject another Circle Collider 2D(set it as Trigger).
Edit size of the Trigger Collider 2D to comfort touch via mobile device.
Put on gameobject Rigidbody 2D (Body Type: Dynamic, Gravity Scale:0).
Put on gameobject Spring Joint 2D (Connected Rigidbody: HookPlace gameobject, Distance: 0.005, Frequency: 1.5).
Put this script on the Disk gameobject (Hook Place: HookPlace gameobject, Release Time: 0.15, Max Drag Distance:2, Next Disk: duplicate Disk gameobject if necessary)
Start scene drag Disk and release to throw.

Made with Unity 2020.1.0a25.3172

