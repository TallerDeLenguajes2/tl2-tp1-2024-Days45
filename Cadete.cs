using System;

namespace EspacioCadeteria;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private List<Pedidos> listaPedidos;

    public Cadete(int id, string nombre, string direccion)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.listaPedidos = new List<Pedidos>();
    }
}
