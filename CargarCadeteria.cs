using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace EspacioCadeteria;

public class CargarCadeteria
{
    public Cadeteria Cargar(string archivoCsvCadeteria, string archivoCsvCadete)
        {
            Cadeteria cadeteria = null;
            List<Cadete> listaCadetes = new List<Cadete>();

            // Cargar datos de la cadetería
            string[] lineasCadeteria = File.ReadAllLines(archivoCsvCadeteria);
            string[] encabezados = lineasCadeteria[0].Split(',');
            string[] datos = lineasCadeteria[1].Split(',');
            cadeteria = new Cadeteria(datos[0], datos[1]);

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

            // Asignar cadetes a la cadetería
            foreach (Cadete cadete in listaCadetes)
            {
                cadeteria.AgregarCadete(cadete);
            }

            return cadeteria;
        }
}
