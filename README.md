# Todo API Service

Görev ekleme, silme, güncelleme, kriterlere göre filtreleme işlemlerini yapan bir RESTful API.

## [Swagger URL](https://apitodo.azurewebsites.net/swagger/index.html)


## Assignment:

### 1. GET Assignment/GetAll

Tüm görevleri getirir.

- **URL:** `Assignment/GetAll`
- **Method:** `GET`
- **Request Parameters:** Yoktur.
- **Response:**

```json
[
    {
  "data": [
    {
      "id": 1,
      "name": "string",
      "description": "string",
      "isCompleted": false
    },
    
    {
      "id": 3,
      "name": "boş açıklamalı görev",
      "description": null,
      "isCompleted": true
    }
  ],
  "isSuccess": true,
  "message": "Tüm Görevler:"
}
]
```



### 2. POST /Assignment/Add

Yeni görev ekler.

- **URL:** `/Assignment/Add`
- **Method:** `POST`
- **Request Parameters:** ` "name" : Görev adı. "description": Görev açıklaması(opsiyonel).  `
- **Response:**

```json

[
{
  "data": {
    "id": 5,
    "name": "yeni görev",
    "description": null,
    "status": 1,
    "boardId": 2
  },
  "isSuccess": true,
  "message": "Ekleme başarılı."
}
]

```


### 3. PATCH /Assignment/Update

Belirtilen görevi günceller.

- **URL:** `/Assignment/Update`
- **Method:** `PATCH`
- **Request Parameters:** ` "id" : Güncellenecek görev id. "name" : Görev adı. "description": Görev açıklaması. "isCompleted" : Tamamlanma durumu.  `
- **Response:**

```json

[
{
  "data": true,
  "isSuccess": true,
  "message": "Güncelleme başarılı."
}
]

```
### 4. DELETE /Assignment/Delete

Id parametresinde belirtilen görevi siler.

- **URL:** `/Assignment/Delete`
- **Method:** `DELETE`
- **Request Parameters:** ` "id" : Silinecek görev id.   `
- **Response:**

```json

[
{
  "data": true,
  "isSuccess": true,
  "message": "Silme işlemi başarılı."
}
]

```



### 5. POST /Assignment/FilterByStatus

Görev durumlarına göre görevleri filtreler.

- **URL:** `/Assignment/FilterByStatus`
- **Method:** `POST`
- **Request Parameters:** ` "status" : Görevin durumu. "boardId": Board Id(board'a göre filtrelenir, zorunludur.)     `
- **Response:**

```json

[
{
  "data": [
    {
      "id": 3,
      "name": "boş açıklamalı görev",
      "description": null,
      "isCompleted": true
    }
  ],
  "isSuccess": true,
  "message": "Durum Filtresine Göre Sonuçlar:"
}
]

```


### 6. POST /Assignment/FilterByName

İsimlerine göre görevleri filtreler.

- **URL:** `/Assignment/FilterByName`
- **Method:** `POST`
- **Request Parameters:** `  "name" : Görev adı. "boardId": Board Id(board'a göre filtrelenir, zorunludur.)   `
- **Response:**

```json

[
{
  "data": [
    {
      "id": 3,
      "name": "boş açıklamalı görev",
      "description": null,
      "isCompleted": true
    }
  ],
  "isSuccess": true,
  "message": "Durum Filtresine Göre Sonuçlar:"
}
]

```





## Board:

### 1. GET /Board/GetAll

Tüm boardları getirir.

- **URL:** `/Board/GetAll`
- **Method:** `GET`
- **Request Parameters:** Yoktur.
- **Response:**

```json

[
{
  "data": [
    {
      "id": 1,
      "name": "Board1",
      "status": true
    },
    {
      "id": 2,
      "name": "Board2",
      "status": true
    }
  ],
  "isSuccess": true,
  "message": "Tüm Boardlar Listesi:"
}
]

```



### 2. POST /Board/Add

Yeni board ekler.

- **URL:** `/Board/Add`
- **Method:** `POST`
- **Request Parameters:** ` "name" : Board adı.  `
- **Response:**

```json

[
{
  "data": {
    "id": 9,
    "name": "Board9",
    "status": true
  },
  "isSuccess": true,
  "message": "Ekleme işlemi başarıyla tamamlandı."
}
]

```


### 3. PATCH /Board/Update

Belirtilen board'ı günceller.

- **URL:** `/Board/Update`
- **Method:** `PATCH`
- **Request Parameters:** ` "id" : Güncellenecek görev id. "name" : Board adı. "status" : Aktif/Pasif durumu.  `
- **Response:**

```json

[
{
  "isSuccess": true,
  "message": "Güncelleme işlemi başarıyla tamamlandı."
}
]

```


### 4. DELETE /Board/Delete

Id parametresinde belirtilen board'ı siler.

- **URL:** `/Board/Delete`
- **Method:** `DELETE`
- **Request Parameters:** ` "id" : Silinecek görev id.   `
- **Response:**

```json

[
{
  "data": true,
  "isSuccess": true,
  "message": "Silme işlemi başarılı."
}
]

```



## RecentlyVisited:



### 1. GET /RecentlyVisited/Get

Son gidilen 10 endpoint'i getirir. Cookie bazlı çalışır.

- **URL:** `/RecentlyVisited/Get`
- **Method:** `GET`
- **Request Parameters:** Yoktur.
- **Response:**

```json

[
  "GetAll",
  "Add",
  "Update",
  "Delete"
]

```



