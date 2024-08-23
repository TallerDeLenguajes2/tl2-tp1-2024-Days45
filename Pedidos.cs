using System;

namespace EspacioCadeteria;
public enum Estado{
    Pendiente,
    EnProceso,
    Entregado,
    Cancelado
}
public class Pedidos
{
    private int Nro;
    private string Obs;
    private Cliente cliente;
    private Estado estado;

    public Pedidos(int nro, string obs, Cliente cliente, Estado estado)
    {
        Nro1 = nro;
        Obs1 = obs;
        this.Cliente = cliente;
        this.Estado = estado;
    }

    public int Nro1 { get => Nro; private set => Nro = value; }
    public string Obs1 { get => Obs; private set => Obs = value; }
    public Cliente Cliente { get => cliente; private set => cliente = value; }
    public Estado Estado { get => estado; private set => estado = value; }
}
