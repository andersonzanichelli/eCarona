using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCarona
{
    class DAOConfiguracao
    {

        Configuracao config = new Configuracao();

        public IEnumerable ObtemConfiguracoes()
        {
            List<Configuracao> dados = new List<Configuracao>();
            using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
            {
                dados = (from Configuracao in db.Configuracoes orderby Configuracao.Endereco_servidor select Configuracao).ToList();
            }
            return dados;
        }

        public Configuracao ObtemConfiguracao()
        {
            List<Configuracao> dados = new List<Configuracao>();
            using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
            {
                dados = (from Configuracao in db.Configuracoes orderby Configuracao.Endereco_servidor select Configuracao).ToList();
            }
            if (dados.Count > 0 )
            {
                return dados[0];
            }
            else
            {
                return null;
            }
                
            
        }

        public bool Gravar(Configuracao novaConfiguracao)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
                {
                    db.Configuracoes.InsertOnSubmit(novaConfiguracao);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Excluir(Configuracao configuracao)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
                {
                    var excluir = db.Configuracoes.Where(t => t.Id == configuracao.Id).First();
                    db.Configuracoes.DeleteOnSubmit(excluir);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Configuracao Exibir(Configuracao configuracao)
        {
            Configuracao dado = new Configuracao();
            try
            {
                using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
                {
                    dado = db.Configuracoes.Where(t => t.Id == configuracao.Id).First();
                }
                return dado;
            }
            catch (Exception)
            {
                return dado;
            }
        }

        public bool Realizado(Configuracao configuracao)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext(DataBaseContext.ConnectionString))
                {
                    Configuracao update = (from tar in db.Configuracoes
                                    where tar.Id == configuracao.Id
                                    select tar).First();
                    //update.Realizada = !update.Realizada;
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
