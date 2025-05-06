$(document).ready(function () {
    $('#organizationTable').DataTable({
        ajax: {
            url: '/Organization/GetAll',
            type: 'GET',
            dataSrc: function (json) {
                const outerSuccess = json.isSuccess ?? json.IsSuccess;
                const wrapper = json.data;
                if (!outerSuccess || !wrapper || !wrapper.isSuccess) {
                    toastr.error('Board verisi yüklenemedi.');
                    return [];
                }
                return wrapper.data || [];
            }
        },
        columns: [
            { data: 'name' },
            //{
            //    data: 'status',
            //    render: function (data) {
            //        return data
            //            ? '<span class="badge badge-success">Aktif</span>'
            //            : '<span class="badge badge-danger">Pasif</span>';
            //    }
            //},
            //{
            //    data: 'organizationId',
            //    render: function (data) {
            //        return data > 0 ? data : '-';
            //    }
            //},
            {
                data: 'id',
                orderable: false,
                searchable: false,
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-outline-primary edit-btn" data-id="${data}">
                            <i class="ti-pencil"></i> Düzenle
                        </button>
                        <button class="btn btn-sm btn-outline-danger delete-btn" data-id="${data}">
                            <i class="ti-trash"></i> Sil
                        </button>
                    `;
                }
            }
        ],
        responsive: true,
        pageLength: 10,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/tr.json"
        },
        dom: '<"d-flex justify-content-between align-items-center mb-3"lf>tip',
    });

    $('#boardTable tbody').on('click', '.edit-btn', function () {
        const id = $(this).data('id');
        window.location.href = '/Board/Edit/' + id;
    });

    $('#boardTable tbody').on('click', '.delete-btn', function () {
        const id = $(this).data('id');

        Swal.fire({
            title: 'Silmek istediğinize emin misiniz?',
            text: "Bu işlem geri alınamaz!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch('/Organization/Delete/' + id, {
                    method: 'DELETE'
                })
                    .then(r => {
                        if (!r.ok) throw new Error('Silme işlemi başarısız.');
                        return r.json();
                    })
                    .then(() => {
                        toastr.success('Kayıt başarıyla silindi.');
                        $('#boardTable').DataTable().ajax.reload();
                    })
                    .catch(err => {
                        console.error(err);
                        toastr.error('Silme hatası: ' + err.message);
                    });
            }
        });
    });
});
