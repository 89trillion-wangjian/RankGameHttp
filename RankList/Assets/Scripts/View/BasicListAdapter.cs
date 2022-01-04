/*
 * * * * This bare-bones script was auto-generated * * * *
 * The code commented with "/ * * /" demonstrates how data is retrieved and passed to the adapter, plus other common commands. You can remove/replace it once you've got the idea
 * Complete it according to your specific use-case
 * Consult the Example scripts if you get stuck, as they provide solutions to most common scenarios
 * 
 * Main terms to understand:
 *		Model = class that contains the data associated with an item (title, content, icon etc.)
 *		Views Holder = class that contains references to your views (Text, Image, MonoBehavior, etc.)
 * 
 * Default expected UI hiererchy:
 *	  ...
 *		-Canvas
 *		  ...
 *			-MyScrollViewAdapter
 *				-Viewport
 *					-Content
 *				-Scrollbar (Optional)
 *				-ItemPrefab (Optional)
 * 
 * Note: If using Visual Studio and opening generated scripts for the first time, sometimes Intellisense (autocompletion)
 * won't work. This is a well-known bug and the solution is here: https://developercommunity.visualstudio.com/content/problem/130597/unity-intellisense-not-working-after-creating-new-1.html (or google "unity intellisense not working new script")
 * 
 * 
 * Please read the manual under "Assets/OSA/Docs", as it contains everything you need to know in order to get started, including FAQ
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using Controller;
using Entity;
using frame8.Logic.Misc.Other.Extensions;
using Model;
using UnityEngine;
using UnityEngine.UI;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace View
{
    // There are 2 important callbacks you need to implement, apart from Start(): CreateViewsHolder() and UpdateViewsHolder()
    // See explanations below
    public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
    {
        // Helper that stores data and notifies the adapter when items count changes
        // Can be iterated and can also have its elements accessed by the [] operator
        public SimpleDataHelper<MyListItemModel> Data { get; private set; }


        #region OSA implementation

        protected override void Awake()
        {
            Data = new SimpleDataHelper<MyListItemModel>(this);

            // Calling this initializes internal data and prepares the adapter to handle item count changes
            base.Awake();

            // Retrieve the models from your data source and set the items count
            int count = RankModel.CreateInstance().JsonList.Count;
            RetrieveDataAndUpdate(count);
        }

        // This is called initially, as many times as needed to fill the viewport, 
        // and anytime the viewport's size grows, thus allowing more items to be displayed
        // Here you create the "ViewsHolder" instance whose views will be re-used
        // *For the method's full description check the base implementation
        protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
        {
            var instance = new MyListItemViewsHolder();

            // Using this shortcut spares you from:
            // - instantiating the prefab yourself
            // - enabling the instance game object
            // - setting its index 
            // - calling its CollectViews()
            instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

            return instance;
        }

        // This is called anytime a previously invisible item become visible, or after it's created, 
        // or when anything that requires a refresh happens
        // Here you bind the data from the model to the item's views
        // *For the method's full description check the base implementation
        protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
        {
            // In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
            // index of item that should be represented by this views holder. You'll use this index
            // to retrieve the model from your data set

            MyListItemModel model = Data[newOrRecycled.ItemIndex];
            if (model == null)
            {
                return;
            }

            newOrRecycled.LevelImg.sprite =
                Resources.Load<Sprite>($"rank_icon/arenaBadge_{(Convert.ToInt32(model.Trophy) / 1000 + 1)}");
            if (newOrRecycled.LevelImg.sprite != null)
            {
                newOrRecycled.LevelImg.rectTransform.sizeDelta = new Vector2(newOrRecycled.LevelImg.sprite.rect.width,
                    newOrRecycled.LevelImg.sprite.rect.height);
                newOrRecycled.UserName.text = model.NickName;
                newOrRecycled.CupCountTxt.text = model.Trophy;
                if (model.Ranking <= 3)
                {
                    newOrRecycled.Rankimg.gameObject.SetActive(true);
                    newOrRecycled.RankNumTxt.gameObject.SetActive(false);
                    newOrRecycled.Rankimg.sprite =
                        Resources.Load<Sprite>($"ranking/rank_{model.Ranking}");
                    if (newOrRecycled.Rankimg.sprite != null)
                        newOrRecycled.Rankimg.rectTransform.sizeDelta = new Vector2(
                            newOrRecycled.Rankimg.sprite.rect.width,
                            newOrRecycled.Rankimg.sprite.rect.height);
                }
                else
                {
                    newOrRecycled.Rankimg.gameObject.SetActive(false);
                    newOrRecycled.RankNumTxt.gameObject.SetActive(true);
                    newOrRecycled.RankNumTxt.text = model.Ranking + "";
                }
            }

            newOrRecycled.rankItemController.SetData(model);
        }

        #endregion


        #region data manipulation

        public void AddItemsAt(int index, IList<MyListItemModel> items)
        {
            Data.InsertItems(index, items);
        }

        public void RemoveItemsFrom(int index, int count)
        {
            Data.RemoveItems(index, count);
        }

        public void SetItems(IList<MyListItemModel> items)
        {
            Data.ResetItems(items);
        }

        #endregion


        // Here, we're requesting <count> items from the data source
        void RetrieveDataAndUpdate(int count)
        {
            StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
        }

        // Retrieving <count> models from the data source and calling OnDataRetrieved after.
        // In a real case scenario, you'd query your server, your database or whatever is your data source and call OnDataRetrieved after
        IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
        {
            // Simulating data retrieving delay
            yield return new WaitForSeconds(.5f);

            var newItems = new MyListItemModel[count];

            // Retrieve your data here


            List<JsonModel> json = RankModel.CreateInstance().JsonList;

            for (int i = 0; i < count; ++i)
            {
                var model = new MyListItemModel()
                {
                    ID = json[i].id,
                    NickName = json[i].nickName,
                    Avatar = json[i].avatar,
                    Trophy = json[i].trophy,
                    Ranking = i + 1
                };
                newItems[i] = model;
            }


            OnDataRetrieved(newItems);
        }

        void OnDataRetrieved(MyListItemModel[] newItems)
        {
            Data.InsertItemsAtEnd(newItems);
        }
    }

    // Class containing the data associated with an item
    public class MyListItemModel
    {
        public string ID;
        public string NickName;
        public string Avatar;
        public string Trophy;
        public int Ranking;
    }


    // This class keeps references to an item's views.
    // Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
    public class MyListItemViewsHolder : BaseItemViewsHolder
    {
        public RankItemController rankItemController;

        public GameObject gameObject;

        public Image LevelImg;

        public Text UserName;

        public Text CupCountTxt;

        public Text RankNumTxt;

        public Image Rankimg;

        public Image Bg;

        // Retrieving the views from the item's root GameObject
        public override void CollectViews()
        {
            base.CollectViews();


            root.GetComponentAtPath("levelImg", out LevelImg);
            root.GetComponentAtPath("userName", out UserName);
            root.GetComponentAtPath("cupBg/cupCountTxt", out CupCountTxt);
            root.GetComponentAtPath("ranking/rankNumTxt", out RankNumTxt);
            root.GetComponentAtPath("ranking/rankimg", out Rankimg);
            root.GetComponentAtPath("bg", out Bg);
            rankItemController = root.GetComponent<RankItemController>();
            gameObject = root.gameObject;
        }
    }
}