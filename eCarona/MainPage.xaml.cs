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

namespace eCarona
{
    public partial class MainPage : PhoneApplicationPage
    {
        Configuracao configuracao = null;
        public MainPage()
        {
            InitializeComponent();
            //Loaded += MainPage_Loaded;
        }

       /* void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }*/

        private void btnAvancar_Click(object sender, RoutedEventArgs e)
        {
            String token = tbToken.Text;
            if (String.IsNullOrEmpty(token))
            {
                MessageBox.Show("Voce nao pode prosseguir com o cadastro sem preencher o token. Esse token eh repassado pela escola, entre em contato com ela.");
                return;
            }
            else
            {
                configuracao = new Configuracao 
                {
                    Endereco_servidor = "https://ecarona-c9-ednerzuconelli.c9.io/eCarona",
                    Token = App.Token,
                };

                configuracao.Gravar();
                App.Endereco_Servidor = "https://ecarona-c9-ednerzuconelli.c9.io/eCarona";
            }

            NavigationService.Navigate(new Uri("/Cadastro.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NavigationService.CanGoBack == true)
            {
                while (NavigationService.RemoveBackEntry() != null)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            tbToken.IsReadOnly = true;
            if (configuracao == null)
            {
                App.Token = DateTime.Now.Ticks.ToString();
                tbToken.Text = App.Token;
            }
        }


    }
}