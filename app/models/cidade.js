var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var CidadeSchema = new Schema({
   nome: String,
   codigoibge: String
   //uf
});

module.exports = mongoose.model('Cidade', CidadeSchema);
