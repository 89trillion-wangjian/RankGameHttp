## 1. 整体框架

		根据httpClient请求接口数据，并用simpleJson解析

## 2. 项目结构

* scene
  * MainScene
* Scripts
  * SimpleJson               //解析json文件
  * httpClient                //接口请求框架
* Resources
  * Rank_icon				             //段位图标
  * image                       //界面ui
* Prefabs                            //存储prefab
* OSA                                  //osa插件目录

## 3.代码逻辑分层
| 文件夹     | 主要职责                 |
| ---------- | ------------------------ |
| Resources  | 存放资源                 |
| Scripts    | 存放脚本文件             |
| Prefabs    | 存放预制体资源           |
| data       | 存放本地存放的json数据等 |
| Scene      | 存放场景文件             |
| SimpleJSON | 存放解析json的工具       |
| OSA        | osa插件目录              |

## 4. 流程图

```flow
st=>start: 开始
op=>operation: 请求接口数据
cond=>condition: 请求成功(是或否?)
sub1=>subroutine: 再次请求
io=>inputoutput: 输入输出框
op1=>operation: 根据接口数据创建榜单
op2=>operation: OSA解析
op4=>operation: 展示榜单
e=>end: 结束
st->op->cond
cond(yes)->op1->op2->op4->e
cond(no)->sub1(right)->op
```