namespace TorneoFutbolApp.Modelos
{
    // Representa a un equipo participante del torneo.
    // El codigo (por ejemplo "EQ01") funciona como clave dentro del diccionario
    // de equipos que mantiene GestorTorneo.
    public class Equipo
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Entrenador { get; set; }

        public Equipo(string codigo, string nombre, string ciudad, string entrenador)
        {
            Codigo = codigo;
            Nombre = nombre;
            Ciudad = ciudad;
            Entrenador = entrenador;
        }

        public override string ToString()
        {
            return $"{Codigo} - {Nombre} ({Ciudad}) | DT: {Entrenador}";
        }
    }
}
