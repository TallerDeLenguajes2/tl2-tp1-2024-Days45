using System;
using System.Linq;
using System.Collections.Generic;

namespace EspacioCadeteria
{
    class Program
    {
        static List<Pedidos> pedidosPendientes = new List<Pedidos>(); // Lista para pedidos pendientes

        static void Main(string[] args)
        {
            // Cargar los datos de la cadetería y los cadetes
            CargarCadeteria cargador = new CargarCadeteria();
            Cadeteria cadeteria = cargador.Cargar("csv/cadeteria.csv", "csv/cadete.csv");

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gestión de Pedidos");
                Console.WriteLine("1. Dar de alta un pedido");
                Console.WriteLine("2. Asignar pedido a un cadete");
                Console.WriteLine("3. Cambiar estado de un pedido");
                Console.WriteLine("4. Reasignar un pedido a otro cadete");
                Console.WriteLine("5. Mostrar informe al finalizar la jornada");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        DarDeAltaPedido();
                        break;
                    case "2":
                        AsignarPedido(cadeteria);
                        break;
                    case "3":
                        CambiarEstadoPedido(cadeteria);
                        break;
                    case "4":
                        ReasignarPedido(cadeteria);
                        break;
                    case "5":
                        MostrarInforme(cadeteria);
                        break;
                    case "6":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void DarDeAltaPedido()
        {
            Console.WriteLine("Dar de alta un pedido");

            Console.Write("Ingrese el número del pedido: ");
            int nro = int.Parse(Console.ReadLine());

            Console.Write("Ingrese las observaciones del pedido: ");
            string obs = Console.ReadLine();

            Console.Write("Ingrese el nombre del cliente: ");
            string nombreCliente = Console.ReadLine();

            Console.Write("Ingrese la dirección del cliente: ");
            string direccionCliente = Console.ReadLine();

            Console.Write("Ingrese el teléfono del cliente: ");
            string telefonoCliente = Console.ReadLine();

            Console.Write("Ingrese los datos de referencia de la dirección del cliente: ");
            string datosReferencia = Console.ReadLine();

            Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);
            Pedidos pedido = new Pedidos(nro, obs, cliente, Estado.Pendiente);

            pedidosPendientes.Add(pedido);
            Console.WriteLine("Pedido creado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        static void AsignarPedido(Cadeteria cadeteria)
        {
            Console.WriteLine("Asignar pedido a un cadete");

            if (!pedidosPendientes.Any())
            {
                Console.WriteLine("No hay pedidos pendientes para asignar. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Pedidos pendientes:");
            foreach (var pedido in pedidosPendientes)
            {
                Console.WriteLine($"Número: {pedido.Nro}, Cliente: {pedido.Cliente.Nombre}");
            }
            Console.Write("Ingrese el número del pedido a asignar: ");
            int nroPedido = int.Parse(Console.ReadLine());

            var pedidoSeleccionado = pedidosPendientes.FirstOrDefault(p => p.Nro == nroPedido);
            if (pedidoSeleccionado == null)
            {
                Console.WriteLine("El pedido no existe. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Cadetes disponibles:");
            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                Console.WriteLine($"{cadete.Id}: {cadete.Nombre}");
            }
            Console.Write("Ingrese el ID del cadete al que desea asignar el pedido: ");
            int idCadete = int.Parse(Console.ReadLine());

            var cadeteSeleccionado = cadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);

            if (cadeteSeleccionado != null)
            {
                cadeteria.AsignarPedido(cadeteSeleccionado, pedidoSeleccionado);
                pedidosPendientes.Remove(pedidoSeleccionado);
                Console.WriteLine("Pedido asignado exitosamente. Presione Enter para continuar.");
            }
            else
            {
                Console.WriteLine("Cadete no encontrado. Presione Enter para continuar.");
            }
            Console.ReadLine();
        }

        static void CambiarEstadoPedido(Cadeteria cadeteria)
        {
            Console.WriteLine("Cambiar estado de un pedido");

            Console.Write("Ingrese el número del pedido: ");
            int nroPedido = int.Parse(Console.ReadLine());

            Console.WriteLine("Estados disponibles:");
            foreach (var estado in Enum.GetValues(typeof(Estado)))
            {
                Console.WriteLine($"{(int)estado}: {estado}");
            }
            Console.Write("Ingrese el nuevo estado: ");
            Estado nuevoEstado = (Estado)int.Parse(Console.ReadLine());

            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                var pedido = cadete.ListaPedidos.FirstOrDefault(p => p.Nro == nroPedido);
                if (pedido != null)
                {
                    pedido.CambiarEstado(nuevoEstado);
                    Console.WriteLine("Estado del pedido cambiado exitosamente. Presione Enter para continuar.");
                    return;
                }
            }

            Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
            Console.ReadLine();
        }

        static void ReasignarPedido(Cadeteria cadeteria)
        {
            Console.WriteLine("Reasignar un pedido a otro cadete");

            Console.Write("Ingrese el número del pedido a reasignar: ");
            int nroPedido = int.Parse(Console.ReadLine());

            Console.WriteLine("Cadetes disponibles:");
            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                Console.WriteLine($"{cadete.Id}: {cadete.Nombre}");
            }
            Console.Write("Ingrese el ID del nuevo cadete: ");
            int idNuevoCadete = int.Parse(Console.ReadLine());

            Cadete cadeteAnterior = cadeteria.ListadoCadetes.FirstOrDefault(c => c.ListaPedidos.Any(p => p.Nro == nroPedido));
            var nuevoCadete = cadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);

            if (nuevoCadete != null && cadeteAnterior != null)
            {
                var pedido = cadeteAnterior.ListaPedidos.FirstOrDefault(p => p.Nro == nroPedido);
                if (pedido != null)
                {
                    cadeteria.ReasignarPedido(cadeteAnterior, nuevoCadete, pedido);
                    Console.WriteLine("Pedido reasignado exitosamente. Presione Enter para continuar.");
                }
                else
                {
                    Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
                }
            }
            else
            {
                Console.WriteLine("Cadete no encontrado. Presione Enter para continuar.");
            }
            Console.ReadLine();
        }

        static void MostrarInforme(Cadeteria cadeteria)
        {
            Console.WriteLine("Informe de pedidos al finalizar la jornada");

            var informeCadetes = cadeteria.ListadoCadetes.Select(c => new
            {
                Cadete = c,
                CantidadEnvios = c.ListaPedidos.Count,
                MontoGanado = c.ListaPedidos.Count * 100 // Supongamos que cada pedido genera 100 unidades de ganancia
            }).ToList();

            foreach (var item in informeCadetes)
            {
                Console.WriteLine($"Cadete: {item.Cadete.Nombre}, Cantidad de envíos: {item.CantidadEnvios}, Monto ganado: {item.MontoGanado}");
            }

            var totalEnvios = informeCadetes.Sum(x => x.CantidadEnvios);
            var totalGanado = informeCadetes.Sum(x => x.MontoGanado);
            var promedioEnvios = totalEnvios / (double)informeCadetes.Count;

            Console.WriteLine($"Total de envíos: {totalEnvios}");
            Console.WriteLine($"Monto total ganado: {totalGanado}");
            Console.WriteLine($"Promedio de envíos por cadete: {promedioEnvios}");

            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }
    }
}
