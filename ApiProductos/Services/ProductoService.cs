namespace ApiProductos.Services
{
    using ApiProductos.Infraestructura.Repositories;
    using ApiProductos.Models;
    //using ApiProductos.Repositories;

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDTO>> GetProductos()
        {
            var productos = await _productoRepository.GetAll();
            return productos.Select(p => new ProductoDTO
            {
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio,
                Disponible = p.Disponible
            });
        }

        public async Task<ProductoDTO> GetProductoById(int id)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null) throw new KeyNotFoundException($"El producto con ID {id} no existe.");

            return new ProductoDTO
            {
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Precio = producto.Precio,
                Disponible = producto.Disponible
            };
        }

        public async Task<ProductoDTO> CreateProducto(ProductoDTO productoDTO)
        {
            var producto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Categoria = productoDTO.Categoria,
                Precio = productoDTO.Precio,
                Disponible = productoDTO.Disponible
            };

            await _productoRepository.Add(producto);
            return productoDTO;
        }

        public async Task<bool> UpdateProducto(int id, ProductoDTO productoDTO)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null) return false;

            producto.Nombre = productoDTO.Nombre;
            producto.Categoria = productoDTO.Categoria;
            producto.Precio = productoDTO.Precio;
            producto.Disponible = productoDTO.Disponible;

            await _productoRepository.Update(producto);
            return true;
        }

        public async Task<bool> DeleteProducto(int id)
        {
            var producto = await _productoRepository.GetById(id);
            if (producto == null) return false;

            await _productoRepository.Delete(id);
            return true;
        }
    }

}
