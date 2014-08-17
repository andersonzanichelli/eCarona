var express    = require('express');
var app        = express();
var bodyParser = require('body-parser');
var mongoose   = require('mongoose');

var Pessoa     = require('./app/models/pessoa');
var TipoPessoa = require('./app/models/tipopessoa');
var Satisfacao = require('./app/models/tipopessoa');
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
      console.log(req.body);
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

app.use('/eCarona', router);

app.listen(port);
console.log('Server online on port ' + port);
