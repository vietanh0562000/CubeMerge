using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
   public void OnPressDown(int i)
   {
      int quantity = PlayerPrefs.GetInt("RocketPowerUp");
      switch (i)
      {
         case 1:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 1);
            };
             IAPManager.Instance.BuyProductID(IAPKey.PACK1);
            break;
         case 2:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 5);
            };
            IAPManager.Instance.BuyProductID(IAPKey.PACK2);
            break;
         case 3:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 10);
            };
            IAPManager.Instance.BuyProductID(IAPKey.PACK3);
            break;
         case 4:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 20);
            };
            IAPManager.Instance.BuyProductID(IAPKey.PACK4);
            break;
         case 5:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 30);
            };
            IAPManager.Instance.BuyProductID(IAPKey.PACK5);
            break;
         case 6:
            IAPManager.OnPurchaseSuccess = () =>
            {
               PlayerPrefs.SetInt("RocketPowerUp", quantity + 40);
            };
            IAPManager.Instance.BuyProductID(IAPKey.PACK6);
            break;
      }
      
      UIManager.instance.UpdatePowerUpText();
      UIManager.instance.CloseShop();
   }

   public void Sub(int i)
   {
      GameDataManager.Instance.playerData.SubDiamond(i);
   }
}
