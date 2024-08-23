using System;

namespace EspacioCadeteria;
public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string DatosReferenciaDireccion;

    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        DatosReferenciaDireccion1 = datosReferenciaDireccion;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion1 { get => DatosReferenciaDireccion; set => DatosReferenciaDireccion = value; }
}
