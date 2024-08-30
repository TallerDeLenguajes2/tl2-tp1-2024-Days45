using System;
namespace EspacioCadeteria;
public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedidos> listaPedidos;
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Direccion = direccion;
        this.Telefono = telefono;
        this.ListaPedidos = new List<Pedidos>();
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
    public List<Pedidos> ListaPedidos
    {
        get => listaPedidos;
        private set => listaPedidos = value;
    }
    public void agregarPedido(Pedidos pedido)
    {
        ListaPedidos.Add(pedido);
    }

    public void eliminarPedido(Pedidos pedido)
    {
        if (listaPedidos.Contains(pedido))
        {
            listaPedidos.Remove(pedido);
        }
    }
    public void CambiarEstadoPedido(int nroPedido, Estado nuevoEstado)
    {
        var pedido = ListaPedidos.FirstOrDefault(p => p.Nro == nroPedido);
        if (pedido != null)
        {
            pedido.CambiarEstado(nuevoEstado);
        }
    }
    public void ListarPedidos()
    {
        foreach (var pedido in listaPedidos)
        {
            pedido.mostrarPedido();
        }
    }
}
