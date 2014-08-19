using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace eCarona
{
    [Table(Name = "Configuracao")]
    class Configuracao
    {

            private int id;

            [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private string endereco_servidor;
            [Column(Name = "Endereco_servidor", CanBeNull = false)]
            public string Endereco_servidor
            {
                get { return endereco_servidor; }
                set { endereco_servidor = value; }
            }

            private string token;
            [Column(Name = "Token", CanBeNull = false)]
            public string Token
            {
                get { return token; }
                set { token = value; }
            }

            public IEnumerable ObtemConfiguracoes()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.ObtemConfiguracoes();
            }

            public Configuracao ObtemConfiguracao()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.ObtemConfiguracao();
            }

            public bool Gravar()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.Gravar(this);
            }

            public bool Excluir()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.Excluir(this);
            }

            public Configuracao Exibir()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.Exibir(this);
            }

            public bool Realizado()
            {
                DAOConfiguracao daoConfiguracao = new DAOConfiguracao();
                return daoConfiguracao.Realizado(this);
            }

        
    }
}
