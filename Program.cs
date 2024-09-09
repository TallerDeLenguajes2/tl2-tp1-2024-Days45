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
                        DarDeAltaPedido(cadeteria, accesoDatos, rutaPedidos);
                        break;
                    case "2":
                        AsignarPedido(cadeteria, accesoDatos, rutaPedidos);
                        break;
                    case "3":
                        CambiarEstadoPedido(cadeteria, accesoDatos, rutaPedidos);
                        break;
                    case "4":
                        ReasignarPedido(cadeteria, accesoDatos, rutaPedidos);
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

        static void DarDeAltaPedido(Cadeteria cadeteria, AccesoDatos accesoDatos, string rutaPedidos)
        {
            Console.WriteLine("Pedidos anteriores:");
            cadeteria.MostrarPedidos();
            Console.WriteLine("Dar de alta un pedido");
            int nro = cadeteria.ListadoPedidos.Any() ? cadeteria.ListadoPedidos.Max(p => p.Nro) + 1 : 1;
            Console.WriteLine($"Número del nuevo pedido: {nro}");
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
            cadeteria.agregarPedido(pedido);
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);
            Console.WriteLine("Pedido creado y guardado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }


        static void AsignarPedido(Cadeteria cadeteria, AccesoDatos accesoDatos, string rutaPedidos)
        {
            Console.WriteLine("Asignar pedido a un cadete");

            var pedidosSinCadete = cadeteria.ListadoPedidos.Where(p => p.Cadete == null).ToList();

            if (!pedidosSinCadete.Any())
            {
                Console.WriteLine("No hay pedidos pendientes de asignación. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Pedidos sin cadete asignado:");
            foreach (var pedido in pedidosSinCadete)
            {
                Console.WriteLine($"Número: {pedido.Nro}, Observaciones: {pedido.Obs}, Cliente: {pedido.Cliente.Nombre}");
            }

            Console.Write("Ingrese el número del pedido a asignar: ");
            int nroPedido = int.Parse(Console.ReadLine());

            var pedidoSeleccionado = pedidosSinCadete.FirstOrDefault(p => p.Nro == nroPedido);
            if (pedidoSeleccionado == null)
            {
                Console.WriteLine("El pedido no existe o ya está asignado a un cadete. Presione Enter para continuar.");
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

            cadeteria.asignarCadetePedido(idCadete, nroPedido);

            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);

            Console.WriteLine("Pedido asignado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        static void CambiarEstadoPedido(Cadeteria cadeteria, AccesoDatos accesoDatos, string rutaPedidos)
        {
            Console.WriteLine("Pedidos registrados:");
            cadeteria.MostrarPedidos();
            Console.WriteLine("Cambiar estado de un pedido");

            Console.Write("Ingrese el número del pedido: ");
            int nroPedido = int.Parse(Console.ReadLine());

            var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
            if (pedido == null)
            {
                Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            if (pedido.Estado == Estado.Entregado)
            {
                Console.WriteLine("No se puede cambiar el estado de un pedido que ya ha sido entregado.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Estados disponibles:");
            foreach (var estado in Enum.GetValues(typeof(Estado)))
            {
                Console.WriteLine($"{(int)estado}: {estado}");
            }
            Console.Write("Ingrese el nuevo estado: ");
            Estado nuevoEstado = (Estado)int.Parse(Console.ReadLine());

            pedido.CambiarEstado(nuevoEstado);

            // Verificar si el pedido debe ser eliminado
            if (nuevoEstado == Estado.Cancelado)
            {
                cadeteria.eliminarPedido(pedido);
                Console.WriteLine($"El pedido {nroPedido} ha sido eliminado debido a su estado '{nuevoEstado}'.");
            }

            // Guardar la lista de pedidos actualizada
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);

            Console.WriteLine("Estado del pedido cambiado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        static void ReasignarPedido(Cadeteria cadeteria, AccesoDatos accesoDatos, string rutaPedidos)
        {
            Console.WriteLine("Reasignar un pedido a otro cadete");

            cadeteria.MostrarPedidos();
            Console.Write("Ingrese el número del pedido a reasignar: ");
            int nroPedido = int.Parse(Console.ReadLine());

            var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
            if (pedido == null)
            {
                Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            if (pedido.Cadete == null)
            {
                Console.WriteLine("El pedido no está asignado a ningún cadete y no puede ser reasignado.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Cadetes disponibles:");
            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                Console.WriteLine($"{cadete.Id}: {cadete.Nombre}");
            }
            Console.Write("Ingrese el ID del nuevo cadete: ");
            int idNuevoCadete = int.Parse(Console.ReadLine());

            cadeteria.asignarCadetePedido(idNuevoCadete, nroPedido);

            // Guardar la lista de pedidos actualizada
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);

            Console.WriteLine("Pedido reasignado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }


        static void MostrarInforme(Cadeteria cadeteria)
        {
            Console.WriteLine("Informe de pedidos al finalizar la jornada");

            var informeCadetes = cadeteria.ListadoCadetes.Select(c => new
            {
                Cadete = c,
                CantidadEnvios = cadeteria.ListadoPedidos.Count(p => p.Cadete != null && p.Cadete.Id == c.Id),
                MontoGanado = cadeteria.JornalACobrar(c.Id)
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
