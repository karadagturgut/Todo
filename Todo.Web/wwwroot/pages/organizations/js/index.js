
    document.addEventListener('DOMContentLoaded', () => {
        const tbody = document.getElementById('organizationTableBody');
    const url = '/Organization/GetAll';
    fetch(url)
            .then(response => {
                if (!response.ok) throw new Error(`Sunucu hatası: ${response.status}`);
    return response.json();
            })
            .then(json => {
        console.log('API yanıtı:', json);
    // 1) Dış sarımdaki başarılı/mesaj kontrolü
    const outerSuccess = json.isSuccess ?? json.IsSuccess;
    const outerMsg = json.message ?? json.Message;
    if (!outerSuccess) {
        alert(outerMsg || 'Bir hata oluştu.');
    return;
                }

    // 2) İç sarım (data) objesini al
    const wrapper = json.data;
    if (!wrapper) {
        alert('Beklenmedik veri formatı: wrapper yok.');
    return;
                }

    // 3) İç sarımdaki başarı ve mesaj
    const innerSuccess = wrapper.isSuccess ?? wrapper.IsSuccess;
    const innerMsg = wrapper.errorMessage ?? wrapper.Message;
    if (!innerSuccess) {
        alert(innerMsg || 'Bir hata oluştu (inner).');
    return;
                }

    // 4) Array’i al
    const list = wrapper.data;
    if (!Array.isArray(list)) {
        console.error('Beklenen dizi, gelen:', list);
    alert('Beklenmedik veri formatı: dizi bekleniyordu.');
    return;
                }

    // 5) Tabloyu doldur
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
        console.error('Organizasyon verisi yüklenemedi:', err);
    alert('Veri yükleme hatası: ' + err.message);
            });

        // Düzenle/Sil tıklamalarını yakala
        tbody.addEventListener('click', e => {
            const btn = e.target.closest('button');
    if (!btn) return;
    const id = btn.dataset.id;

    if (btn.classList.contains('edit-btn')) {
        window.location.href = '/Organization/Edit/' + id;
            }
    else if (btn.classList.contains('delete-btn')) {
                if (!confirm('Bu kaydı silmek istediğinize emin misiniz?')) return;

    fetch('/Organization/Delete/' + id, {
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