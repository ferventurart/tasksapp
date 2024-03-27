# Task app
_Una simple aplicacion para manejar tus tareas diarias._

## Acerca de 📕
_Esta aplicacion consta de un fron-end hecho Angular 17 y un backend utilizando C#12 y .NET 8. Los requisitos tecnicos para tener instalada esta aplicacion son los siguientes:_
- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node JS - LTS](https://nodejs.org/en/download)
- [Visual Studio Code](https://code.visualstudio.com/download)

## Instalación 🔧
_Para correr esta app en un ambiente local deberas ejecutar los siguientes pasos:_
- Descarga el codigo fuente

- Abre una nueva teriminal en Visual Studio Code y muevete hacia la carpeta Web.Api
```
cd Web.Api
```
- Ejecuta el comando **dotnet run**:
```
dotnet run
```
- Abre otra terminal en Visual Studio Code y muevete hacia la carpeta Web
```
cd Web
```
- Ejecuta el comando **ng serve --open**:
```
ng serve --open
```

## Patrones de diseno utilizados 🛠️

_Este proyecto cuenta a nivel backend la implementacion de Vertical Slice Architecture_

![VSA](https://blog.ndepend.com/wp-content/uploads/net-vertical-slice-architecture.png)

Esta arquitectura se basa en una simple idea : _organizar tu codigo por funcionalidades_. Al contrario de las arquitecturas en capas donde se organiza el sistema orientado a capas tecnicas. 