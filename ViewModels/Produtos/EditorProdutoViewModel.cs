using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeEstoque.ViewModels.Produtos
{
    public class EditorProdutoViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Valor é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O Valor deve ser maior que zero")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "A Quantidade é obrigatoria")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}
