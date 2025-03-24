using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    private List<Produto> _todosProdutos = new List<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        _todosProdutos = await App.Db.GetAll();
        AtualizarLista(_todosProdutos);
    }

    private void AtualizarLista(List<Produto> produtos)
    {
        lista.Clear();
        foreach (var produto in produtos)
        {
            lista.Add(produto);
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string termoBusca = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(termoBusca))
        {
            AtualizarLista(_todosProdutos);
        }
        else
        {
            var produtosFiltrados = _todosProdutos
                .Where(p => p.Descricao.Contains(termoBusca, StringComparison.OrdinalIgnoreCase))
                .ToList();
            AtualizarLista(produtosFiltrados);
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Opa", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            
            var menuItem = sender as MenuItem;

            
            var produto = menuItem?.BindingContext as Produto;

            if (produto == null)
            {
                await DisplayAlert("Erro", "Produto não encontrado.", "OK");
                return;
            }

            
            bool confirmacao = await DisplayAlert("Confirmar Remoção", $"Deseja remover o produto '{produto.Descricao}'?", "Sim", "Não");

            if (confirmacao)
            {
            
                await App.Db.Delete(produto.Id);

            
                lista.Remove(produto);

            
                _todosProdutos.Remove(produto);

                await DisplayAlert("Sucesso", "Produto removido com sucesso.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao remover o produto: {ex.Message}", "OK");
        }
    }

}