jQuery(document).ready(function($){
    $(document).on('click','.tab-title',function(e){
        e.preventDefault();
        var _this = $(this);
        var parent = $('#'+_this.data('parent'));
        var target = $(_this.attr('href'));
        parent.find('.tab-title').removeClass('active');
        parent.find('.tab-content').removeClass('active');
        target.addClass('active');
        _this.addClass('active');
    });
    $( ".date-picker input" ).datepicker({
        showOn: "button",
        buttonImage: "./images/calendar.png",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        regional: 'vi'
    });
});