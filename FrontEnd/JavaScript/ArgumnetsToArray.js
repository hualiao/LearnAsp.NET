
// Ref: http://debuggable.com/posts/turning-javascript-s-arguments-object-into-an-array:4ac50ef8-3bd0-4a2d-8c2e-535ccbdd56cb
function hello() {
    var args = Array.prototype.slice.call(arguments);
    args.unshift('hello');
    alert(args.join(' '));
}

hello('pretty', 'world');

// Ref: http://stackoverflow.com/questions/960866/how-can-i-convert-the-arguments-object-to-an-array-in-javascript
function sortArgs() {
    var args = Array.prototype.slice.call(arguments);
    return args.sort();
}