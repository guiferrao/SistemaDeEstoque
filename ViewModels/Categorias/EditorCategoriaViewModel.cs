using System.ComponentModel.DataAnnotations;

namespace SistemaDeEstoque.ViewModels.Categorias
{
    public class EditorCategoriaViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatorio")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 40 caracteres")]
        public string Nome { get; set; }
    }
}
