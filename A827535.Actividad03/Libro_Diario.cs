using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A827535.Actividad03
{
    static class LibroDiario
    {
        private static readonly Dictionary<int, Asiento> entradas;
        const string nombreArchivo = "Diario.txt";
        static LibroDiario()
        {
            entradas = new Dictionary<int, Asiento>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var asientos = new Asiento(linea);
                        entradas.Add(asientos.NumeroAsiento, asientos);
                    }
                }

            }

        }
        public static void MovimientosFuturos(int codigoCuenta, DateTime fecha, ref decimal debe, ref decimal haber)
        {
            if (entradas.Count == 0)
            {
                Console.WriteLine("No existen asientos cargados en el Libro Diario");
            }
            else
            {
                foreach (var asiento in entradas.Values)
                {
                    if (codigoCuenta == asiento.CodCuenta)
                    {
                        if (fecha < asiento.Fecha)
                        {
                            debe += asiento.Debe;
                            haber += asiento.Haber;
                        }
                    }

                }
            }
        }
        public static Asiento Seleccionar()
        {
            var modelo = Asiento.CrearModeloBusqueda();
            foreach (var asientos in entradas.Values)
            {
                if (asientos.CoincideCon(modelo))
                {
                    return asientos;
                }
            }

            Console.WriteLine("No se ha encontrado una cuenta en el maestro");
            return null;
        }
    }
}
