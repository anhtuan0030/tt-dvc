/**
 * Created by Hai Yen on 9/6/14.
 */
jQuery(document).ready(function($){
    $( ".date-picker input" ).datepicker({
        showOn: "button",
        buttonImage: "./images/calendar.png",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        regional: 'vi'
    });
});