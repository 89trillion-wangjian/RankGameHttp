## 1. 整体框架

		根据httpClient请求接口数据，controller中用simpleJson解析，通过OSA创建scroll view，优化滑动列表节点问题，view中控制item的显示，点击item弹出全局toast提示

## 2. 项目结构

* Scene
  * MainScene
* Scripts
* Resources
  * Rank_icon				#段位图标
  * RankLevel                       	#排名图标
* Prefabs                            	#存储prefab
* OSA                                   #osa插件目录
* Res					#静态资源

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
![](https://github.com/89trillion-wangjian/RankGameHttp/blob/master/seq.png)
