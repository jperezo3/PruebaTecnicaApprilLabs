using ApiProductos.Models;
using ApiProductos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiProductos.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _productoService.GetProductos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoService.GetProductoById(id);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> CrearProducto(ProductoDTO productoDTO)
        {
            var productoCreado = await _productoService.CreateProducto(productoDTO);
            return CreatedAtAction(nameof(GetProducto), new { id = productoCreado.Nombre }, productoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ProductoDTO productoDTO)
        {
            var actualizado = await _productoService.UpdateProducto(id, productoDTO);
            if (!actualizado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoService.DeleteProducto(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }
    }

}
