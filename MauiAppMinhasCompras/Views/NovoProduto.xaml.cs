using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async Task ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto produto = new Produto
			{
				Descricao = txt_desc.Text,

				Quantidade = Convert.ToInt32(txt_quantidade.Text),

				Preco = Convert.ToDouble(txt_preco.Text),
			};

			await App._Db.Insert(produto);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
		}
		catch (Exception ex)
		{

			DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}