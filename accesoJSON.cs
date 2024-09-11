using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspacioCadeteria
{
    public class accesoJSON : AccesoDatos
    {
        public override Cadeteria Cargar(string archivoCadeteria, string archivoCadete, string archivoPedidos)
        {
            Cadeteria cadeteria = null;
            List<Cadete> listaCadetes = new List<Cadete>();
            List<Pedidos> listaPedidos = new List<Pedidos>();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }  // Para manejar los enum como cadenas
            };

            if (File.Exists(archivoCadeteria))
            {
                string jsonCadeteria = File.ReadAllText(archivoCadeteria);
                var datosCadeteria = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonCadeteria, opciones);
                cadeteria = new Cadeteria(datosCadeteria["nombre"], datosCadeteria["telefono"]);
            }

            if (File.Exists(archivoCadete))
            {
                string jsonCadetes = File.ReadAllText(archivoCadete);
                listaCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes, opciones);
            }

            if (File.Exists(archivoPedidos))
            {
                string jsonPedidos = File.ReadAllText(archivoPedidos);
                listaPedidos = JsonSerializer.Deserialize<List<Pedidos>>(jsonPedidos, opciones);
            }

            if (cadeteria != null)
            {
                foreach (Cadete cadete in listaCadetes)
                {
                    cadeteria.AgregarCadete(cadete.Id, cadete.Nombre, cadete.Direccion, cadete.Telefono);
                }

                foreach (Pedidos pedido in listaPedidos)
                {
                    cadeteria.ListadoPedidos.Add(pedido);
                }
            }

            return cadeteria;
        }

        public override void Guardar(List<Pedidos> pedidos, string archivoPedidos)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string jsonPedidos = JsonSerializer.Serialize(pedidos, opciones);
            File.WriteAllText(archivoPedidos, jsonPedidos);
        }
    }
}
