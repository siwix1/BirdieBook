//<script type="text/javascript">
    $(document).ready(function () {

        $('#golfCourseName').autocomplete({
            source: function (request, response) {
                $.getJSON('/GolfCourses/GetGolfCoursesJson', { term: request.term }, function (data) {
                    response($.map(data, function (el) {
                        return {
                            value: el.golfCourseId,
                            label: el.name
                        };
                    }));
                });
            },
            select: function (event, ui) {
                event.preventDefault();
                console.log(ui.item.value);
                console.log(ui.item.label);
                $('#golfCourseName').val(ui.item.label);
                $('#golfCourseId').val(ui.item.value);
                refreshTeeBoxes(ui.item.value);
            }            /* other autocomplete options */
        });

        function refreshTeeBoxes(golfCourseId) {
            console.log('refreshTeeBoxes');
            $.getJSON("/TeeBoxes/GetTeeBoxesJson", {golfCourseId: golfCourseId }, function (data) {
                $.each(data,
                    function (key, val) {
                        console.log(key + ' ' + val.teeBoxId);
                        $('#teeBoxName').append('<option value="' + val.teeBoxId + '">' + val.name + '</option>');
                    });
            });
        }

        $('#teeTime').datetimepicker();

        $('#teeBoxName').click(function () {
            var id = $(this).val();


            //var id = $(this).children(":selected").attr("id");
            console.log('selected id = ' + id);
            $.getJSON('/TeeBoxes/GetParJson', { teeBoxId: id }, function(data) {
                console.log(data);
                $('#dsr').val(data);

            });
        });

    });
//</script>




