var mongoose	= require('mongoose');
var Schema	= mongoose.Schema;

var TipoCaronaSchema= new Schema({
	descricao: String
});

module.exports = mongoose.model('TipoCarona', TipoCaronaSchema);
