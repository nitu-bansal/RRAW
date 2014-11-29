/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />

function handleResize(funcCallOndrag) {
    $('textarea.trac-resizable').each(function () {
        var textarea = $(this);
        var offset = null;

        function beginDrag(e) {
            offset = textarea.height() - e.pageY;
            textarea.blur();
            $(document).mousemove(dragging).mouseup(endDrag);
            return false;
        }

        function dragging(e) {
            textarea.height(Math.max(32, offset + e.pageY) + 'px');
            window[funcCallOndrag]();
            return false;
        }

        function endDrag(e) {
            textarea.focus();
            $(document).unbind('mousemove', dragging).unbind('mouseup', endDrag);
        }

        var grip = $('<div class="trac-grip"/>').mousedown(beginDrag)[0];
        textarea.wrap('<div class="trac-resizable"><div></div></div>')
            .parent().append(grip);
        grip.style.marginLeft = (this.offsetLeft - grip.offsetLeft) + 'px';
        grip.style.marginRight = (grip.offsetWidth - this.offsetWidth) + 'px';
    });
}