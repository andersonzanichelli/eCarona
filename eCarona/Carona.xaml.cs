using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using eCarona.Modelo;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json;

namespace eCarona
{
    public partial class Carona : PhoneApplicationPage
    {
        IList<Pessoa> objPessoas;
        public Carona()
        {
            InitializeComponent();
            ListaPessoas();
        }

        public void ListaPessoas() 
        {            

            /*List<Pessoa> objPessoa = new List<Pessoa>();
            Pessoa pessoa = new Pessoa();
            pessoa.ativo = 1;
            pessoa.nome = "Eduardo";
            pessoa.telefone = "0414499417135";
            objPessoa.Add(pessoa);
            toDoItemsListBox.ItemsSource = objPessoa;*/
            
            WebClient requisicao_pessoa = new WebClient();
            Uri url = new Uri(App.Endereco_Servidor + "/pessoa/lista/token=" + App.Token);
            requisicao_pessoa.DownloadStringCompleted += requisicao_pessoa_DownloadStringCompleted;
            requisicao_pessoa.DownloadStringAsync(url);
        }

        private void requisicao_pessoa_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result == null)
                MessageBox.Show("Erro ao acessar a sua conta. Entrar em contato com o suporte tecnico!");
            else
            {
                objPessoas = JsonConvert.DeserializeObject<IList<Pessoa>>(e.Result);
                toDoItemsListBox.ItemsSource = objPessoas;
            }
        }



        private void btnLigar_Click(object sender, RoutedEventArgs e)
        {

            var t = sender as Button;        
            Pessoa pessoa = t.DataContext as Pessoa;

            if (pessoa.telefone == null)
            {
                MessageBox.Show("Essa pessoa nao possui telefone em seu cadastro. Nao sera possivel fazer o contato!");
                return;
            }

            PhoneCallTask ligacao = new PhoneCallTask()
            {

                DisplayName = pessoa.nome,
                PhoneNumber = pessoa.telefone
            };

           ligacao.Show();
                                   
        }


        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            
                var t = sender as Button;
                if (t != null)
                {
                    Pessoa pessoa = t.DataContext as Pessoa;

                    MessageBoxResult result = MessageBox.Show("Deseja confirmar a carona com "+pessoa.nome+"?", "Confirmando!", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    { 
                        //Confirmar a carona, mandando o link/url para o servidor e ele ira gravar na tabela Carona o id da pessoa que ligou com a id da pessoa que atendeu
                        /*WebClient confirma_carona = new WebClient();
                        Uri url = new Uri(App.Endereco_Servidor + "/carona/token=" + App.Token);
                        confirma_carona.DownloadStringCompleted += requisicao_carona_DownloadStringCompleted;
                        confirma_carona.DownloadStringAsync(url);*/
                    }

                }
            
        }

        private void requisicao_carona_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result == null)
                MessageBox.Show("Erro ao acessar a sua conta. Entrar em contato com o suporte tecnico!");
            else
            {
                objPessoas = JsonConvert.DeserializeObject<IList<Pessoa>>(e.Result);
                toDoItemsListBox.ItemsSource = objPessoas;
            }
        }
    }
}