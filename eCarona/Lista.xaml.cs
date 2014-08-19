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
    public partial class Lista : PhoneApplicationPage
    {
        IList<Pessoa> objPessoas;
        public Lista()
        {
            InitializeComponent();
        }

        private void btnCaronasAgendadas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Carona.xaml", UriKind.Relative));
        }

        private void btnAgendarCaronas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Listagem.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Deseja sair do aplicativo eCarona?", "Saindo!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                if (NavigationService.CanGoBack == true)
                {
                    while (NavigationService.RemoveBackEntry() != null)
                    {
                        NavigationService.RemoveBackEntry();
                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
           
        }

        private void btnAlterarCadastro_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Cadastro.xaml", UriKind.Relative));
        }
    }
}