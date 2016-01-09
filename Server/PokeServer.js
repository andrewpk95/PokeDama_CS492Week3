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
    console.log('new message received: ', data);
    // we tell the client to execute 'new message'
    socket.emit('new message', data);
  }); 
  
  socket.on('ConnectionTest', function (data) {
    console.log('Connection Test received: ', data);
    
    socket.emit('ConnectionTest', {message: 'efgh'});
  });
  
  socket.on('Request', function (data) {
    console.log('Request Received: ', data);
    var jsonObj;
    try {
      jsonObj = data;
      //console.log(data);
      //console.log(data.toString().slice(0, -4));
      //jsonObj = JSON.parse(data.toString().slice(0, -4));
    }
    catch (e) {
      return console.error(e);
    }
    if (jsonObj.RequestType == "FindByIMEI") {
	//socket.emit('Response', {successful: true, message: 'haha'});
	console.log('Processing FindByIMEI...');
	MongoClient.connect(url, function (err, db) {
        if(err){
          console.log('Error: ', err);
        }
        else {
          db.collection("PokeDama").find({"IMEI":jsonObj.IMEI}).toArray(function (err, items) {
            if (err) {
              console.log('Error:', err);
            }
            else {
              if (items.length == 0) {
		socket.emit('Response', {
		  ResponseType: 'FindByIMEI',
		  successful: false,
		  message: null
		});
	      }
	      else if (items.length == 1) {
		socket.emit('Response', {
		  ResponseType: 'FindByIMEI',
		  successful: true,
		  message: items[0]
		});
	      }
	      else {
		socket.emit('Response', {
		  ResponseType: 'FindByIMEI',
		  successful: false,
		  message: 'error'
		});
	      }
	    }
	  });
	  console.log('Processing Complete');
	}
      });
    }
    else if (jsonObj.RequestType == "FindByLocation") {
	console.log('Processing FindByLocation...');
	MongoClient.connect(url, function (err, db) {
	if(err){
	  console.log('Error: ', err);
	}
	else {
	  var loc = jsonObj.location;
	  var range = jsonObj.range;
          db.collection("PokeDama").find({
	    "location.latitude": {$gt: loc.latitude - range}, 
	    "location.latitude": {$lt: loc.latitude + range},
	    "location.longtitude": {$gt: loc.longtitude - range},
	    "location.longtitude": {$lt: loc.longtitude + range}
	  }).toArray(function (err, items) {
            if (err) {
              console.log('Error:', err);
            }
            else if (item.length) {
              for (var i in items) {
		socket.emit('Response', {
		  ResponseType: 'FindByLocation',
		  successful: true,
		  message: i
		});
	      }
	    }
	    else {
	      console.log('No items found');
	      socket.emit('Response', {
		ResponseType: 'FindByLocation',
		successful: true,
		message: null
	      });
	    }
	    console.log('Processing Complete');
	  });
        }
      });
    }
    else if (jsonObj.RequestType == "Save") {
      console.log('Processing Save...');
      
      MongoClient.connect(url, function (err, db) {
        if(err){
	  console.log('Error: ', err);
        }
        else {
	  var pokedama = jsonObj.PokeDama;
	  
          db.collection("PokeDama").replaceOne(
	    {"IMEI":pokedama.IMEI},
	    pokedama
	  );
	  socket.emit('Response', {
	    ResponseType: 'Save',
	    successful: true,
	    message: pokedama
	  });
	  console.log('Processing Complete');
        }
      });
    }
    else if (jsonObj.RequestType == "Create") {
      console.log('Processing Create...');
      
      MongoClient.connect(url, function (err, db) {
	if (err) {
	  console.log('Error: ', err);
	}
	else {
	  var pokedama = jsonObj.PokeDama;
	  
	  db.collection("PokeDama").insertOne(pokedama);
	  socket.emit('Response', {
	    ResponseType: 'Create',
	    successful: true,
	    message: pokedama
	  });
	  console.log('Processing Complete');
	}
      });
    }
  });
});
