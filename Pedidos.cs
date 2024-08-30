using System;

namespace EspacioCadeteria;
public enum Estado
{
    Pendiente,
    EnProceso,
    Entregado,
    Cancelado
}
public class Pedidos
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estado;

    public Pedidos(int nro, string obs, Cliente cliente, Estado estado)
    {
        this.nro = nro;
        this.obs = obs;
        this.Cliente = cliente;
        this.Estado = estado;
    }

    public int Nro { get => nro; private set => nro = value; }
    public string Obs { get => obs; private set => obs = value; }
    public Cliente Cliente { get => cliente; private set => cliente = value; }
    public Estado Estado { get => estado; private set => estado = value; }
    public void CambiarEstado(Estado nuevoEstado)
    {
        this.Estado = nuevoEstado;
    }

    public void mostrarPedido()
    {
        Console.WriteLine($"Nro: {nro}");
        Console.WriteLine($"Obs: {obs}");
        Console.WriteLine($"Cliente: {cliente.VerDatosCliente()}");
        Console.WriteLine($"Estado: {estado}");
    }
}
