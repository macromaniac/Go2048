var net = require('net');

net.createServer(function(socket){
	socket.on('data',function(data){
		socket.write("hello!");
		console.log('data: ' + data);
	});
}).listen(7777);
console.log("Listening to port 7777");
