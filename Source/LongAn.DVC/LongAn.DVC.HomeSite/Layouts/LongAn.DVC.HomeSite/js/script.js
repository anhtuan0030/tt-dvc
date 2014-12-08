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
    if (typeof $.datepicker != 'undefined'){
        $( ".date-picker input" ).datepicker({
            showOn: "button",
            buttonImage: "./images/calendar.png",
            buttonImageOnly: true,
            dateFormat: 'dd-mm-yy',
            regional: 'vi'
        });
    }

    $(document).on('click','.acco-tab-title',function(e){
        var _this = $(this);
        var _content  = _this.next();
        $('.acco-tab-title').removeClass('active');
        $('.acco-tab-content').removeClass('active');
        _this.addClass('active');
        _content.addClass('active');
    });

    $(document).on('click','.button-expand',function(e){
        e.preventDefault();
        var _this = $(this);
        var id = $(_this.attr('href'));
        id.slideToggle();
        _this.toggleClass('expanded');
    });
});