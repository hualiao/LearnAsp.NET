// Ref: http://www.dustindiaz.com/scriptjs
// https://github.com/ded/script.js

var el = doc.createElement("script"),
loaded = false;
el.onload = el.onreadystatechange = function () {
    if ((el.readyState && el.readyState !== "complete" && el.readyState !== "loaded") || loaded) {
        return false;
    }
    el.onload = el.onreadystatechange = null;
    loaded = true;
    // done!
};
el.async = true;
el.src = path;
document.getElementsByTagName('head')[0].insertBefore(el, head.firstChild);


// use $script.js JavaScript loader
// <script src="jquery.js"></script>
$script('analytics.js');
$script('library.js', function () {
    // do stuff with library.
});

/* DEPENDENCY MANAGEMENT */

// 1.
$script('yui-base.js', function () {
    // do stuff with base...
    $script(['yui-anim.js', 'yui-connect.js'], function () {
        // do stuff with anim and connect...
    });
    $script('yui-drag.js', function () {
        // do stuff with drag...
    });
});

// 2.
$script('jquery.js', 'jquery');


$script.ready('jquery', function () {
    // do stuff with jQuery
});


/*

old school - blocks CSS, Images, AND JS!
<script src="jquery.js"></script>
<script src="my-jquery-plugin.js"></script>
<script src="my-app-that-uses-plugin.js"></script>

*/

// middle school - loads as non-blocking, but has multiple dependents
$script('jquery.js', function() {
    $script('my-jquery-plugin.js', function() {
        $script('my-app-that-uses-plugin.js');
    });
});


// new school - loads as non-blocking, and ALL js files load asynchronously
$script('jquery.js', 'jquery');
$script('my-jquery-plugin.js', 'plugin');
$script('my-app-that-uses-plugin.js');


/*--- in my-jquery-plugin.js ---*/
$script.ready('jquery', function() {
    // plugin code...
});


/*--- in my-app-that-uses-plugin.js ---*/
$script.ready('plugin', function() {
    // use your plugin :)
});

// ----------------------------------
var dependencyList = {
    foo: 'foo.js',
    bar: 'bar.js',
    thunk: ['thunkor.js', 'thunky.js']
};


$script('foo.js', 'foo');
$script('bar.js', 'bar');


$script.ready(['foo', 'bar', 'thunk'], function () {
    // foo.js & bar.js & thunkor.js & thunky.js is ready
}, function (depsNotFound) {
    // did not find 'thunk' dependency (which has thunkor.js and thunky.js
    // so lazy load them now
    depsNotFound.forEach(function (dep) {
        $script(dependencyList[dep], dep);
    });
});