# First.Fizz.Order

## 1. 资源定义规则

- **资源URL**  `http://{host}/{资源}/{id}/{子资源}/{id}`   

## 2. 请求通用Header说明 ##

| 名称        | 说明                     | 示例 |
| ----------- | ------------------------ | ---- |
| ContextType | 固定值：Application/json |      |
| userName    | 租户名                   | fizz |

## 3. 返回值说明 ##

| Status Code | 类型     | 说明     |
| ----------- | -------- | -------- |
| 200         | 业务对象 | 调用成功 |
| 400         | Error    | 获取失败 |

## 4. 资源CURD

### 4.1 获取订单

**简要描述： **

- 根据资源id获取订单和明细资源

**URL示例：**

- 获取全部销售订单：

  `http://{host}/Fizz/SalesOrder?orderNo={orderNo}&pageSize={pageSize}&pageNum={pageNum}&sortName={sortName}`

- 获取某个销售订单：

  `http://{host}/Fizz/SalesOrder/{orderNo}`

**请求方式：**

- Get

**返回参数示例：**

- 全部订单:

` https://localhost:5001/Fizz/SalesOrder`

```json
{
    "pageCount": 1,
    "pageNum": 1,
    "pageItems": [
        {
            "no": "33",
            "clientName": "le",
            "signDate": "2017-02-27T00:00:00",
            "status": 1,
            "comment": "6_s52",
            "orderDetails": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T13:36:08",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T13:36:08"
        },
        {
            "no": "32",
            "clientName": "le",
            "signDate": "2017-02-27T00:00:00",
            "status": 1,
            "comment": "6_s52",
            "orderDetails": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T11:11:06",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T11:11:06"
        }
    ]
}
```

- 某个订单

` https://localhost:5001/Fizz/SalesOrder/32`

```json
{
    "no": "32",
    "clientName": "le",
    "signDate": "2017-02-27T00:00:00",
    "status": 1,
    "comment": "6_s52",
    "orderDetails": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T11:11:06",
    "updateUserNo": "s52",
    "updateUserDate": "2020-08-13T11:11:06"
}
```

### 4.2增加订单

**URL示例：**

- 新增订单：

  `http://{host}/Fizz/SalesOrder`

  header: userName

  request body:

```json
{
    "no":"34",
    "clientname":"le",
    "comment":"6_s52",
    "status":1,
    "signdate":"2017-02-27"
}
  ```

**请求方式：**

- Post

**返回参数示例：**

```json
{
    "no": "34",
    "clientName": "le",
    "signDate": "2017-02-27T00:00:00",
    "status": 1,
    "comment": "6_s52",
    "orderDetails": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T14:33:51.5013797+08:00",
    "updateUserNo": "s52",
    "updateUserDate": "2020-08-13T14:33:51.5013797+08:00"
}
```


### 4.3更新订单

**URL示例：**

- 更新订单：`http://{host}/Fizz/SalesOrder/{orderNo}`

  http method: put

```json
{
    "status":0,
    "comment":"111",
    "signdate":"2020-02-27"
}
```

**请求方式：**

- Put

**返回参数示例：**

` https://localhost:5001/Fizz/SalesOrder/33`

```json
{
    "no": "33",
    "clientName": "bz",
    "signDate": "2020-02-27T00:00:00",
    "status": 0,
    "comment": "111",
    "orderDetails": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T13:36:08",
    "updateUserNo": "fizz",
    "updateUserDate": "2020-08-13T14:34:41.7794059+08:00"
}
```


### 4.4删除订单

**URL示例：**

- 删除订单：`http://{host}/Fizz/SalesOrder/{orderNo}`

**请求方式：**

- Delete

**返回参数示例:**

` https://localhost:5001/Fizz/SalesOrder/6`

```json
""
```

### 4.5获取订单明细

- 获取一个销售订单的所有明细：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail?pageSize={pageSize}&pageNum={pageNum}`

- 获取一个订单明细：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail/{detailNo}`


**请求方式：**

- Get

**返回参数示例:**

- 获取一个订单下的所有明细

` http://localhost:5000/Fizz/SalesOrder/33/OrderDetail`

```json
{
    "pageCount": 1,
    "pageNum": 1,
    "pageItems": [
        {
            "orderNo": "33",
            "rowNo": 4,
            "materialNo": "300",
            "amount": 29.0,
            "unit": "38",
            "sortNo": null,
            "comment": null,
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:37:54",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:37:54"
        },
        {
            "orderNo": "33",
            "rowNo": 5,
            "materialNo": "300",
            "amount": 29.0,
            "unit": "38",
            "sortNo": null,
            "comment": null,
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:41:52",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:41:52"
        },
        {
            "orderNo": "33",
            "rowNo": 6,
            "materialNo": "300",
            "amount": 29.0,
            "unit": "38",
            "sortNo": null,
            "comment": null,
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:41:56",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:41:56"
        }
    ]
}
```

- 获取某个明细

` http://localhost:5000/Fizz/SalesOrder/33/OrderDetail/4`

```json
{
    "orderNo": "33",
    "rowNo": 4,
    "materialNo": "300",
    "amount": 29.0,
    "unit": "38",
    "sortNo": null,
    "comment": null,
    "order": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T14:37:54",
    "updateUserNo": "s52",
    "updateUserDate": "2020-08-13T14:37:54"
}
```

### 4.6创建订单明细

- 订单明细的新增：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail`

  header: userName

  ```json
{
      "rowno":4,
      "materialno":"300",
      "amount":29,
      "unit":"38"
  }
  
  ```

**请求方式：**

- Post

**返回参数示例:**

` https://localhost:5001/Fizz/SalesOrder/33/OrderDetail`

```
{
    "orderNo": "33",
    "rowNo": 4,
    "materialNo": "300",
    "amount": 29.0,
    "unit": "38",
    "sortNo": null,
    "comment": null,
    "order": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T14:37:54.0751952+08:00",
    "updateUserNo": "s52",
    "updateUserDate": "2020-08-13T14:37:54.0751952+08:00"
}
```

### 4.7删除订单明细

- 删除一个订单明细：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail/{detailNo}`

- 删除一个订单下的所有明细：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail`


**请求方式：**

- Delete

**返回参数示例:**

- 删除一个明细

  ` https://localhost:5001/Fizz/SalesOrder/33/OrderDetail/4`

  ```json
  ""
  ```


- 删除一个订单下的所有明细

  ` https://localhost:5001/Fizz/SalesOrder/33/OrderDetail/`

  ```json
  ""
  ```


### 4.8更新订单明细

- 订单明细的修改：

  `http://{host}/Fizz/SalesOrder/{orderNo}/OrderDetail/{detailNo}`

  http method: put

  header: userName

  request body:

  ```
  {
    "comment":"6_s52",
    "amount":200,
    "unit":"10"
  }
  ```
  


**请求方式：**

- Put

**返回参数示例:**

` https://localhost:5001/Fizz/SalesOrder33/OrderDetail/4`

```
{
    "orderNo": "33",
    "rowNo": 4,
    "materialNo": "300",
    "amount": 200.0,
    "unit": "10",
    "sortNo": null,
    "comment": "6_s52",
    "order": null,
    "createUserNo": "s52",
    "createUserDate": "2020-08-13T14:37:54",
    "updateUserNo": "fizz",
    "updateUserDate": "2020-08-13T14:48:51.8050617+08:00"
}
```

### 4.9获取一个用户下的所有订单 

- 获取某个用户全部销售订单：

  `http://{host}/Fizz/OrderUser`

  header: userName

**请求方式：**

- Get

**返回参数示例:**

```
{
    "pageCount": 1,
    "pageNum": 1,
    "pageItems": [
        {
            "no": "32",
            "clientName": "le",
            "signDate": "2017-02-27T00:00:00",
            "status": 1,
            "comment": "6_s52",
            "orderDetails": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T11:11:06",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T11:11:06"
        },
        {
            "no": "33",
            "clientName": "bz",
            "signDate": "2020-02-27T00:00:00",
            "status": 0,
            "comment": "111",
            "orderDetails": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T13:36:08",
            "updateUserNo": "fizz",
            "updateUserDate": "2020-08-13T14:34:42"
        },
        {
            "no": "34",
            "clientName": "le",
            "signDate": "2017-02-27T00:00:00",
            "status": 1,
            "comment": "6_s52",
            "orderDetails": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:33:52",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:33:52"
        }
    ]
}
```




### 4.10获取一个用户下的所有明细 

- 获取某个用户所有明细：

  `http://{host}/Fizz/DetailUser`

  header: userName

**请求方式：**

- Get

**返回参数示例:**

```json
{
    "pageCount": 1,
    "pageNum": 1,
    "pageItems": [
        {
            "orderNo": "33",
            "rowNo": 4,
            "materialNo": "300",
            "amount": 200.0,
            "unit": "10",
            "sortNo": null,
            "comment": "6_s52",
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:37:54",
            "updateUserNo": "fizz",
            "updateUserDate": "2020-08-13T14:48:52"
        },
        {
            "orderNo": "33",
            "rowNo": 5,
            "materialNo": "300",
            "amount": 29.0,
            "unit": "38",
            "sortNo": null,
            "comment": null,
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:41:52",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:41:52"
        },
        {
            "orderNo": "33",
            "rowNo": 6,
            "materialNo": "300",
            "amount": 29.0,
            "unit": "38",
            "sortNo": null,
            "comment": null,
            "order": null,
            "createUserNo": "s52",
            "createUserDate": "2020-08-13T14:41:56",
            "updateUserNo": "s52",
            "updateUserDate": "2020-08-13T14:41:56"
        }
    ]
}
```

### 4.11删除一个用户下的所有明细 

- 删除某个用户所有明细：

  `http://{host}/Fizz/DetailUser`

  header: userName

**请求方式：**

- Delete

**返回参数示例:**

```json
""
```

