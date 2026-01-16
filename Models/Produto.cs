using System.Text.Json.Serialization;

namespace SistemaDeEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
