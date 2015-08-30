// Ref: https://css-tricks.com/perfect-full-page-background-image/

// <img id="bg" src="data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7" alt="" style="position: fixed; left: 0; top: 0" />
(function () {

    var win = $(window);

    win.resize(function () {

        var win_w = win.width(),
            win_h = win.height(),
            $bg = $("#bg");

        // Load narrowest background image based on 
        // viewport width, but never load anything narrower 
        // that what's already loaded if anything.
        var available = [
          1024, 1280, 1366,
          1400, 1680, 1920,
          2560, 3840, 4860
        ];

        var current = $bg.attr('src').match(/([0-9]+)/) ? RegExp.$1 : null;

        if (!current || ((current < win_w) && (current < available[available.length - 1]))) {

            var chosen = available[available.length - 1];

            for (var i = 0; i < available.length; i++) {
                if (available[i] >= win_w) {
                    chosen = available[i];
                    break;
                }
            }

            // Set the new image
            $bg.attr('src', '/img/bg/' + chosen + '.jpg');

            // for testing...
            // console.log('Chosen background: ' + chosen);

        }

        // Determine whether width or height should be 100%
        if ((win_w / win_h) < ($bg.width() / $bg.height())) {
            $bg.css({ height: '100%', width: 'auto' });
        } else {
            $bg.css({ width: '100%', height: 'auto' });
        }

    }).resize();

})(jQuery);