// Setup basic express server
var express = require('express');
var app = express();
var server = require('http').createServer(app);
var io = require('socket.io')(server);
var port = process.env.PORT || 4000;

// MongoDB part
var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost/test";

server.listen(port, function () {
  console.log('Server listening at port %d', port);
});

// Routing
app.use(express.static(__dirname + '/public'));

io.on('connection', function(socket) {
  var addedUser = false;

  // when the client emits 'new message', this listens and executes
  socket.on('new message', function (data) {
    //console.log('[new message] socket.position = %s', socket.position);
    console.log('[data] socket = %', data);

    // we tell the client to execute 'new message'
    socket.broadcast.to(socket.position).emit('new message', {
      username: socket.username,
      message: data
    });
  }); 
});
