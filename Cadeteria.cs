using System;
using System.Collections.Generic;
using System.Linq;

namespace EspacioCadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private string telefono;
        private List<Cadete> listadoCadetes;
        private List<Pedidos> listadoPedidos;

        public Cadeteria(string nombre, string telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.ListadoCadetes = new List<Cadete>();
            this.ListadoPedidos = new List<Pedidos>();
        }

        public string Nombre
        {
            get => nombre;
            private set => nombre = value;
        }
        public string Telefono
        {
            get => telefono;
            private set => telefono = value;
        }
        public List<Cadete> ListadoCadetes
        {
            get => listadoCadetes;
            private set => listadoCadetes = value;
        }
        public List<Pedidos> ListadoPedidos
        {
            get => listadoPedidos;
            private set => listadoPedidos = value;
        }

        public void agregarPedido(int nroPedido, string observaciones, string nombreCliente, string direccionCliente, string telefonoCliente, string datosReferencia)
        {
            if (ListadoPedidos.Any(p => p.Nro == nroPedido))
            {
                throw new ArgumentException(); // Evita duplicados
            }

            Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);
            Pedidos nuevoPedido = new Pedidos(nroPedido, observaciones, cliente, Estado.Pendiente);
            ListadoPedidos.Add(nuevoPedido);
        }

        public void eliminarPedido(int nroPedido)
        {
            var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);
            if (pedido != null)
            {
                listadoPedidos.Remove(pedido);
            }
        }

        public void EliminarCadete(int idCadete)
        {
            var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
            if (cadete != null)
            {
                ListadoCadetes.Remove(cadete);
            }
        }

        public void AgregarCadete(int idCadete, string nombre, string direccion, string telefono)
        {
            var nuevoCadete = new Cadete(idCadete, nombre,direccion, telefono);
            ListadoCadetes.Add(nuevoCadete);
        }

        public void asignarCadetePedido(int idCadete, int nroPedido)
        {
            var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
            var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == nroPedido);

            if (cadete != null && pedido != null)
            {
                pedido.asignarCadete(cadete);
            }
        }

        public string MostrarPedidos()
        {
            if (ListadoPedidos.Count == 0)
            {
                return "No hay pedidos registrados.";
            }
            else
            {
                var resultado = string.Empty;
                foreach (var pedido in ListadoPedidos)
                {
                    resultado += pedido.mostrarPedido() + "\n";
                }
                return resultado;
            }
        }

        public double JornalACobrar(int idCadete)
        {
            var pedidosDelCadete = ListadoPedidos.Where(p =>
                p.Cadete != null && p.Cadete.Id == idCadete && p.Estado == Estado.Entregado
            );
            return pedidosDelCadete.Count() * 500;
        }
    }
}

