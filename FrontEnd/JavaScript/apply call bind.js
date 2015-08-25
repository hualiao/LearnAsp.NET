// Ref: http://geekswithblogs.net/shaunxu/archive/2013/12/12/bind-call-and-apply-in-javascript-function.aspx

var Person = function (firstName, lastName) {
    this._firstName = firstName;
    this._lastName = lastName;
};
  
Person.prototype.say = function (message) {
    return '[' + this + ']: ' + this._firstName + ' ' + this._lastName + ' said: "' + message + '"';
};
 
Person.prototype.toString = function () {
    return '[object Person]';
};


var shaunxu = new Person('Shaun', 'Xu');
  
console.log("shaunxu.say('Hellold')");
console.log(shaunxu.say('Hello world'));
console.log();


var func = shaunxu['say'];
console.log("func('Hello world')");
console.log(func('Hello world'));
console.log();

// We assigned the value to the variants "_firstName" and "_lastName" without defined 
// so that they will became members under the global context. The code only worked under the non-strict mode.
_firstName = 'Kun';
_lastName = 'Zhang';
 
var func = shaunxu['say'];
console.log("func('Hello world')");
console.log(func('Hello world'));
console.log();

// The only different between "call" and "apply" is, you can pass parameters for following arguments to "call", 
// while you can pass parameters as an array to "apply".

var func = shaunxu['say'];
 
console.log("func.call(shaunxu, 'Hello world')");
console.log(func.call(shaunxu, 'Hello world'));
console.log();
 
console.log("func.apply(shaunxu, ['Hello world'])");
console.log(func.apply(shaunxu, ['Hello world']));
console.log();

// With "bind()" method, we can set the context of a function. 
// So in the future we can invoke this function variant without specifying the context when invoked.
var func = shaunxu['say'].bind(shaunxu);
 
console.log("func.bind(); func('Hello world')");
console.log(func('Hello world'));
console.log();


var func = shaunxu['say'].bind({ _firstName: 'Ziyan', _lastName: 'Xu' });
 
console.log("func.bind(); func('Hello world')");
console.log(func('Hello world'));
console.log();