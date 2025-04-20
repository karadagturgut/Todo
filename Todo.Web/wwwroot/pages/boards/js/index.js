
document.addEventListener('DOMContentLoaded', () => {
    const tbody = document.getElementById('boardTableBody');
    const url = '/Board/ActiveBoards';
    fetch(url)
        .then(response => {
            if (!response.ok) throw new Error(`Sunucu hatası: ${response.status}`);
            return response.json();
        })
        .then(json => {
            console.log('API yanıtı:', json);
            debugger;
            const outerSuccess = json.isSuccess ?? json.IsSuccess;
            const outerMsg = json.message ?? json.Message;
            if (!outerSuccess) {
                alert(outerMsg || 'Bir hata oluştu.');
                return;
            }

            const wrapper = json.data;
            if (!wrapper) {
                alert('Beklenmedik veri formatı: wrapper yok.');
                return;
            }

            const innerSuccess = wrapper.isSuccess ?? wrapper.IsSuccess;
            const innerMsg = wrapper.errorMessage ?? wrapper.Message;
            if (!innerSuccess) {
                alert(innerMsg || 'Bir hata oluştu (inner).');
                return;
            }

            const list = wrapper.data;
            if (!Array.isArray(list)) {
                console.error('Beklenen dizi, gelen:', list);
                alert('Beklenmedik veri formatı: dizi bekleniyordu.');
                return;
            }

          
            tbody.innerHTML = '';
            list.forEach(item => {
                const tr = document.createElement('tr');
                tr.innerHTML = `
    <td>${item.name}</td>
    <td>
        <button class="btn btn-sm btn-primary edit-btn" data-id="${item.id}">
            Düzenle
        </button>
        <button class="btn btn-sm btn-danger delete-btn" data-id="${item.id}">
            Sil
        </button>
    </td>
    <td></td>
    `;
                tbody.appendChild(tr);
            });
        })
        .catch(err => {
            console.error('Pano verisi yüklenemedi:', err);
            alert('Veri yükleme hatası: ' + err.message);
        });

    tbody.addEventListener('click', e => {
        const btn = e.target.closest('button');
        if (!btn) return;
        const id = btn.dataset.id;

        if (btn.classList.contains('edit-btn')) {
            window.location.href = '/Board/Edit/' + id;
        }
        else if (btn.classList.contains('delete-btn')) {
            if (!confirm('Bu kaydı silmek istediğinize emin misiniz?')) return;

            fetch('/Board/Delete/' + id, {
                method: 'DELETE'
            })
                .then(r => {
                    if (!r.ok) throw new Error('Silme işlemi başarısız.');
                    return r.json();
                })
                .then(() => window.location.reload())
                .catch(delErr => alert('Silme hatası: ' + delErr.message));
        }
    });
});