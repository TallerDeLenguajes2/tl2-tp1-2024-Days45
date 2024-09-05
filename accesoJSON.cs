using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EspacioCadeteria;

public class accesoJSON : AccesoDatos
{
    public override Cadeteria Cargar(string archivoCadeteria, string archivoCadetes)
    {
        Cadeteria cadeteria = null;
        List<Cadete> listaCadetes = new List<Cadete>();
        if (File.Exists(archivoCadeteria))
        {
            string jsonCadeteria = File.ReadAllText(archivoCadeteria);
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var datosCadeteria = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonCadeteria, opciones);
            cadeteria = new Cadeteria(datosCadeteria["nombre"], datosCadeteria["telefono"]);
        }

        if (File.Exists(archivoCadetes))
        {
            string jsonCadetes = File.ReadAllText(archivoCadetes);
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            listaCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes, opciones);
        }

        if (cadeteria != null)
        {
            foreach (Cadete cadete in listaCadetes)
            {
                cadeteria.AgregarCadete(cadete);
            }
        }
        return cadeteria;
    }
}
