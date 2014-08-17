var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var UfSchema = new Schema({
   nome: String,
   sigla: String
   //pais
});

module.exports = mongoose.model('Uf', UfSchema);
