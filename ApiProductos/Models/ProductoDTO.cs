namespace ApiProductos.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public string Categoria { get; set; }

        [Range(0.01, 10000, ErrorMessage = "El precio debe estar entre 0.01 y 10,000.")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }
    }

}
