using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class AppListener : MonoBehaviour
{

    public Game g;
    // Use this for initialization
    void Start()
    {
        PurchaseManager.OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable; ;
        PurchaseManager.PurchaseFailed += PurchaseManager_PurchaseFailed;
    }

    private void PurchaseManager_PurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    {
        Debug.Log("Buy: " + args.purchasedProduct.definition.id);
        if (args.purchasedProduct.definition.id == "gold10") BuyGold(10);
        if (args.purchasedProduct.definition.id == "gold50") BuyGold(50);
        if (args.purchasedProduct.definition.id == "gold100") BuyGold(100);
        if (args.purchasedProduct.definition.id == "gold250") BuyGold(250);
        if (args.purchasedProduct.definition.id == "gold500") BuyGold(500);
        if (args.purchasedProduct.definition.id == "gold1000") BuyGold(1000);
        if (args.purchasedProduct.definition.id == "gold2000") BuyGold(2000);
        if (args.purchasedProduct.definition.id == "gold5000") BuyGold(5000);
        if (args.purchasedProduct.definition.id == "silver10000") BuySilver(10000);
        if (args.purchasedProduct.definition.id == "silver50000") BuySilver(50000);
        if (args.purchasedProduct.definition.id == "silver100000") BuySilver(100000);
        if (args.purchasedProduct.definition.id == "silver500000") BuySilver(500000);
    }

    private void BuyGold(int gold)
    {
        g.gold = g.gold + gold;
    }
    private void BuySilver(int silver)
    {
        g.silver = g.silver + silver;
    }
}
