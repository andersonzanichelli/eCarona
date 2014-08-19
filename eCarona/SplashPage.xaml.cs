using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Newtonsoft.Json;
using eCarona.Modelo;
using System.Net.NetworkInformation;

namespace eCarona
{
    public partial class SplashPage : PhoneApplicationPage
    {
        private HttpWebRequest request;
        public static bool atualiza_cadastro;

        public SplashPage()
        {
            InitializeComponent();
            Loaded += SplashPage_Loaded;

            
       
        }

        void SplashPage_Loaded(object sender, RoutedEventArgs e)
        {

            //Se nao estiver conectado a internet, avisar ao usuario e abortar o aplicativo
            bool conectado = ConectadoInternet();
            if (conectado == false)
            {
                MessageBox.Show("Seu aparelho nao esta conectado a internet. O aplicativo eCarona nao pode ser iniciado.");
                while (NavigationService.RemoveBackEntry() != null)
                {
                     NavigationService.RemoveBackEntry();
                 }               
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://www.developer.nokia.com/Community/Wiki/Portal:Windows_Phone_UI_Articles"));
                request.BeginGetResponse(new AsyncCallback(ReceiveResponseCallBack), null);


                bool configurado = VerificarConfiguracao();

                if (configurado == true)
                {
                    //Passar o token da pessoa numa url, pegar o jason e montar a lista
                    WebClient requisicao_pessoa = new WebClient();
                    Uri url = new Uri(App.Endereco_Servidor + "/pessoa/token=" + App.Token);
                    requisicao_pessoa.DownloadStringCompleted += requisicao_pessoa_DownloadStringCompleted;
                    requisicao_pessoa.DownloadStringAsync(url);
                    MessageBoxResult resultado = MessageBox.Show("Deseja atualizar seu cadastro?", "Acessando o aplicativo!", MessageBoxButton.OKCancel);
                    if (resultado == MessageBoxResult.OK)
                        atualiza_cadastro = true;
                    else
                        atualiza_cadastro = false;
                     
                }
                else
                {
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }


            }

            
        }

        public bool ConectadoInternet()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
                return true;
            else
                return false;

        }


        private void ReceiveResponseCallBack(IAsyncResult result)
        {
            /*HttpWebResponse response = (HttpWebResponse)this.request.EndGetResponse(result);
            using (var stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string str = reader.ReadToEnd();
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {                        

                    });
                }
            }*/
        }


                        
        private void requisicao_pessoa_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Result == null)
                MessageBox.Show("Erro ao acessar a sua conta. Entrar em contato com o suporte tecnico!");
            else
            {
                App.pessoa = JsonConvert.DeserializeObject<Pessoa>(e.Result);
                if (atualiza_cadastro == true)
                     NavigationService.Navigate(new Uri("/Cadastro.xaml", UriKind.Relative));
                else
                     NavigationService.Navigate(new Uri("/Lista.xaml", UriKind.Relative));
            }
                
        }

        public bool VerificarConfiguracao()
        {
            Configuracao objConfiguracao = new Configuracao();
            Configuracao dados = objConfiguracao.ObtemConfiguracao();

            if (dados != null)
            {
                App.Endereco_Servidor = dados.Endereco_servidor;
                App.Token = dados.Token;
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}