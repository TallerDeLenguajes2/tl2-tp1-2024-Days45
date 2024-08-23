using System;

namespace EspacioCadeteria;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete>listadoCadetes;
    public Cadeteria(string nombre, string telefono)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
        this.ListadoCadetes = new List<Cadete>();
    }

    public string Nombre { get => nombre; private set => nombre = value; }
    public string Telefono { get => telefono; private set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; private set => listadoCadetes = value; }
}
