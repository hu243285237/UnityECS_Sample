# UnityECS_Sample
A sample of ECS made by Unity Entities and C# job system

Unity Version : 2018.2.14

It will automatically import Entities package

if get lots of error, it possible is not find Entities package, you can manual import it from "Windows/Package Manager/All/Entities"

UseMonoBehavior is use traditional method, UseECS is use Entities and C# job System

It will generat error when running UseMonoBehavior Scene, but it no matter

In my computer test:

EnemyCount 3000  |  UseMonoBehaviorFPS 100  |  UseECSFPS 130

EnemyCount 8000  |  UseMonoBehaviorFPS 42  |  UseECSFPS 70

EnemyCount 15000  |  UseMonoBehaviorFPS 22  |  UseECSFPS 40

EnemyCount 50000  |  UseMonoBehaviorFPS 6  |  UseECSFPS 10
