using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}
	 

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_qtde.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso", "Produto inserido com sucesso", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("EPA!", ex.Message, "OK");
        }
    }
}