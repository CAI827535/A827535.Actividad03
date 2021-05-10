using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A827535.Actividad03
{
    class Asientos
    {
        public int NroAsiento { get; set; }
        public DateTime Fecha { get; set; }
        public int CodCuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }

        public Asientos() { }

        public Asientos(string linea)
        {
            var datos = linea.Split(';');
            NroAsiento = int.Parse(datos[0]);
            Fecha = DateTime.Parse(datos[1]);
            CodCuenta = int.Parse(datos[2]);
            Debe = decimal.Parse(datos[3]);
            Haber = decimal.Parse(datos[4]);
        }

        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine($"Nro de Asiento {NroAsiento}");
            Console.WriteLine($"Fecha {Fecha.ToShortDateString()}");
            Console.WriteLine($"Cod. de Cuenta {CodCuenta}");
            Console.WriteLine($"Debe {Debe}");
            Console.WriteLine($"Haber {Haber}");
            Console.WriteLine();
        }

        public static Asientos CrearModeloBusqueda()
        {
            var modelo = new Asientos();
            modelo.NroAsiento = IngresarNroAsiento(obligatorio: false);
            return modelo;
        }
        public bool CoincideCon(Asientos modelo)
        {
            if (modelo.NroAsiento != 0 && modelo.NroAsiento != NroAsiento)
            {
                return false;
            }
            return true;
        }
        private static int IngresarNroAsiento(bool obligatorio = true)
        {
            var titulo = "Ingrese el nro de asiento";
            if (!obligatorio)
            {
                titulo += " o presione la tecla ENTER si desea continuar";
            }

            do
            {
                Console.WriteLine(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }

                if (!int.TryParse(ingreso, out var NroAsiento))
                {
                    Console.WriteLine("Debe ingresar un nro de asiento válido");
                    continue;
                }

                return NroAsiento;

            } while (true);
        }
    }
}

