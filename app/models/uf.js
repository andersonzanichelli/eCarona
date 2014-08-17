var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var UfSchema = new Schema({
   nome: String,
   sigla: String
});



module.exports = mongoose.model('Uf', UfSchema);
