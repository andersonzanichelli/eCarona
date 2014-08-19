using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCarona.Modelo
{
    public class UF
    {
        public string sigla { get; set; }
        public string nome { get; set; }
    }

    public class Pessoa
    {
        public int ativo { get; set; }
        public string horarioVolta { get; set; }
        public string horarioIda { get; set; }
        public int vagasPendentes { get; set; }
        public string dataFinal { get; set; }
        public string dataInicial { get; set; }
        public string bairro { get; set; }
        public int tipoServico { get; set; }
        public string endereco { get; set; }
        public int curtidas { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public int tipoCarona
        {
            get;
            set;
        }

        public bool rad_TipoCaronaOfereco 
        {
            get
            {
                switch (tipoCarona)
                {
                    case 0:
                        return true;
                    default:
                        return false;
                }
            }

        }

        public bool rad_TipoCaronaSolicito
        {
            get
            {
                switch (tipoCarona)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool rad_ServicoIda
        {
            get
            {
                switch (tipoServico)
                {
                    case 0:
                        return true;               
                    default:
                        return false;

                }
            }
        }

        public bool rad_ServicoVolta
        {
            get
            {
                switch (tipoServico)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool rad_ServicoIdaVolta
        {
            get
            {
                switch (tipoServico)
                {
                    case 2:
                        return true;
                    default:
                        return false;
                }
            }
        }

    }


    public class Satisfacao
    {
        public string data { get; set; }
        public int curtir { get; set; }
    }

    public class Carona
    {
        public int idMotorista { get; set; }
        public int idCarona { get; set; }
    }



    
}
