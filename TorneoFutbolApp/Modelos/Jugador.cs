namespace TorneoFutbolApp.Modelos
{
    // Representa a un jugador inscrito en el torneo.
    // La cedula se usa como clave unica en las estructuras de tipo mapa/diccionario.
    public class Jugador
    {
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; } // Portero, Defensa, Volante, Delantero
        public int NumeroCamiseta { get; set; }
        public string CodigoEquipo { get; set; }

        public Jugador(string cedula, string nombres, string apellidos, int edad,
                        string posicion, int numeroCamiseta, string codigoEquipo)
        {
            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
            Edad = edad;
            Posicion = posicion;
            NumeroCamiseta = numeroCamiseta;
            CodigoEquipo = codigoEquipo;
        }

        public override string ToString()
        {
            return $"[{NumeroCamiseta:D2}] {Nombres} {Apellidos} - {Posicion} " +
                   $"(CI: {Cedula}, Edad: {Edad})";
        }
    }
}
