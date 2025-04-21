var $jq = jQuery.noConflict();

$jq(document).ready(function () {
    const url = '/Organization/GetAll';

    $jq.ajax({
        url: url,
        type: 'GET',
        dataType: 'json'
    })
        .done(function (json) {
            console.log('API yanıtı:', json);

            const outerSuccess = json.isSuccess ?? json.IsSuccess;
            if (!outerSuccess) {
                alert(json.message ?? json.Message ?? 'Bir hata oluştu.');
                return;
            }

            const wrapper = json.data;
            if (!wrapper) {
                alert('Beklenmedik veri formatı: data yok.');
                return;
            }

            const innerSuccess = wrapper.isSuccess ?? wrapper.IsSuccess;
            if (!innerSuccess) {
                alert(wrapper.errorMessage ?? wrapper.Message ?? 'Bir hata oluştu (inner).');
                return;
            }

            const list = wrapper.data;
            if (!Array.isArray(list)) {
                alert('Beklenmedik veri formatı: dizi bekleniyordu.');
                return;
            }


            const $select = $jq('.js-org-select').empty();
            $jq.each(list, function (_, item) {
                $jq('<option>')
                    .val(item.id)
                    .text(item.name)
                    .appendTo($select);
            });


            $jq('.js-org-select').select2({
                placeholder: 'Organizasyon seçiniz',
                allowClear: true
            });
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error('AJAX hatası:', textStatus, errorThrown);
            alert('Veri yükleme hatası: ' + errorThrown);
        });
});
