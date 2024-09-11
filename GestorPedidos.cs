using System;
using System.Linq;

namespace EspacioCadeteria
{
    public class GestorPedidos
    {
        private Cadeteria cadeteria;
        private AccesoDatos accesoDatos;
        private string rutaPedidos;

        public GestorPedidos(Cadeteria cadeteria, AccesoDatos accesoDatos, string rutaPedidos)
        {
            this.cadeteria = cadeteria;
            this.accesoDatos = accesoDatos;
            this.rutaPedidos = rutaPedidos;
        }

        public void DarDeAltaPedido()
        {
            Console.WriteLine("Pedidos anteriores:");
            Console.WriteLine(cadeteria.MostrarPedidos());
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
            cadeteria.agregarPedido(nro, obs, nombreCliente, direccionCliente, telefonoCliente, datosReferencia);
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);
            Console.WriteLine("Pedido creado y guardado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        public void AsignarPedido()
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

            int nroPedido;
            while (true)
            {
                Console.Write("Ingrese el número del pedido a asignar: ");
                if (int.TryParse(Console.ReadLine(), out nroPedido) && cadeteria.ListadoPedidos.Any(p => p.Nro == nroPedido && p.Cadete == null))
                {
                    break;
                }
                Console.WriteLine("Número de pedido inválido. Intente nuevamente.");
            }

            Console.WriteLine("Cadetes disponibles:");
            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                Console.WriteLine($"{cadete.Id}: {cadete.Nombre}");
            }

            int idCadete;
            while (true)
            {
                Console.Write("Ingrese el ID del cadete al que desea asignar el pedido: ");
                if (int.TryParse(Console.ReadLine(), out idCadete) && cadeteria.ListadoCadetes.Any(c => c.Id == idCadete))
                {
                    break;
                }
                Console.WriteLine("ID de cadete inválido. Intente nuevamente.");
            }

            cadeteria.asignarCadetePedido(idCadete, nroPedido);
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);
            Console.WriteLine("Pedido asignado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        public void CambiarEstadoPedido()
        {
            Console.WriteLine("Pedidos registrados:");
            Console.WriteLine(cadeteria.MostrarPedidos());
            Console.WriteLine("Cambiar estado de un pedido");

            int nroPedido;
            while (true)
            {
                Console.Write("Ingrese el número del pedido: ");
                if (int.TryParse(Console.ReadLine(), out nroPedido) && cadeteria.ListadoPedidos.Any(p => p.Nro == nroPedido))
                {
                    break;
                }
                Console.WriteLine("Número de pedido inválido. Intente nuevamente.");
            }

            var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
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

            Estado nuevoEstado;
            while (true)
            {
                Console.Write("Ingrese el nuevo estado: ");
                if (Enum.TryParse(Console.ReadLine(), out nuevoEstado) && Enum.IsDefined(typeof(Estado), nuevoEstado))
                {
                    break;
                }
                Console.WriteLine("Estado inválido. Intente nuevamente.");
            }

            pedido.CambiarEstado(nuevoEstado);

            if (nuevoEstado == Estado.Cancelado)
            {
                cadeteria.eliminarPedido(nroPedido);
            }

            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);
            Console.WriteLine("Estado del pedido cambiado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        public void ReasignarPedido()
        {
            Console.WriteLine("Reasignar un pedido a otro cadete");

            Console.WriteLine(cadeteria.MostrarPedidos());
            int nroPedido;
            while (true)
            {
                Console.Write("Ingrese el número del pedido a reasignar: ");
                if (int.TryParse(Console.ReadLine(), out nroPedido) && cadeteria.ListadoPedidos.Any(p => p.Nro == nroPedido))
                {
                    break;
                }
                Console.WriteLine("Número de pedido inválido. Intente nuevamente.");
            }

            var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
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

            int idNuevoCadete;
            while (true)
            {
                Console.Write("Ingrese el ID del nuevo cadete: ");
                if (int.TryParse(Console.ReadLine(), out idNuevoCadete) && cadeteria.ListadoCadetes.Any(c => c.Id == idNuevoCadete))
                {
                    break;
                }
                Console.WriteLine("ID de cadete inválido. Intente nuevamente.");
            }

            cadeteria.asignarCadetePedido(idNuevoCadete, nroPedido);
            accesoDatos.Guardar(cadeteria.ListadoPedidos, rutaPedidos);
            Console.WriteLine("Pedido reasignado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }

        public void MostrarInforme()
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
