var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var PessoaSchema = new Schema({
	token: String,
	nome: String,
	curtidas: Number,
	endereco: String,
	tipoServico: String, // ofereco / solicito
	bairro: String,
	cidade: String,
	uf: String,
	dataInicial: String,
	dataFinal: String,
	vagasPendentes: Number,
	tipoCarona: String, // somente ida, somente volta, ida e volta
	horarioIda: String,
	horarioVolta: String,
	ativo: Number
});

module.exports = mongoose.model('Pessoa', PessoaSchema);
