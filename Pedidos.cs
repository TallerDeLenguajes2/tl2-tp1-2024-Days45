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
    private Cadete cadete;

    public Pedidos(int nro, string obs, Cliente cliente, Estado estado)
    {
        this.nro = nro;
        this.obs = obs;
        this.Cliente = cliente;
        this.Estado = estado;
        this.Cadete = null;
    }

    public int Nro { get => nro; private set => nro = value; }
    public string Obs { get => obs; private set => obs = value; }
    public Cliente Cliente { get => cliente; private set => cliente = value; }
    public Estado Estado { get => estado; private set => estado = value; }
    public Cadete Cadete { get => cadete; private set => cadete = value; }

    public void CambiarEstado(Estado nuevoEstado)
    {
        this.Estado = nuevoEstado;
    }
    public void asignarCadete(Cadete cadete)
    {
        this.Cadete = cadete;
        this.Estado = Estado.EnProceso;
    }

    public string mostrarPedido()
    {
        return $"Nro: {nro}\nObs: {obs}\nCliente: {cliente.VerDatosCliente()}\nEstado: {estado}\n" +
               (cadete != null ? $"Cadete asignado: {cadete.Nombre}\n" : "Cadete no asignado.\n");
    }
}
