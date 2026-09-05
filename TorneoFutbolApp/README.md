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

## Como subir esto a tu propio repositorio de GitHub

1. Crea un repositorio nuevo y vacio en https://github.com/new
2. En esta carpeta, ejecuta:

       git init
       git add .
       git commit -m "Guia de practicas 3 - conjuntos y mapas"
       git branch -M main
       git remote add origin https://github.com/TU-USUARIO/NOMBRE-REPO.git
       git push -u origin main

3. Copia la URL del repositorio y reemplaza el marcador "[pendiente...]"
   en la seccion de Anexos del informe (Informe_Practica03_ConjuntosMapas.docx).
