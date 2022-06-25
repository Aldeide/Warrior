function renderjQueryComponents() {
    $(document).ready(function () {
        Reload();
    });
}

function Reload() {
    $('.select2').select2({
        width: "250px",
    }).on('select2:unselecting', function () {
        $(this).data('unselecting', true);
    }).on('select2:opening', function (e) {
        if ($(this).data('unselecting')) {
            $(this).removeData('unselecting');
            e.preventDefault();
        }
    }).on('change.select2', function (e) {
        var selectedVal = e.target.value;
        var slot = e.target.dataset["slot"];
        var item = e.target.dataset["item"];
        var itemSlot = e.target.dataset["itemSlot"];
        if (selectedVal == e.target.dataset["value"]) {
            return;
        }
        DotNet.invokeMethodAsync('Warrior', 'UpdateGem', selectedVal, slot, item, itemSlot);
    });
    $('.select2').each(function (index) {
        var setValue = this.dataset["value"];
        $(this).val(setValue).trigger("change");
    });

}