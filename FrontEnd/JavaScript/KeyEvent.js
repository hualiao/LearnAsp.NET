// Ref http://javascript.info/tutorial/keyboard-events
// Date: 2015-10-29 00:24

// 1.Get input char
/*
In all browsers except IE, the charCode property is defined for keypress and contains the 
character code. Opera follows this principle, but bugs on special keys. It triggers keypress 
without charCode on some of them, e.g “backspace”.
Internet Explorer has it’s own way. In case of keypress event it doesn’t set the charCode, 
but puts the character code in keyCode instead of scan-code.
*/

function getChar(event) {
    if (event.which == null) {
        return String.fromCharCode(event.keyCode) // IE
    } else if (event.which != 0 && event.charCode != 0) {
        return String.fromCharCode(event.which) // the rest
    } else {
        return null; // special key
    }
}

// The wrong `getChar`
function getChar(event) {
    return String.fromCharCode(event.keyCode || event.charCode)
}


// 2. Canceling user input

/*
 For all browsers except Opera, two events can be used to cancel a key input: keydown and keypress. 
 But Opera is more picky. It will cancel character only if preventDefault comes from keypress.

 In IE and Safari/Chrome preventing default action on keydown cancels keypressed event too.

 <input onkeydown="return false" type="text" size="30">
 <input onkeypress="return false" type="text" size="30">

*/

// 3. Scan-codes
/*
Most browsers do not generate keypress for such keys. Instead, 
for special keys the keydown/keyup should be used.
Another example is hotkeys. When we implement a custom hotkey with JavaScript, 
it should work same no matter of case and current language. We don’t want a character. 
We just want a scan code.

As we know, the char code is a unicode character code. It is given only in the keypress event.

In case of a letter, the scan code equals an uppercased english letter char code.
For example, you want to track “Ctrl-S” hotkey. The checking code for keydown event would be:
  e.ctrlKey && e.keyCode == 'S'.charCodeAt(0)

For all keys except ';', '=' and '-' different browsers use same key code.

*/

//----------------- Example --------------------------

// <input id='my' type="text">

document.getElementById('my').onkeypress = function(event) {
    var char = getChar(event || window.event)
    if (!char) return // special key
    this.value = char.toUpperCase()
    return false
}

/*
 a. How to track when a mousie is in clicked state and when it’s not?
 To make DIV focusable, we should add tabindex:
 <div style="width:41px;height:48px;background:url(mousie.gif)" id="mousie" tabindex="0"></div>
 b. How to track keys?
 We choose keydown, because it allows to cancel the default action, which is page scrolling.
 c. How to move the mousie?
 Like any other element: position:absolute, left and top change depending on the key.
*/

// we need a placeholder or a wrapper, like this:
//<div style="width:41px; height:48px">
//  <div style="width:41px;height:48px;background:url(mousie.gif)" id="mousie" tabindex="0"></div>

//</div>

document.getElementById('mousie').onfocus = function () {
    this.style.position = 'absolute'
    var offset = getOffset(this)
    this.style.left = offset.left + 'px'
    this.style.top = offset.top + 'px'
}



document.getElementById('mousie').onkeydown = function (e) {
    e = e || event
    switch (e.keyCode) {
        case 37: // left
            this.style.left = parseInt(this.style.left) - this.offsetWidth + 'px'
            return false
        case 38: // up
            this.style.top = parseInt(this.style.top) - this.offsetHeight + 'px'
            return false
        case 39: // right
            this.style.left = parseInt(this.style.left) + this.offsetWidth + 'px'
            return false
        case 40: // down
            this.style.top = parseInt(this.style.top) + this.offsetHeight + 'px'
            return false
    }
}



/*
How to track Caps Lock?

Unfortunately, there is no direct access to the state.

But we could use the events:
 a.Check keypress events. An uppercased char without shift or a lowercased char with shift means that Caps Lock is on.
 b.Check keydown for Caps Lock key. It has keycode 20.
*/
var capsLockEnabled = null
document.onkeypress = function (e) {
    e = e || event

    var chr = getChar(e)
    if (!chr) return // special key

    if (chr.toLowerCase() == chr.toUpperCase()) {
        // caseless symbol, like whitespace 
        // can't use it to detect Caps Lock
        return
    }

    capsLockEnabled = (chr.toLowerCase() == chr && e.shiftKey) || (chr.toUpperCase() == chr && !e.shiftKey)
}
document.onkeydown = function (e) {
    e = e || event

    if (e.keyCode == 20 && capsLockEnabled !== null) {
        capsLockEnabled = !capsLockEnabled
    }
}

/*
First, the user focuses on it. We should show Caps Lock warning if we know it’s enabled.
The user starts to type. Every keypress bubbles up to document.keypress handler 
which updates capsLockEnabled. We can’t use input.onkeypress to indicate the state to the user, 
because it will work before document.onkeypress (cause of bubbling) and hence before we know the Caps Lock state.

There are many ways to solve this problem. We’ll stick to simplest and assign caps
lock indication handler to input.onkeyup. It always happens after keypress.

At last, user blurs the input. The Caps Lock warning may happen to be on, but it is not needed 
any more if the input is blurred. So we need to hide it.

<input type="text" onkeyup="checkCapsWarning(event)" onfocus="checkCapsWarning(event)" onblur="removeCapsWarning()"/>

<div style="display:none;color:red" id="caps">Warning: Caps Lock is on!</div>

*/

function checkCapsWarning() {
    document.getElementById('caps').style.display = capsLockEnabled ? 'block' : 'none'
}

function removeCapsWarning() {
    document.getElementById('caps').style.display = 'none'
}
