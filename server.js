var express    = require('express');
var app        = express();
var bodyParser = require('body-parser');
var mongoose   = require('mongoose');

var Pessoa     = require('./app/models/pessoa');
var Satisfacao = require('./app/models/satisfacao');
var Uf = require('./app/models/uf');

mongoose.connect('mongodb://localhost:27017/eCarona');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

var port = process.env.PORT || 9000;

var router = express.Router();

// Filter
router.use(function(req, res, next) {
	console.log('Cliente fez alguma requisição');
	next();
});

router.get('/', function(req, res) {
	res.json({ message: 'Bem-Vindo ao eCarona, seu gerenciador de caronas!' });	
});

router.route('/ok')
  .get(function(req, res){
      res.send("server online");
  });

//pessoa
router.route('/pessoa')
   .post(function(req, res) {
      var pessoa = new Pessoa();
      pessoa.token = req.body.token;
      pessoa.nome = req.body.nome;
      pessoa.curtidas = req.body.curtidas;
      pessoa.endereco = req.body.endereco;
      pessoa.tipoServico = req.body.tipoServico;
      pessoa.bairro = req.body.bairro;
      pessoa.cidade = req.body.cidade;
      pessoa.uf = req.body.uf;
      pessoa.dataInicial = req.body.dataInicial;
      pessoa.dataFinal = req.body.dataFinal;
      pessoa.vagasPendentes = req.body.vagasPendentes;
      pessoa.tipoCarona = req.body.tipoCarona;
      pessoa.horarioIda = req.body.horarioIda;
      pessoa.horarioVolta = req.body.horarioVolta;
      pessoa.ativo = req.body.ativo;
      pessoa.telefone = req.body.telefone;
	  
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

router.route('/pessoa/lista/:token')
   .get(function(req, res) {
      var query = Pessoa.findOne({ 'token': req.query.token });
      query.select('token nome curtidas endereco telefone tipoServico bairro cidade uf dataInicial dataFinal vagasPendentes tipoCarona horarioIda horarioVolta ativo');
      query.exec(function (err, pessoa) {
        if(err) {
          res.send(err)
        }

        Pessoa.find(function(err, pessoas) {
         if (err) {
           res.send(err);
         }

          var resultado = [];

         // console.log(pessoa);

          // tem como resultado as pessoas que tem o tipo inverso do que consulta
          for(var i in pessoas) {
            if(pessoas[i].vagasPendentes > 0) {
              if(pessoas[i].bairro.toUpperCase() === pessoa.bairro.toUpperCase()) {
              if(pessoa.tipoCarona !== pessoas[i].tipoCarona) {
                  if(pessoa.tipoServico == pessoas[i].tipoServico || pessoas[i].tipoServico === 2) {
                    resultado.push(pessoas[i]);
                  }
                }
              }
            }
          }

         res.json(resultado);
       
      });
    });
});

router.route('/pessoa/:token')
   .get(function(req, res) {
      var query = Pessoa.findOne({ 'token': req.query.token });
      query.select('token nome curtidas endereco telefone tipoServico bairro cidade uf dataInicial dataFinal vagasPendentes tipoCarona horarioIda horarioVolta ativo');
      query.exec(function (err, pessoa) {
        if(err) {
          res.send(err)
        }

        res.json(pessoa);
  });
});

router.route('/pessoa/:pessoa_id')
	.get(function(req, res) {
		Pessoa.findById(req.params.pessoa_id,function(err, pessoa) {
      if (err) {
        res.send(err);
      }

			res.json(pessoa);
	    });
	})
	.put(function(req, res) {
		Pessoa.findById(req.params.pessoa_id, function(err, pessoa) {

			if (err)
				res.send(err);

			pessoa.token = req.body.token;
			pessoa.name = req.body.name;
			pessoa.curtidas = req.body.curtidas;
			pessoa.endereco = req.body.endereco;
			pessoa.tipoServico = req.body.tipoServico;
			pessoa.bairro = req.body.bairro;
			pessoa.cidade = req.body.cidade;
			pessoa.uf = req.body.uf;
			pessoa.dataInicial = req.body.dataInicial;
			pessoa.dataFinal = req.body.dataFinal;
			pessoa.vagasPendentes = req.body.vagasPendentes;
			pessoa.tipoCarona = req.body.tipoCarona;
			pessoa.horarioIda = req.body.horarioIda;
			pessoa.horarioVolta = req.body.horarioVolta;
			pessoa.ativo = req.body.ativo;
      pessoa.telefone = req.body.telefone

			// save the bear
			pessoa.save(function(err) {
				if (err)
					res.send(err);

				res.json({ message: 'Bear updated!' });
			});

		});

	})
	.delete(function(req, res) {
		Pessoa.remove({
			_id: req.params.pessoa_id
		}, function(err, pessoa) {
			if (err)
				res.send(err);

			res.json({ message: 'Successfully deleted' });
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
