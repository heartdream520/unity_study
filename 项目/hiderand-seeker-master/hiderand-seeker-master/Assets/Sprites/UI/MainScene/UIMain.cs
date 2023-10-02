
using System;
using UnityEngine;
namespace MainScene
{
    public class UIMain : MonoSingleton<UIMain>
    {
        public GameObject coinPanel;
        public GameObject loadingPanel;
        private bool oneGameIsEnd;
        private bool isFirst;
        public override void OnAwake()
        {
            EventManager.Instance.OnSelectedHiderChangeAction += this.OnSelectedHiderChange;
            EventManager.Instance.OneGameEndAction += this.OnOneGameEnd;
        }


        private void OnDestroy()
        {
            EventManager.Instance.OnSelectedHiderChangeAction -= this.OnSelectedHiderChange;
            EventManager.Instance.OneGameEndAction -= this.OnOneGameEnd;

        }


        private void OnOneGameEnd(bool iswin)
        {
            oneGameIsEnd = true;
        }

        private void OnSelectedHiderChange(string name)
        {
            MainUICharacterModleManager.Instance.SetActiveModle(
                name
                );
        }

        private void Start()
        {
            isFirst = true;
            coinPanel.SetActive(true);

            MainUICharacterModleManager.Instance.SetActiveModle(
                GameDataManager.Instance.charactersManager.SelectedHider_String
                );
        }
        
        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        public void OnChickBeginGameButton()
        {

            AudioManager.Instance.PlayOnChickButton();

            if (isFirst)
            {
                isFirst = false;
                GameingMainManager.Instance.BeginOneNewGame();
                InvokeManager.Instance.InvokeOneActiveAfterSeconds(() =>
                {
                    this.gameObject.SetActive(false);
                }, 0.3f);
                //UIManager.Instance.Show<UISelectedIdentity>();
            }
            else
            {
                this.gameObject.SetActive(false); 
            }
            
        }

        public void OnchickTestUIManagerButton()
        {
            AudioManager.Instance.PlayOnChickButton();

            UIManager.Instance.Show<UISelectedIdentity>();

        }
        public void OnChickCharacterShopButton()
        {
            AudioManager.Instance.PlayOnChickButton();

            UIManager.Instance.Show<UICharacterShop>();
        }
        public void Loading()
        {
            loadingPanel.SetActive(true);
        }
        public void OnChickSetMainButton()
        {
            AudioManager.Instance.PlayOnChickButton();
            UIManager.Instance.Show<UISettingMain>();
        }

        
    }

}
