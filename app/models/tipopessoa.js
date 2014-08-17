var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var TipoPessoaSchema = new Schema({
   descricao: String,
   ativo: Number
});

module.exports = mongoose.model('TipoPessoa', TipoPessoaSchema);
