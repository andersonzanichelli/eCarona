using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;
using Microsoft.Phone.Data.Linq.Mapping;


namespace eCarona
{
    class DataBaseContext: DataContext
    {
        public static string ConnectionString = "Data Source=isostore:/Configuracoes.sdf";

        private Table<Configuracao> configuracoes;

        public Table<Configuracao> Configuracoes
        {
            get
            {
                if (configuracoes == null)

                    configuracoes = GetTable<Configuracao>();

                return configuracoes;
            }
        }

        public DataBaseContext(string connectionString)
            : base(connectionString)
        {
            if (!this.DatabaseExists())
                this.CreateDatabase();
        }

    }
}
