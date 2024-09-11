using System;
using System.Linq;
using System.Collections.Generic;

namespace EspacioCadeteria
{
    class Program
    {
        static void Main(string[] args)
        {
            AccesoDatos accesoDatos;
            Console.WriteLine("Seleccione el tipo de archivo de datos:");
            Console.WriteLine("1. CSV");
            Console.WriteLine("2. JSON");
            Console.Write("Ingrese el número de la opción: ");
            string opcion = Console.ReadLine();
            Cadeteria cadeteria;
            string rutaCadeteria = "", rutaCadetes = "", rutaPedidos = "";

            switch (opcion)
            {
                case "1":
                    accesoDatos = new accesoCSV();
                    rutaCadeteria = "csv/cadeteria.csv";
                    rutaCadetes = "csv/cadete.csv";
                    rutaPedidos = "csv/pedidos.csv";
                    cadeteria = accesoDatos.Cargar(rutaCadeteria, rutaCadetes, rutaPedidos);
                    break;
                case "2":
                    accesoDatos = new accesoJSON();
                    rutaCadeteria = "json/cadeteria.json";
                    rutaCadetes = "json/cadete.json";
                    rutaPedidos = "json/pedidos.json";
                    cadeteria = accesoDatos.Cargar(rutaCadeteria, rutaCadetes, rutaPedidos);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Se usará el acceso por defecto (CSV).");
                    accesoDatos = new accesoCSV();
                    rutaCadeteria = "csv/cadeteria.csv";
                    rutaCadetes = "csv/cadete.csv";
                    rutaPedidos = "csv/pedidos.csv";
                    cadeteria = accesoDatos.Cargar(rutaCadeteria, rutaCadetes, rutaPedidos);
                    break;
            }

            GestorPedidos gestor = new GestorPedidos(cadeteria, accesoDatos, rutaPedidos);

            bool seguir = true;
            while (seguir)
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Dar de alta un pedido");
                Console.WriteLine("2. Asignar un pedido a un cadete");
                Console.WriteLine("3. Cambiar el estado de un pedido");
                Console.WriteLine("4. Reasignar un pedido a otro cadete");
                Console.WriteLine("5. Mostrar informe de pedidos");
                Console.WriteLine("6. Salir");
                Console.Write("Ingrese el número de la opción: ");
                string opcionMenu = Console.ReadLine();

                switch (opcionMenu)
                {
                    case "1":
                        gestor.DarDeAltaPedido();
                        break;
                    case "2":
                        gestor.AsignarPedido();
                        break;
                    case "3":
                        gestor.CambiarEstadoPedido();
                        break;
                    case "4":
                        gestor.ReasignarPedido();
                        break;
                    case "5":
                        gestor.MostrarInforme();
                        break;
                    case "6":
                        seguir = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                        break;
                }
            }
        }
    }
}
