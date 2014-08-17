var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var CaronaSchema = new Schema({
	id_motorista: Number,
	id_carona: Number
});

module.exports = mongoose.model('Carona', CaronaSchema);
