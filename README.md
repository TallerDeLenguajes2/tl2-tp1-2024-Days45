# Respuestas

## ¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?
- **Composición**: La relación entre `Cliente` y `Pedido` es una composición porque si se elimina el Cliente, el Pedido también debe eliminarse.
- **Agregación**: La relación entre `Cadete` y `Pedido` es una agregación porque un Pedido puede ser reasignado a otro Cadete, y los Cadetes pueden existir independientemente de un Pedido específico.

## ¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?

### Cadetería
- `AsignarPedido(Cadete cadete, Pedidos pedido)`: Asigna un pedido a un cadete.
- `ReasignarPedido(Cadete anterior, Cadete nuevo, Pedidos pedido)`: Reasigna un pedido de un cadete a otro.
- `AgregarCadete(Cadete cadete)`: Añade un nuevo cadete a la cadetería.
- `EliminarCadete(Cadete cadete)`: Elimina un cadete de la cadetería.
- `EliminarPedido(Pedidos pedido)`: Elimina un pedido y el cliente asociado.

### Cadete
- `ListarPedidos()`: Muestra los pedidos asignados al cadete.
- `AgregarPedido(Pedidos pedido)`: Añade un pedido a la lista de pedidos del cadete.
- `EliminarPedido(Pedidos pedido)`: Elimina un pedido de la lista de pedidos del cadete.
- `CambiarEstadoPedido`: este método encuentra un pedido específico en una lista por su número y cambia su estado si el pedido existe
## Teniendo en cuenta los principios de abstracción y ocultamiento, ¿qué atributos, propiedades y métodos deberían ser públicos y cuáles privados?
-`Atributos`: Todos privados para mantener los detalles internos a salvo y controlar cómo se accede a ellos.
-`Métodos`: Todos públicos para permitir que otras partes del código interactúen con la clase y usen sus funcionalidades.

## ¿Cómo diseñaría los constructores de cada una de las clases?
Cadeteria:
public Cadeteria(string nombre, string telefono)
{
this.Nombre = nombre;
this.Telefono = telefono;
this.ListadoCadetes = new List<Cadete>();
}
Cliente:
public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
{
this.Nombre = nombre;
this.Direccion = direccion;
this.Telefono = telefono;
this.DatosReferenciaDireccion = datosReferenciaDireccion;
}
public Cadete(int id, string nombre, string direccion, string telefono)
{
this.Id = id;
this.Nombre = nombre;
this.Direccion = direccion;
this.Telefono=telefono;
this.ListaPedidos = new List<Pedidos>();
}
Pedido:
public Pedidos(int nro, string obs, Cliente cliente, Estado estado)
{
this.nro = nro;
this.obs = obs;
this.Cliente = cliente;
this.Estado = estado;
}

## ¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?
