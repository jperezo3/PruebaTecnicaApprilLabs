namespace ApiProductos.Controllers
{
    using ApiProductos.Data;
    using ApiProductos.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  obtener todos los productos (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();

            var productosDTO = productos.Select(p => new ProductoDTO
            {
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio,
                Disponible = p.Disponible
            });

            return Ok(productosDTO); //  200 si el producto existe
        }

        //  obtener un producto por ID (GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (id == 0)
            {
                Console.WriteLine("ID inválido.");
                throw new Exception("error para probar el middleware.");
            }
            if (producto == null)
            {
                throw new KeyNotFoundException($"El producto con ID {id} no existe."); // manejo de exepciones por medio del middleware
            }

            var productoDTO = new ProductoDTO
            {
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Disponible = producto.Disponible
            };

            return Ok(productoDTO); // 200 si el producto existe
        }

        //  crear un nuevo producto (POST)
        [HttpPost]
        public async Task<ActionResult<Producto>> CrearProducto(ProductoDTO productoDTO)
        {
            var producto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Categoria = productoDTO.Categoria,
                Precio = productoDTO.Precio,
                Disponible = productoDTO.Disponible
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        //  actualizar un producto por ID (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ProductoDTO productoDTO)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound(); // 404 si no existe el producto

            producto.Nombre = productoDTO.Nombre;
            producto.Categoria = productoDTO.Categoria;
            producto.Precio = productoDTO.Precio;
            producto.Disponible = productoDTO.Disponible;

            await _context.SaveChangesAsync();
            return NoContent(); // 204 si la actualización fue exitosa
        }

        //  eliminar un producto por ID (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound(); // 404 si no existe el producto

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 si la actualización fue exitosa
        }
    }

}
