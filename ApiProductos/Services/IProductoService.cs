namespace ApiProductos.Services
{
    using ApiProductos.Models;

    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetProductos();
        Task<ProductoDTO> GetProductoById(int id);
        Task<ProductoDTO> CreateProducto(ProductoDTO productoDTO);
        Task<bool> UpdateProducto(int id, ProductoDTO productoDTO);
        Task<bool> DeleteProducto(int id);
    }

}
