# UnityECS_Sample
A sample of ECS made by Unity Entities and C# job system

Unity Version : 2018.2.14

It will automatically import Entities package

if get lots of error, it possible is not find Entities package, you can manual import it from "Windows/Package Manager/All/Entities"

UseMonoBehavior is use traditional method, UseECS is use Entities and C# job System

It will generat error when running UseMonoBehavior Scene, but it no matter

In my computer test:

EnemyCount     UseMonoBehavior     UseECS     FPS
    3000             100             130      
    8000              42              70
   15000              22              40
   50000               6              10
