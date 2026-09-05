using TorneoFutbolApp.Modelos;
using TorneoFutbolApp.Servicios;

var gestor = new GestorTorneo();
CargarDatosDemo(gestor);

bool salir = false;
while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("=======================================================");
    Console.WriteLine(" SISTEMA DE REGISTRO - TORNEO DE FUTBOL");
    Console.WriteLine(" Guia de Practicas N.3 - Estructura de Datos - UEA");
    Console.WriteLine("=======================================================");
    Console.WriteLine("1. Registrar equipo");
    Console.WriteLine("2. Registrar jugador");
    Console.WriteLine("3. Listar equipos");
    Console.WriteLine("4. Listar jugadores de un equipo");
    Console.WriteLine("5. Buscar jugador por cedula");
    Console.WriteLine("6. Comparar plantillas de dos equipos (union/interseccion/diferencia)");
    Console.WriteLine("7. Estadisticas generales del torneo");
    Console.WriteLine("8. Analizar tiempo de ejecucion (Lista vs Conjunto vs Diccionario)");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opcion: ");

    var opcion = Console.ReadLine();
    Console.WriteLine();

    switch (opcion)
    {
        case "1": RegistrarEquipoInteractivo(gestor); break;
        case "2": RegistrarJugadorInteractivo(gestor); break;
        case "3": ListarEquipos(gestor); break;
        case "4": ListarJugadoresDeEquipo(gestor); break;
        case "5": BuscarJugador(gestor); break;
        case "6": CompararPlantillas(gestor); break;
        case "7": EstadisticasGenerales(gestor); break;
        case "8": AnalizarRendimiento(); break;
        case "0": salir = true; break;
        default: Console.WriteLine("Opcion invalida."); break;
    }
}

Console.WriteLine("Gracias por usar el sistema. Hasta luego.");


// ---------------------- FUNCIONES DE APOYO DEL MENU ----------------------

static void RegistrarEquipoInteractivo(GestorTorneo gestor)
{
    Console.Write("Codigo del equipo (ej. EQ04): ");
    var codigo = Console.ReadLine() ?? "";
    Console.Write("Nombre del equipo: ");
    var nombre = Console.ReadLine() ?? "";
    Console.Write("Ciudad: ");
    var ciudad = Console.ReadLine() ?? "";
    Console.Write("Entrenador: ");
    var entrenador = Console.ReadLine() ?? "";

    var equipo = new Equipo(codigo, nombre, ciudad, entrenador);
    if (gestor.RegistrarEquipo(equipo))
        Console.WriteLine($"Equipo '{nombre}' registrado correctamente.");
    else
        Console.WriteLine($"ERROR: el codigo '{codigo}' ya esta en uso.");
}

static void RegistrarJugadorInteractivo(GestorTorneo gestor)
{
    Console.Write("Codigo del equipo al que pertenece: ");
    var codigoEquipo = Console.ReadLine() ?? "";
    Console.Write("Cedula: ");
    var cedula = Console.ReadLine() ?? "";
    Console.Write("Nombres: ");
    var nombres = Console.ReadLine() ?? "";
    Console.Write("Apellidos: ");
    var apellidos = Console.ReadLine() ?? "";
    Console.Write("Edad: ");
    int.TryParse(Console.ReadLine(), out int edad);
    Console.Write("Posicion (Portero/Defensa/Volante/Delantero): ");
    var posicion = Console.ReadLine() ?? "";
    Console.Write("Numero de camiseta: ");
    int.TryParse(Console.ReadLine(), out int numero);

    var jugador = new Jugador(cedula, nombres, apellidos, edad, posicion, numero, codigoEquipo);
    if (gestor.RegistrarJugador(jugador))
        Console.WriteLine($"Jugador '{nombres} {apellidos}' registrado en {codigoEquipo}.");
    else
        Console.WriteLine("ERROR: cedula duplicada o el equipo no existe.");
}

static void ListarEquipos(GestorTorneo gestor)
{
    Console.WriteLine("--- Equipos registrados ---");
    foreach (var equipo in gestor.ListarEquipos())
        Console.WriteLine(equipo);
}

static void ListarJugadoresDeEquipo(GestorTorneo gestor)
{
    Console.Write("Codigo del equipo: ");
    var codigo = Console.ReadLine() ?? "";
    var jugadores = gestor.ListarJugadoresDeEquipo(codigo);
    if (jugadores.Count == 0)
    {
        Console.WriteLine("No se encontraron jugadores para ese equipo.");
        return;
    }
    Console.WriteLine($"--- Plantilla de {codigo} ({jugadores.Count} jugadores) ---");
    foreach (var jugador in jugadores)
        Console.WriteLine(jugador);
}

static void BuscarJugador(GestorTorneo gestor)
{
    Console.Write("Cedula a buscar: ");
    var cedula = Console.ReadLine() ?? "";
    var jugador = gestor.BuscarJugadorPorCedula(cedula);
    Console.WriteLine(jugador is null ? "No existe un jugador con esa cedula." : jugador.ToString());
}

static void CompararPlantillas(GestorTorneo gestor)
{
    Console.Write("Codigo del equipo A: ");
    var a = Console.ReadLine() ?? "";
    Console.Write("Codigo del equipo B: ");
    var b = Console.ReadLine() ?? "";

    var (union, interseccion, soloA, soloB) = gestor.CompararPlantillas(a, b);

    Console.WriteLine($"Union de posiciones cubiertas         : {{{string.Join(", ", union)}}}");
    Console.WriteLine($"Interseccion (posiciones en comun)    : {{{string.Join(", ", interseccion)}}}");
    Console.WriteLine($"Solo en {a}                          : {{{string.Join(", ", soloA)}}}");
    Console.WriteLine($"Solo en {b}                          : {{{string.Join(", ", soloB)}}}");
}

static void EstadisticasGenerales(GestorTorneo gestor)
{
    Console.WriteLine("--- Estadisticas generales del torneo ---");
    Console.WriteLine($"Total de equipos   : {gestor.TotalEquipos}");
    Console.WriteLine($"Total de jugadores : {gestor.TotalJugadores}");
    foreach (var equipo in gestor.ListarEquipos())
    {
        var cantidad = gestor.ListarJugadoresDeEquipo(equipo.Codigo).Count;
        var promedio = gestor.EdadPromedio(equipo.Codigo);
        Console.WriteLine($"  {equipo.Nombre,-20} jugadores: {cantidad,3}   edad promedio: {promedio:F1}");
    }
}

static void AnalizarRendimiento()
{
    Console.WriteLine("Ejecutando pruebas de tiempo de busqueda (2000 busquedas por tamano)...");
    var tamanos = new[] { 1_000, 10_000, 50_000, 100_000, 300_000 };
    var resultados = AnalizadorRendimiento.Ejecutar(tamanos);

    Console.WriteLine();
    Console.WriteLine($"{"N registros",12} | {"List<T> (ms)",13} | {"HashSet<T> (ms)",16} | {"Dictionary (ms)",16}");
    Console.WriteLine(new string('-', 66));
    foreach (var r in resultados)
    {
        Console.WriteLine($"{r.N,12:N0} | {r.MsLista,13:F3} | {r.MsHashSet,16:F3} | {r.MsDiccionario,16:F3}");
    }
}

static void CargarDatosDemo(GestorTorneo gestor)
{
    gestor.RegistrarEquipo(new Equipo("EQ01", "Amazonicos FC", "Puyo", "Carlos Andrade"));
    gestor.RegistrarEquipo(new Equipo("EQ02", "Deportivo Napo", "Tena", "Luis Vargas"));
    gestor.RegistrarEquipo(new Equipo("EQ03", "Union Pastaza", "Puyo", "Jorge Salazar"));

    gestor.RegistrarJugador(new Jugador("1750001111", "Mateo", "Chimbo", 22, "Portero", 1, "EQ01"));
    gestor.RegistrarJugador(new Jugador("1750002222", "Bryan", "Yumbo", 24, "Defensa", 4, "EQ01"));
    gestor.RegistrarJugador(new Jugador("1750003333", "Kevin", "Alvarado", 21, "Volante", 8, "EQ01"));
    gestor.RegistrarJugador(new Jugador("1750004444", "Diego", "Peralta", 26, "Delantero", 9, "EQ01"));

    gestor.RegistrarJugador(new Jugador("1750005555", "Sebastian", "Loor", 23, "Portero", 1, "EQ02"));
    gestor.RegistrarJugador(new Jugador("1750006666", "Andres", "Chalan", 25, "Defensa", 3, "EQ02"));
    gestor.RegistrarJugador(new Jugador("1750007777", "Wilson", "Guaman", 27, "Volante", 6, "EQ02"));

    gestor.RegistrarJugador(new Jugador("1750008888", "Paul", "Zambrano", 20, "Defensa", 2, "EQ03"));
    gestor.RegistrarJugador(new Jugador("1750009999", "Ivan", "Toapanta", 29, "Delantero", 11, "EQ03"));
}
