// Import the discord.js module
const Discord = require('discord.js');
const fs = require("fs");
const {prefix, token} = require("./config.json");
// Create an instance of a Discord client
const client = new Discord.Client();


// const poem = fs.readFile('./poem.txt', 'utf8');

var controls;
// First I want to read the file
fs.readFile('./controls.txt','utf8', function read(err, data) {
    if (err) {
        throw err;
    }
    controls = data;

    // Invoke the next step here however you like
    //console.log(content);   // Put all of the code here (not the best solution)
    //processFile();          // Or put the next step in a function and invoke it
});

// The ready event is vital, it means that your bot will only start reacting to information
// from Discord _after_ ready is emitted
client.on('ready', () => {
  console.log('I am ready!');
});

// Create an event listener for messages
client.on('message', message => {
  // If the message is "ping"
  if (message.content === `${prefix}ping`) {
    // Send "pong" to the same channel
    message.channel.send('pong');
  }

  else if 
  	(message.content === `${prefix}server`) {
  		message.channel.send(`This server's name is: ${message.guild.name}`);
  	}

  	else if (message.content === `${prefix}controls`) {

  		message.channel.send(controls);
  	}

});

// Arrays to search based on user input, and convert these terms to single-letter commands for the emulator. Should only have to check for lower case.
var up = ["up"];
var left = ["left"];
var right = ["right"];
var down = ["down"];

var commands = ['u', 'd', 'l', 'r', 'a', 'b', 'x', 'y', 's', 'e', 'q', 'w']

client.on('message', message => {
    console.log(`${message.createdAt}` + ' ' + `${message.author.username}` + ': ' + message.content);

    // Testing
    // console.log(up.indexOf(message.content) > -1);

    // Lower case only please
    message.content = message.content.toLowerCase();

    // .indexOf(message.content) verifies that String message.content is in Array 'up'. Using 'if ... in' returns false for string searches 
    //   becasue you can only search index numbers. https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/in
    if (up.indexOf(message.content) > -1) {
    	message.content = "u";
    }
    if (left.indexOf(message.content) > -1) {
    	message.content = "l";
    }
    if (down.indexOf(message.content) > -1) {
    	message.content = "d";
    }
    if (right.indexOf(message.content) > -1) {
    	message.content = "r";
    }

    // Writes User Messages to text file if they are a valid command
    if (commands.indexOf(message.content) > -1) {
    	fs.writeFile('messages.txt', message.content, (error) => {});
	} else {
		return;
	}
    //Testing
    //console.log('Written to messages.txt:  ' + message.content);
});

client.login(token);