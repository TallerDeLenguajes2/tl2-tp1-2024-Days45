using System;

namespace EspacioCadeteria;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
    }

    public int Id
    {
        get => id;
        private set => id = value;
    }
    public string Nombre
    {
        get => nombre;
        private set => nombre = value;
    }
    public string Direccion
    {
        get => direccion;
        private set => direccion = value;
    }
    public string Telefono
    {
        get => telefono;
        private set => telefono = value;
    }


}
