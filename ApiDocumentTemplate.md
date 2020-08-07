# First.Fizz.Order
# First.Fizz.Order

[RFC7807]: https://tools.ietf.org/html/rfc7807#page-5

## 1. 资源定义的规则
- **资源Url** `https://{host}/{大分类}/{小分类}/{资源名称}`

## 2. 资源通用属性

| 名称                 | 描述           |类型             | 说明                              |
| -------------------- | -------------- | -------------- |--------------------------------- |
| id       | 资源主键         |long| 只读                              |
| createByUserId       | 创建人         |long| 只读                              |
| createDateTime       | 创建时间       |DateTime| 只读                              |
| lastUpdateByUserId   | 最新更新者     |long| 只读                              |
| lastUpdateDateTime   | 最新更新时间   |DateTime| 只读                              |
| thirdPartyId         | 接入方唯一ID   |string |必须是唯一的ID  最大长度：50      |
| thirdPartyExtensions | 接入方扩展属性 |string | 扩展属性必须是Json 最大长度：2000 |
| rowVersion | 资源的版本号 |int | 调用`put`API是不要传rowVersion |
- long类型都会转成字符串
- DateTime 使用Utc格式 --& 2009-06-15T13:45:30.0000000Z
- 资源属性命名规则使用驼峰式

## 3. 请求通用Header说明

|名称 |说明|示例|
|:-----  |:-----|----- |
|ContextType |国定值:Application/json   | |
|X-Api-Version |Api版本号   |1 |
|X-Request-Id |每次请求的唯一Id   |201901011234123456 |

## 4. 返回值说明

|Status Code |类型|说明|
|:-----  |:-----|----- |
|200 |业务对象   |调用成功 |
|204 |无   |调用成功 |
|400 |Error   |获取失败 |
|401 |无   |身份验证失败 |
|403 |无   |禁止访问 |

## 5. 资源CRUD
### 5.1 获取资源
**简要描述：** 

- 根据资源Id获取资源

**URL示例：** 
- ` https://{host}/Oms/Ord/SalesOrder/{Id} `
  
**请求方式：**
- Get 

**返回参数示例：**
``` 
  {
    "id": "300936932001619968"
    "no": "GYS002",
    "supplierName": "XXXXXX公司",
    "customerOrderNo": "CON20190101000001",
    "projectName": "XXXX项目",
    "thirdPartyId": "2019010100001"
  }
```

 **返回值说明** 

|Status Code |类型|说明|
|:-----  |:-----|----- |
|200 |业务对象   |获取成功 |
|400 |Error   |获取失败 |
### 5.2 创建资源
**简要描述：** 

- 创建资源

**URL示例：** 
- ` https://{host}/Oms/Ord/SalesOrder `
  
**请求方式：**
- POST 

**请求参数示例：**
``` 
  {
    "no": "GYS002",
    "supplierName": "XXXXXX公司",
    "customerOrderNo": "CON20190101000001",
    "projectName": "XXXX项目",
    "thirdPartyId": "2019010100001"
  }
```

 **返回值说明** 

|Status Code |类型|说明|
|:-----  |:-----|----- |
|200 |long   |资源Id |
|400 |Error   |创建失败 |

### 5.3 修改资源

    
**简要描述：** 

- 修改资源

**URL示例：** 
- ` https://{host}/Oms/Ord/SalesOrder/{Id} `
  
**请求方式：**
- Put 

**请求参数示例：**
``` 
  {
    "no": "GYS002",
    "supplierName": "XXXXXX公司",
    "customerOrderNo": "CON20190101000001",
    "projectName": "XXXX项目",
    "thirdPartyId": "2019010100001"
  }
```

 **返回值说明** 

|Status Code |类型|说明|
|:-----  |:-----|----- |
|204 |无   |没有返回内容 |
|400 |Error   |修改失败 |

### 5.4 删除资源
**简要描述：** 

- 删除资源

**URL示例：** 
- ` https://{host}/Oms/Ord/SalesOrder/{Id} `
  
**请求方式：**
- Delete 

**返回值说明** 

|Status Code |类型|说明|
|:-----  |:-----|----- |
|204 |无   |删除成功，没有返回内容 |
|400 |Error   |删除失败 |
### 5.5 查询资源
**简要描述：** 

- 查询资源

**URL示例：** 
- ` https://{host}/Oms/Ord/SalesOrder/{Id}?limit=10&amp;offset=0`
  
**请求方式：**
- Get 

**请求参数说明：**

|参数名 |类型|说明|
|:-----  |:-----|----- |
|limit |int   |查询最大数据量<br&limit不设置时服务器回返回默认的limit |
|offset |int   |查询数据偏移量 <br&offset未设置可以获得该查询的总数据量|
**翻页的实现**
- 第一次查询offset不要设置
- 获取count后，通过limit和count计算总页数
- offset=limit * pageIndex

**返回参数示例：**
```json
  {
    "count":100,
    "items": [
        {
            "no": "GYS002",
            "supplierName": "",
            "customerOrderNo": "",
            "projectName": "",
            "tendersBatchNo": "",
            "status": 1,
            "remark": "",
            "syncSourceAddress": "",
            "id": "300936932001619968",
            "rowVersion": 1
        }
    ],
    "offset": 0,
    "limit": 10,
  }
```

 **返回参数说明** 

|Status Code |类型|说明|
|:-----  |:-----|----- |
|200 |QueryResult   |查询成功|
|400 |Error   |查询失败 |
**QueryResult参数说明**

|参数名 |类型|说明|
|:-----  |:-----|----- |
|count |int   |查询到的业务对象数据量只有<br&当Offset为设置时，count才会有值|
|limit |int   |查询最大数据量 |
|offset |int   |查询数据偏移量 |
|items|object[]|业务对象数组|
## 6.  返回错误类型Error
调用API `StatusCode:400`表示发生业务异常
异常类型的设计符合[RFC 7807规范][RFC7807]  
```json
{
    "type":"NotFound",
    "title": "销售订单不存在",
    "status": 400
}
```
### 6.1 标准Error Type
|Type |说明|
|:-----  |:-----|
|NotFound |资源不存在   |
|NoDuplicate |编号已经存在   |
|NotAllowedDelete |资源不可删除   |
|NotAllowedEdit |资源不可编辑  |
