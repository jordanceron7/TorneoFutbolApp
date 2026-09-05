using System.Diagnostics;

namespace TorneoFutbolApp.Servicios
{
    // Este modulo no forma parte de la logica de negocio del torneo; se creo
    // exclusivamente para medir, con datos reales, la diferencia de tiempo de
    // busqueda entre una lista, un conjunto (HashSet) y un diccionario cuando
    // el volumen de datos crece. Los resultados de esta clase se citan en el
    // apartado de Resultados del informe.
    public class AnalizadorRendimiento
    {
        public record ResultadoMedicion(int N, double MsLista, double MsHashSet, double MsDiccionario);

        public static List<ResultadoMedicion> Ejecutar(int[] tamanos, int busquedasPorPrueba = 2000)
        {
            var resultados = new List<ResultadoMedicion>();
            var rnd = new Random(42); // semilla fija para que la prueba sea repetible

            foreach (var n in tamanos)
            {
                // Generamos "cedulas" sinteticas de 10 digitos, todas distintas.
                var datos = new List<string>(n);
                for (int i = 0; i < n; i++)
                    datos.Add(i.ToString("D10"));

                var lista = new List<string>(datos);
                var conjunto = new HashSet<string>(datos);
                var diccionario = new Dictionary<string, int>(n);
                for (int i = 0; i < n; i++) diccionario[datos[i]] = i;

                // Claves a buscar: mitad existentes, mitad inexistentes, en orden aleatorio.
                var claves = new List<string>(busquedasPorPrueba);
                for (int i = 0; i < busquedasPorPrueba; i++)
                {
                    if (i % 2 == 0)
                        claves.Add(datos[rnd.Next(n)]);
                    else
                        claves.Add("X" + rnd.Next(int.MaxValue)); // no existe
                }

                var swLista = Stopwatch.StartNew();
                foreach (var c in claves) _ = lista.Contains(c);
                swLista.Stop();

                var swSet = Stopwatch.StartNew();
                foreach (var c in claves) _ = conjunto.Contains(c);
                swSet.Stop();

                var swDic = Stopwatch.StartNew();
                foreach (var c in claves) _ = diccionario.ContainsKey(c);
                swDic.Stop();

                resultados.Add(new ResultadoMedicion(
                    n,
                    swLista.Elapsed.TotalMilliseconds,
                    swSet.Elapsed.TotalMilliseconds,
                    swDic.Elapsed.TotalMilliseconds));
            }

            return resultados;
        }
    }
}
