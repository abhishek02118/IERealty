$(document).ready(function () {
    $("#datepicker").datepicker();
    if($("#hdn_appointment_Date")!=undefined && $("#hdn_appointment_Date").val()!=undefined && $("#hdn_appointment_Date").val()!='' )
    {
        $("#datepicker").datepicker('setDate', $("#hdn_appointment_Date").val());
    }
    
    
    $("#datepicker").on("change", function () {
        var selected = $(this).val();
        $("#hdn_appointment_Date").val(selected);
        $("#txtbx_appointmentDate").val(selected);
    });

    $('#drpdwn_timelist').click(function () {
        //var rr = [];
        //$('.selectpicker :selected').each(function (i, selected) {
        //    rr[i] = $(selected).text();
        //});
        //alert(rr);
        $("#txtbx_appointmenttime").val($('#drpdwn_timelist').val());
    });
    
});
    
    
    