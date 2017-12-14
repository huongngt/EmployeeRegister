$(document).ready(function () {
    $("#ddlEmployee").on("change", function () {
        $.ajax(
        {
            url: '/Salary/GetSalaryRanges?range=' + $(this).val(),
            type: 'GET',
            data: "",
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $("#partialDiv").html(data);
            },
            error: function () {
                alert("error");
            }
        });
    });

    var ajaxFormSubmit= function (){
        var  $form=$(this);

        //options for ajax function/JSON data
        var options = {
            url : $form.attr("action"),
            type : $form.attr("method"),
            data : $form.serialize()
        };

        //ajax function call after submit done -> attach data to the part of form pointed in attribute data-custom-target
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-custom-target"));
            var $count = $($form.attr("data-custom-count"));
            $target.replaceWith(data);
            var rowCount = $("table").find("tr").length;
            if ($count.find("#count").length > 0) {
                $count.find("#count").remove();
            }
            var $div = $("<p>", { id: "count" });
            $div.text("Search result: " + (rowCount-1));
            //if ($count.remove($("<p>"))
            $count.append($div);
        });

        //prevent page to reload, similar to event.preventDefault(); 
        return false;
    };

    //var ajaxKeyDown = function (e) {
    //    if (e.keyCode == 13){
    //        var $form = $(this);

    //        options for ajax function/JSON data
    //        var options = {
    //            url: $form.attr("action"),
    //            type: $form.attr("method"),
    //            data: $form.serialize()
    //        };
    //        $.ajax(options).done(function (data) {
    //            var $target = $($form.attr("data-custom-target"));
    //            $target.replaceWith(data);
    //        });
    //    }
    //    return false;

    //}


    //create a form tag and put data-custom-ajax='true' -> when the form submit, it calls the ajax code 
    $("form[data-custom-ajax='true']").submit(ajaxFormSubmit);
    //$(".sendContact").keydown(ajaxKeyDown);


});