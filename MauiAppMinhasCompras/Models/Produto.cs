using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Dedescricao { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
    }
}
