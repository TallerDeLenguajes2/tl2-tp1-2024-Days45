using System;
using System.Collections.Generic;
namespace EspacioCadeteria;

public abstract class AccesoDatos
{
    public abstract Cadeteria Cargar(string archivoCadeteria, string archivoCadete, string archivoPedidos);
    public abstract void Guardar(List<Pedidos> pedidos, string archivoPedidos);
}
