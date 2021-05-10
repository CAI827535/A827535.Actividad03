using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A827535.Actividad03
{
    class Cuentas
{
    public int CodCuenta { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Debe { get; set; }
    public decimal Haber { get; set; }
    public Cuentas() { }
    public Cuentas(string linea)
    {
        var datos = linea.Split(';');
        CodCuenta = int.Parse(datos[0]);
        Fecha = DateTime.Parse(datos[1]);
        Debe = decimal.Parse(datos[2]);
        Haber = decimal.Parse(datos[3]);
    }

    public string ObtenerLineaDatos() => $"{CodCuenta};{Fecha};{Debe};{Haber}";

    public void Mostrar()
    {
        Console.WriteLine();
        Console.WriteLine($"Codigo cuenta: {CodCuenta}");
        Console.WriteLine($"Fecha: {Fecha.ToShortDateString()}");
        Console.WriteLine($"Debe: {Debe}");
        Console.WriteLine($"Haber: {Haber}");
        Console.WriteLine();
    }



    public static Cuentas CrearModeloBusqueda()
    {
        var modelo = new Cuentas();
        modelo.CodCuenta = IngresarCodigoCuenta(obligatorio: false);
        modelo.Fecha = IngresarFecha("Ingrese la fecha", obligatorio: false);
        return modelo;
    }
    public bool CoincideCon(Cuentas modelo)
    {
        if (modelo.CodCuenta != 0 && modelo.CodCuenta != CodCuenta)
        {
            return false;
        }
        if (modelo.Fecha != DateTime.MinValue && Fecha != modelo.Fecha)
        {
            return false;
        }
        return true;
    }

    private static int IngresarCodigoCuenta(bool obligatorio = true)
    {
        var titulo = "Ingresar el código de la cuenta";
        if (!obligatorio)
        {
            titulo += " o presione ENTER si desea continuar";
        }

        do
        {
            Console.WriteLine(titulo);
            var ingreso = Console.ReadLine();
            if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
            {
                return 0;
            }

            if (!int.TryParse(ingreso, out var codigoCuenta))
            {
                Console.WriteLine("No ha ingresado un codigo de cuenta válido");
                continue;
            }

            return codigoCuenta;

        } while (true);
    }

    private static DateTime IngresarFecha(string titulo, bool obligatorio = true)
    {
        do
        {
            if (!obligatorio)
            {
                titulo += " o presione [Enter para continuar]";
            }

            Console.WriteLine(titulo);

            var ingreso = Console.ReadLine();

            if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
            {
                return DateTime.MinValue;
            }

            if (!DateTime.TryParse(ingreso, out DateTime fechaNacimiento))
            {
                Console.WriteLine("No es una fecha válida");
                continue;
            }
            if (fechaNacimiento > DateTime.Now)
            {
                Console.WriteLine("Debe ingresar una fecha menor a la actual");
                continue;
            }
            return fechaNacimiento;

        } while (true);
    }

}
}
