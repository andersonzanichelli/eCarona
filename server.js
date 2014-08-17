var express    = require('express');
var app        = express();
var bodyParser = require('body-parser');
var mongoose   = require('mongoose');

var Pessoa     = require('./app/models/pessoa');
var TipoPessoa = require('./app/models/tipopessoa');
var Satisfacao = require('./app/models/satisfacao');
var Cidade = require('./app/models/cidade');
var Uf = require('./app/models/uf');
var Pais = require('./app/models/pais');

mongoose.connect('mongodb://localhost:27017/local');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

var port = process.env.PORT || 9000;

var router = express.Router();

// Filter
router.use(function(req, res, next) {
	console.log('Cliente fez alguma requisição.');
	next();
});

router.get('/', function(req, res) {
	res.json({ message: 'Bem-Vindo ao eCarona, seu gerenciador de caronas!' });	
});

router.route('/pais')
   .post(function(req, res) {
      var pais = new Pais();
      pais.nome = req.body.nome;
      pais.sigla = req.body.sigla;
      pais.codigo = req.body.codigo;

      pais.save(function(err) {
         if (err) {
	         res.send(err);
         }
   	 
         res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      Pais.find(function(err, pais) {
           if (err) {
	           res.send(err);
           }
         
           res.json(pais);
	     });
   });

//cidade
router.route('/cidade')
   .post(function(req, res) {
      var cidade = new Cidade();
      cidade.nome = req.body.nome;
      cidade.codigoibge = req.body.codigoibge;
    
      cidade.save(function(err) {
         if (err) {
	         res.send(err);
         }
   	 
         res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      Cidade.find(function(err, cidade) {
         if (err) {
           res.send(err);
         }
       
         res.json(cidade);
	    });
   });
//pessoa
router.route('/pessoa')
   .post(function(req, res) {
      var pessoa = new Pessoa();
      pessoa.nome = req.body.nome;
      pessoa.tipopessoa = req.body.tipopessoa;
  	  pessoa.curtidas = req.body.curtidas;
  	  pessoa.endereco = req.body.endereco;
  	  pessoa.tipoServico = req.body.tipoServico;
  	  pessoa.bairro = req.body.bairro;
  	 //cidade: Cidade,
  	  pessoa.dataInicial = req.body.dataInicial;
  	  pessoa.dataFinal = req.body.dataFinal;
  	  pessoa.vagasPendentes = req.body.vagasPendentes;
  	  pessoa.tipoCarona = req.body.tipoCarona;
  	  pessoa.horarioIda = req.body.horarioIda;
  	  pessoa.horarioVolta = req.body.horarioVolta;
  	  pessoa.ativo = req.body.ativo;
	  
      pessoa.save(function(err) {
         if (err) {
	         res.send(err);
         }
   	 
         res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      Pessoa.find(function(err, pessoa) {
         if (err) {
           res.send(err);
         }
       
         res.json(pessoa);
	    });
   });

//satisfacao
router.route('/satisfacao')
   .post(function(req, res) {
      var satisfacao = new Satisfacao();
      satisfacao.curtir = req.body.curtir;
      satisfacao.data = req.body.data;
    
      satisfacao.save(function(err) {
        if (err) {
          res.send(err);
        }
   	 
        res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      Satisfacao.find(function(err, satisfacao) {
        if (err) {
          res.send(err);
        }
         
        res.json(satisfacao);
	   });
   });

//tipopessoa
router.route('/tipopessoa')
   .post(function(req, res) {
      var tipopessoa = new TipoPessoa();
      tipopessoa.descricao = req.body.descricao;
      tipopessoa.ativo = req.body.ativo;
    
      tipopessoa.save(function(err) {
        if (err) {
          res.send(err);
        }
   	 
        res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      TipoPessoa.find(function(err, tipopessoa) {
        if (err) {
          res.send(err);
        }
         
        res.json(tipopessoa);
      });
   });

//estado
router.route('/uf')
   .post(function(req, res) {
      var uf = new Uf();
      uf.nome = req.body.nome;
      uf.sigla = req.body.sigla;
    
      uf.save(function(err) {
        if (err) {
          res.send(err);
        }

        res.json({ message: 'Gravado com sucesso!' });
      });		
   })
   .get(function(req, res) {
      Uf.find(function(err, uf) {
        if (err) {
          res.send(err);
        }

        res.json(uf);
      });
   });

app.use('/eCarona', router);

app.listen(port);
console.log('Server online on port ' + port);
