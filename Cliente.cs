using System;

namespace EspacioCadeteria;
public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        this.DatosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Nombre { get => nombre; private set => nombre = value; }
    public string Direccion { get => direccion; private set => direccion = value; }
    public string Telefono { get => telefono; private set => telefono = value; }
    public string DatosReferenciaDireccion
    {
        get => datosReferenciaDireccion;
        private set => datosReferenciaDireccion = value;
    }
    public string VerDireccionCliente()
    {
        return Direccion;
    }

    public string VerDatosCliente()
    {
        return $"Nombre: {Nombre}, Dirección: {Direccion}, Teléfono: {Telefono}, Datos de referencia: {DatosReferenciaDireccion}";
    }
}
