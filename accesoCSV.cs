using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EspacioCadeteria;

public class accesoCSV : AccesoDatos
{

    public override Cadeteria Cargar(string archivoCsvCadeteria, string archivoCsvCadete, string archivoPedidos)
    {
        Cadeteria cadeteria = null;
        List<Cadete> listaCadetes = new List<Cadete>();
        List<Pedidos> listaPedidos = new List<Pedidos>();

        // Cargar datos de la cadetería
        string[] lineasCadeteria = File.ReadAllLines(archivoCsvCadeteria);
        string[] encabezadosCadeteria = lineasCadeteria[0].Split(',');
        string[] datosCadeteria = lineasCadeteria[1].Split(',');
        cadeteria = new Cadeteria(datosCadeteria[0], datosCadeteria[1]);

        // Cargar datos de los cadetes
        string[] lineasCadete = File.ReadAllLines(archivoCsvCadete);
        foreach (string linea in lineasCadete.Skip(1)) // saltar encabezado
        {
            string[] campos = linea.Split(',');
            Cadete cadete = new Cadete(
                int.Parse(campos[0]),
                campos[1],
                campos[2],
                campos[3]
            );
            listaCadetes.Add(cadete);
        }
        foreach (Cadete cadete in listaCadetes)
        {
            cadeteria.AgregarCadete(cadete);
        }

        // Cargar datos de los pedidos
        if (File.Exists(archivoPedidos))
        {
            string[] lineasPedidos = File.ReadAllLines(archivoPedidos);
            foreach (string linea in lineasPedidos.Skip(1)) // saltar encabezado
            {
                string[] campos = linea.Split(',');
                int nroPedido = int.Parse(campos[0]);
                string observaciones = campos[1];
                Cliente cliente = new Cliente(campos[2], campos[3], campos[4], campos[5]);
                Estado estado = (Estado)Enum.Parse(typeof(Estado), campos[6]);

                Pedidos pedido = new Pedidos(nroPedido, observaciones, cliente, estado);
                listaPedidos.Add(pedido);
            }
        }

        foreach (Pedidos pedido in listaPedidos)
        {
            cadeteria.ListadoPedidos.Add(pedido);
        }

        return cadeteria;
    }
    public override void Guardar(List<Pedidos> pedidos, string archivoPedidos)
    {
        using (var writer = new StreamWriter(archivoPedidos))
        {
            writer.WriteLine("Numero,Observaciones,NombreCliente,DireccionCliente,TelefonoCliente,Referencia,Estado");
            foreach (var pedido in pedidos)
            {
                writer.WriteLine($"{pedido.Nro},{pedido.Obs},{pedido.Cliente.Nombre},{pedido.Cliente.Direccion},{pedido.Cliente.Telefono},{pedido.Cliente.DatosReferenciaDireccion},{pedido.Estado}");
            }
        }
    }
}