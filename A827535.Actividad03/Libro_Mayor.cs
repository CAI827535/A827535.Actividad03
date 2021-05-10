using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A827535.Actividad03
{
    static class LibroMayor
    {
        private static readonly Dictionary<int, Cuentas> entradasC;
        private static readonly Dictionary<int, Asiento> entradasA;
        const string nombreArchivo = "Mayor.txt";


        static LibroMayor()
        {

            entradasC = new Dictionary<int, Cuentas>();
            entradasA = new Dictionary<int, Asiento>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuentas = new Cuentas(linea);
                        entradasC.Add(cuentas.CodCuenta, cuentas);
                    }
                }

            }

        }

        public static bool Existe(int codigoCuenta)
        {
            return entradasC.ContainsKey(codigoCuenta);
        }


        public static bool Actualizar()
        {
            bool actualizar = false;
            do
            {
                if (entradasC.Count == 0)
                {

                    Console.WriteLine("Imposible actualizar las cuentas del Libro Mayor ya que no existe una carga previa" +
                        ".");
                }
                else
                {
                    foreach (var cuenta in entradasC.Values)
                    {
                        var codigoCuenta = cuenta.CodCuenta;
                        var fechaCuenta = cuenta.Fecha;
                        decimal debe = 0;
                        decimal haber = 0;

                        LibroDiario.MovimientosFuturos(codigoCuenta, fechaCuenta, ref debe, ref haber);

                        if (debe != 0 || haber != 0)
                        {
                            cuenta.Debe += debe;
                            cuenta.Haber += haber;
                            cuenta.Fecha = DateTime.Today;
                        }
                    }
                    LibroMayor.Grabar();
                }
                actualizar = true;

            } while (actualizar == false);

            return actualizar;
        }
        public static void MostrarDatosActualizados()
        {
            string Mensaje = "";
            foreach (var cuentas in entradasC.Values)
            {
                if (cuentas.Fecha == DateTime.Today)
                {
                    Mensaje += $"Cuenta: {cuentas.CodCuenta}\n" +
                               $"Fecha: {DateTime.Now.ToShortDateString()} \n" +
                               $"Saldo del Debe: {cuentas.Debe}\n" +
                               $"Saldo del Haber: {cuentas.Haber}\n" +
                               System.Environment.NewLine;
                }
            }
            if (Mensaje == "")
            {
                Console.WriteLine("No se actualizó ninguna cuenta");
            }
            if (Mensaje != "")
            {
                Console.WriteLine("Datos actualizados: " + System.Environment.NewLine + Mensaje);
            }
        }
        public static Cuentas Seleccionar()
        {
            var modelo = Cuentas.CrearModeloBusqueda();
            foreach (var cuentas in entradasC.Values)
            {
                if (cuentas.CoincideCon(modelo))
                {
                    return cuentas;
                }
            }

            Console.WriteLine("No se ha encontrado la cuenta en el maestro");
            return null;
        }

        public static void Grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var cuentas in entradasC.Values)
                {
                    var linea = cuentas.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }


    }
}
