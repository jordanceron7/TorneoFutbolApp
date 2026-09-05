using TorneoFutbolApp.Modelos;

namespace TorneoFutbolApp.Servicios
{
    // Clase central del sistema. Aqui se concentra el uso de las tres
    // estructuras de datos pedidas en la guia de practicas:
    //
    //   - Dictionary<string, Equipo>            -> MAPA de equipos (clave = codigo)
    //   - Dictionary<string, Jugador>            -> DICCIONARIO de jugadores (clave = cedula)
    //   - Dictionary<string, List<string>>       -> MAPA equipo -> cedulas de su plantilla
    //   - HashSet<string>                        -> CONJUNTOS para unicidad y para las
    //                                                operaciones de union/interseccion/diferencia
    public class GestorTorneo
    {
        private readonly Dictionary<string, Equipo> _equipos = new();
        private readonly Dictionary<string, Jugador> _jugadoresPorCedula = new();
        private readonly Dictionary<string, List<string>> _plantillaPorEquipo = new();

        private readonly HashSet<string> _codigosEquipo = new();
        private readonly HashSet<string> _cedulasRegistradas = new();

        // ---------- REGISTRO ----------

        public bool RegistrarEquipo(Equipo equipo)
        {
            // HashSet.Add devuelve false si el elemento ya existia: asi evitamos
            // codigos de equipo repetidos sin recorrer ninguna lista.
            if (!_codigosEquipo.Add(equipo.Codigo))
                return false;

            _equipos[equipo.Codigo] = equipo;
            _plantillaPorEquipo[equipo.Codigo] = new List<string>();
            return true;
        }

        public bool RegistrarJugador(Jugador jugador)
        {
            if (!_equipos.ContainsKey(jugador.CodigoEquipo))
                return false; // el equipo debe existir primero

            // La cedula debe ser unica en todo el torneo (conjunto = sin duplicados).
            if (!_cedulasRegistradas.Add(jugador.Cedula))
                return false;

            _jugadoresPorCedula[jugador.Cedula] = jugador;
            _plantillaPorEquipo[jugador.CodigoEquipo].Add(jugador.Cedula);
            return true;
        }

        // ---------- CONSULTAS (REPORTERIA) ----------

        public IEnumerable<Equipo> ListarEquipos() => _equipos.Values;

        public Jugador? BuscarJugadorPorCedula(string cedula)
        {
            // Acceso directo por clave: O(1) en promedio gracias al diccionario.
            _jugadoresPorCedula.TryGetValue(cedula, out var jugador);
            return jugador;
        }

        public List<Jugador> ListarJugadoresDeEquipo(string codigoEquipo)
        {
            var resultado = new List<Jugador>();
            if (!_plantillaPorEquipo.TryGetValue(codigoEquipo, out var cedulas))
                return resultado;

            foreach (var cedula in cedulas)
                resultado.Add(_jugadoresPorCedula[cedula]);

            return resultado;
        }

        public int TotalEquipos => _equipos.Count;
        public int TotalJugadores => _jugadoresPorCedula.Count;

        public double EdadPromedio(string codigoEquipo)
        {
            var jugadores = ListarJugadoresDeEquipo(codigoEquipo);
            if (jugadores.Count == 0) return 0;
            return jugadores.Average(j => j.Edad);
        }

        // ---------- OPERACIONES DE CONJUNTOS ----------

        // Devuelve el conjunto de posiciones distintas que cubre un equipo.
        // Por ejemplo: { "Portero", "Defensa", "Volante", "Delantero" }
        public HashSet<string> PosicionesCubiertas(string codigoEquipo)
        {
            var posiciones = new HashSet<string>();
            foreach (var jugador in ListarJugadoresDeEquipo(codigoEquipo))
                posiciones.Add(jugador.Posicion);
            return posiciones;
        }

        // Compara tacticamente dos equipos a partir de las posiciones que cada
        // uno cubre en su plantilla, usando union, interseccion y diferencia.
        public (HashSet<string> union, HashSet<string> interseccion,
                HashSet<string> soloA, HashSet<string> soloB)
            CompararPlantillas(string codigoA, string codigoB)
        {
            var posicionesA = PosicionesCubiertas(codigoA);
            var posicionesB = PosicionesCubiertas(codigoB);

            var union = new HashSet<string>(posicionesA);
            union.UnionWith(posicionesB);

            var interseccion = new HashSet<string>(posicionesA);
            interseccion.IntersectWith(posicionesB);

            var soloA = new HashSet<string>(posicionesA);
            soloA.ExceptWith(posicionesB);

            var soloB = new HashSet<string>(posicionesB);
            soloB.ExceptWith(posicionesA);

            return (union, interseccion, soloA, soloB);
        }

        // Acceso de solo lectura a las cedulas registradas, usado por el
        // modulo de analisis de tiempo de ejecucion (Servicios/AnalizadorRendimiento.cs).
        public HashSet<string> CedulasRegistradas => _cedulasRegistradas;
        public Dictionary<string, Jugador> MapaJugadores => _jugadoresPorCedula;
    }
}
