/**
 * Ref:http://www.dustindiaz.com/javascript-curry/
 */
function curry(fn, scope) {

    var scope = scope || window;

    var args = [];

    for (var i = 2, len = arguments.length; i < len; ++i) {

        args.push(arguments[i]);

    };

    return function () {

        fn.apply(scope, args);

    };

}

//PRACTICAL USAGE OF THE CURRY FUNCTION

/* add a curried function bound to a click event */
var el = document.getElementById('my-element');

el.addEventListener('click', curry(sayHello, el, 'Hello World from Dustin'), false);



function sayHello(msg) {

    alert(msg + '\n You clicked on ' + this.id);

}

/* or to use it in the callback example */

request('comment.php', curry(update, window, 'item'));

/* 

or let's say you're subscribing 

to a custom event (eg. YAHOO.util.CustomEvent) 

*/

var myEvent = YAHOO.util.CustomEvent(

    'shake it up',

    this,

    true,

    YAHOO.util.CustomEvent.FLAT

);

myEvent.subscribe(curry(doSomeThingCool, null, 'that', 'requires', 'these', args));


/**
 * Ref: https://javascriptweblog.wordpress.com/2010/04/05/curry-cooking-up-tastier-functions/
 */
function toArray(obj){
    return Array.prototype.slice.call(obj);
}

Function.prototype.curry = function () {
    if (arguments.length < 1)
        return this; // nothing to curry with - return function
    var __method = this;
    var args = toArray(arguments);
    return function () {
        return __method.apply(this,args.concat(toArray(arguments)));
    }
}

var add = function (a, b) {
    return a + b;
}

var addTen = add.curry(10); // create function that return 10 + argument
addTen(20); // 30
// not very useful example
var make5 = add.curry(2, 3);
make5(); // 5

// better example
var sayHello = alert.curry("Hello!");
friendlyDiv.onmouseover = sayHello;


// Here's another example that generates various conversion functions
var converter = function (ratio, symbol, input) {
    return [(input * ratio).toFixed(1), symbol].join(" ");
}

var kilosToPounds = converter.curry(2.2, "lbs");
var litersToUKPints = converter.curry(1.75, "imperial pints");
var litersToUSPints = converter.curry(1.98, "US pints");
var milesToKilometers = converter.curry(1.62, "km");

kilosToPounds(4); //8.8 lbs
litersToUKPints(2.4); // 4.2 imperial pints
litersToUSPints(2.4); // 4.8 US pints
milesToKilometers(34); // 55.1 km