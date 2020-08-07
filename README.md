# First.Fizz.Order

## 1. 资源定义规则

- **资源URL**  `http://{host}/{大分类}/{小分类}/{资源名称}`   

## 2. 请求通用Header说明 ##

| 名称        | 说明                     | 示例 |
| ----------- | ------------------------ | ---- |
| ContextType | 固定值：Application/json |      |
| userName    | 租户名                   | fizz |

## 3. 返回值说明 ##

| Status Code | 类型     | 说明     |
| ----------- | -------- | -------- |
| 200         | 业务对象 | 调用成功 |
|             | Error    | 获取失败 |

## 4. 资源CURD

### 5.1 获取资源

**简要描述： **

- 根据资源id获取订单和明细资源

**URL示例：**

- 获取销售订单：`http://{host}/Fizz/Sales/Order?orderNo={orderNo}&pageSize={pageSize}&pageNum={pageNum}&findType={findType}`

  findType: GetAll = 1, GetByKey = 2, GetByUser = 3

  http method: get

  header: userName

- 新增订单：`http://{host}/Fizz/Sales/Order`

  http method: post

  header: userName

  request body:

  {

   "no":"2",

   "comment":"6_s52",

   "signdate":"2017-02-27"

  }

  response:

  

- 更新订单：`http://{host}/Fizz/Sales/Order/{orderNo}`

  http method: put

  header: userName

  {

   "comment":"6_s52",

   "amount":200,

   "unit":"10"

  }

  

- 删除订单：`http://{host}/Fizz/Sales/Order?orderno={orderNo}?deleType={deleteType}`

  deleteType: DeleteAll = 1, DeleteByKey = 2, DeleteByUser = 3

  http method: delete

  header: userName

  

- 获取订单明细：`http://{host}/Fizz/Sales/OrderDetail?orderNo={orderNo}&detailNo={detailNo}&pageSize={pageSize}&pageNum={pageNum}&findType={findType}`

  findType:  GetAll = 1, GetByKey = 2, GetByUser = 3, GetByOrder = 4

  http method: get

  header: userName

  

- 订单明细的新增：`http://{host}/Fizz/Sales/OrderDetail/{orderNo}`

  http method:post

  header: userName

  request body:

  {

    "materialno":"300",

    "amount":29,

    "unit":"38"

  }

- 订单明细的删除：`http://{host}/Fizz/Sales/OrderDetail?orderNo={orderNo}&detailNo={detailNO}&deleteType={deleteType}`

  deleteType: DeleteAll = 1, DeleteByKey = 2, DeleteByUser = 3, DeleteByOrder = 4

  http method: delete

  header: userName

  

- 订单明细的修改：`http://{host}/Fizz/Sales/OrderDetail/{orderNo}/{detailNo}`

  http method: put

  header: userName

  request body:

  {

   "comment":"6_s52",

   "amount":200,

   "unit":"10"

  }

  

 