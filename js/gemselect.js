function renderjQueryComponents() {
    $(document).ready(function () {
        Reload();
    });
}

function Reload() {
    $('.select2').select2({
        placeholder: "Select",
    }).on('select2:unselecting', function () {
        $(this).data('unselecting', true);
    }).on('select2:opening', function (e) {
        if ($(this).data('unselecting')) {
            $(this).removeData('unselecting');
            e.preventDefault();
        }
    }).on('change.select2', function (e) {
        var selectedVal = e.target.value;
        DotNet.invokeMethodAsync('Warrior', 'UpdateGem', selectedVal);
        //BlazorApp - Your Application DLL Name
        //UpdateModel - Function Name [JSInvokable]
    });;
}