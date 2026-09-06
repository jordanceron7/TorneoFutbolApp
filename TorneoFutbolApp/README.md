# Sistema de Registro de Jugadores y Equipos - Torneo de Futbol

Guia de Practicas N.3 - Estructura de Datos - UEA
Unidad III: Conjuntos y Mapas

## Como ejecutar

Requiere el SDK de .NET 8.0 (https://dotnet.microsoft.com/download).

    dotnet run

## Estructura del proyecto

- Program.cs                       -> menu de consola (punto de entrada)
- Modelos/Jugador.cs                -> entidad Jugador
- Modelos/Equipo.cs                 -> entidad Equipo
- Servicios/GestorTorneo.cs          -> logica principal: Dictionary, HashSet
- Servicios/AnalizadorRendimiento.cs -> benchmark List vs HashSet vs Dictionary

