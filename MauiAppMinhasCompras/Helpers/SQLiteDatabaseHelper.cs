using MauiAppMinhasCompras.Models;
using SQLite;


namespace MauiAppMinhasCompras.Helpers
{
    public class SQliteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQliteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p) 
        {
            String sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }
        public Task<int> Delete(int Id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == Id);
        }
        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();

        }

        public Task<List<Produto>> Search(string q)
        {
            String sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);

        }

    }
}
