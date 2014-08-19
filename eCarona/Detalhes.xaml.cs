using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using eCarona.Resources;
using eCarona.Modelo;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;


namespace eCarona
{
    public partial class Detalhes : PhoneApplicationPage
    {
        Pessoa _pessoa = new Pessoa();

        public Detalhes()
        {
            InitializeComponent();            
        }

        private void CarregaPessoa()
        {
            if (App.pessoa != null)                
                _pessoa = App.pessoa;
            ContentPanel.DataContext = _pessoa;
        }

      

        private void percursoRad_A_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            arruma_objetos_horarios();
        }

        private void arruma_objetos_horarios()
        {
            if (percursoRad_A.IsChecked == true) //Somente ida
            {
                tblHorarioIda.Visibility = System.Windows.Visibility.Visible;
                tblHorarioIda.Visibility = System.Windows.Visibility.Visible;
                tbHorarioVolta.Visibility = System.Windows.Visibility.Collapsed;
                tblHorarioVolta.Visibility = System.Windows.Visibility.Collapsed;
                tblHorarioIda.Margin = new Thickness(10, 959, 0, 0);
                tbHorarioIda.Margin = new Thickness(-2, 986, 0, 0);
            }
            else
            {
                if (percursoRad_B.IsChecked == true) //Somente volta
                {
                    tblHorarioIda.Visibility = System.Windows.Visibility.Collapsed;
                    tblHorarioIda.Visibility = System.Windows.Visibility.Collapsed;
                    tbHorarioVolta.Visibility = System.Windows.Visibility.Visible;
                    tblHorarioVolta.Visibility = System.Windows.Visibility.Visible;
                    tblHorarioVolta.Margin = new Thickness(10, 959, 0, 0);
                    tbHorarioVolta.Margin = new Thickness(-2, 986, 0, 0);
                }
                else //Ida e Volta
                {
                    tblHorarioIda.Visibility = System.Windows.Visibility.Visible;
                    tblHorarioIda.Visibility = System.Windows.Visibility.Visible;
                    tbHorarioVolta.Visibility = System.Windows.Visibility.Visible;
                    tblHorarioVolta.Visibility = System.Windows.Visibility.Visible;
                    tblHorarioIda.Margin = new Thickness(10, 959, 0, 0);
                    tbHorarioIda.Margin = new Thickness(-2, 986, 0, 0);
                    tblHorarioVolta.Margin = new Thickness(171, 959, 0, 0);
                    tbHorarioVolta.Margin = new Thickness(160, 986, 0, 0);
                }
            }
        }


        private void percursoRad_B_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            arruma_objetos_horarios();
        }

        private void percursoRad_C_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            arruma_objetos_horarios();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            arruma_objetos_horarios();
            CarregaPessoa();
        }

        private void tbVagas_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.PlatformKeyCode == 190) //Ponto
                e.Handled = true; //Ignora o valor digitado
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //Se ja estiver cadastrado e cancelar por nao querer alterar mais o cadastro, abrir a lista
            if (!String.IsNullOrEmpty(_pessoa.nome))
                NavigationService.Navigate(new Uri("/Lista.xaml", UriKind.Relative));
            else
            {
                MessageBox.Show("Seu cadastro nao foi efetivado, o aplicativo sera fechado.");
                //Se cancelou o cadastro, fechar o aplicativo e avisar o usuario
                NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
            }
            
        }

        private async void btnSalvar_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(_pessoa.nome))
            {
                MessageBox.Show("O campo nome nao pode ser vazio.");
                return;
            }

            if (String.IsNullOrEmpty(_pessoa.bairro))
            {
                MessageBox.Show("O campo bairro nao pode ser vazio.");
                return;
            }

            if (String.IsNullOrEmpty(_pessoa.cidade))
            {
                MessageBox.Show("O campo cidade nao pode ser vazio.");
                return;
            }

            if (String.IsNullOrEmpty(_pessoa.dataInicial))
            {
                MessageBox.Show("O periodo nao foi preenchido corretamente.");
                return;
            }

            if (String.IsNullOrEmpty(_pessoa.dataFinal))
            {
                MessageBox.Show("O periodo nao foi preenchido corretamente.");
                return;
            }

            if (String.IsNullOrEmpty(_pessoa.telefone))
            {
                MessageBox.Show("O campo telefone nao pode ser vazio.");
                return;
            } 

            try
            {
                var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("nome", _pessoa.nome),
                    new KeyValuePair<string, string>("bairro", _pessoa.bairro),
                    new KeyValuePair<string, string>("cidade", _pessoa.cidade),
                    new KeyValuePair<string, string>("curtidas", _pessoa.curtidas.ToString()),
                    new KeyValuePair<string, string>("dataInicial", _pessoa.dataInicial),
                    new KeyValuePair<string, string>("dataFinal", _pessoa.dataFinal),
                    new KeyValuePair<string, string>("horarioIda", _pessoa.horarioIda),
                    new KeyValuePair<string, string>("horarioVolta", _pessoa.horarioVolta),
                    new KeyValuePair<string, string>("tipoCarona", _pessoa.tipoCarona.ToString()),
                    new KeyValuePair<string, string>("tipoServico", _pessoa.tipoServico.ToString()),
                    new KeyValuePair<string, string>("telefone", _pessoa.telefone)
                };

                var encode = new FormUrlEncodedContent(values);

                var client = new HttpClient();

                client.BaseAddress = new Uri(App.Endereco_Servidor);

                client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

                var response = await client.PostAsync("/eCarona/Pessoa", encode);
                response.EnsureSuccessStatusCode();

                MessageBox.Show("Seu cadastro foi salvo com sucesso!");
                NavigationService.Navigate(new Uri("/Lista.xaml", UriKind.Relative));

                //var pessoa = JsonConvert.DeserializeObject<Pessoa>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar cadastrar uma Pessoa" + ex.Message, "Atenção", MessageBoxButton.OKCancel);
            }
        }

    }
}