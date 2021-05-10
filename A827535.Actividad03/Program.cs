using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A827535.Actividad03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Opciones disponibles:");
                Console.WriteLine("");
                Console.WriteLine("1. Consultar Libro Mayor");
                Console.WriteLine("2. Actualizar Libro Mayor");
                Console.WriteLine("3. Consultar Libro Diario");
                Console.WriteLine("4. Salir");

                Console.WriteLine("Ingresar una opción del listado anterior y presionar ENTER");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Consultar();
                        break;
                    case "2":
                        ActualizarMayor();
                        break;
                    case "3":
                        MostrarLDiario();
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Ingrese una opción correcta");
                        break;
                }

            } while (!salir);
        }

        private static void Consultar()
        {
            var cuentas = LibroMayor.Seleccionar();
            cuentas?.Mostrar();
        }

        private static void ActualizarMayor()
        {
            var cuentas = LibroMayor.Actualizar();
            if (cuentas == false)
            {
                return;
            }

            LibroMayor.MostrarDatosActualizados();
        }
        private static void MostrarLDiario()
        {
            var asientos = LibroDiario.Seleccionar();
            asientos?.Mostrar();
        }

    }
}
