var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var PaisSchema = new Schema({
   nome: String,
   sigla: String,
   codigo: Number
});

module.exports = mongoose.model('Pais', PaisSchema);
