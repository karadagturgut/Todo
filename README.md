# Todo API Service

Görev ekleme, silme, güncelleme, kriterlere göre filtreleme işlemlerini yapan bir RESTful API.

## [Swagger URL](https://apitodo.azurewebsites.net/swagger/index.html)


## Endpointler

### 1. GET Assignment/GetAll

Tüm görevleri getirir.

- **URL:** `Assignment/GetAll`
- **Method:** `GET`
- **Request Parameters:** None
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
- **Method:** `GET`
- **Request Parameters:** ` "name" : Görev adı. "description": Görev açıklaması(opsiyonel).  `
- **Response:**

```json

[
{
  "data": {
    "id": 5,
    "name": "yeni görev",
    "description": null,
    "isCompleted": false
  },
  "isSuccess": true,
  "message": "Ekleme başarılı."
}
]

```


### 3. PUT /Assignment/Update

Belirtilen görevi günceller.

- **URL:** `/Assignment/Update`
- **Method:** `PUT`
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
- **Method:** `PUT`
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
- **Method:** `PUT`
- **Request Parameters:** ` "isCompleted" : Tamamlanma durumu.   `
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
- **Method:** `PUT`
- **Request Parameters:** `  "name" : Görev adı.   `
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
