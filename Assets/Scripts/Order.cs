using UnityEngine;
using System.Collections.Generic;

public class Order
{
    // Will be created by the OrderManager script with the following attributes: Customer, string, Coordinates, Price, Status
    public string customer;
    public string quadrant;
    public Vector2 coordinates;
    public float price;
    public string status;
    public float expirationTime;
    public float coldTime;
    public List<List<string>> orderList;

    // Will be created by the OrderManager script with the following attributes: string, Quadrant, Coordinates, Price, Status
    public Order(string customer, string quadrant, Vector2 coordinates, float price, string status, float expirationTime, float coldTime, List<List<string>> orderList)
    {
        this.customer = customer;
        this.quadrant = quadrant;
        this.coordinates = coordinates;
        this.price = price;
        this.status = status;
        this.expirationTime = expirationTime;
        this.coldTime = coldTime;
        this.expirationTime = expirationTime;
        this.coldTime = coldTime;
        this.orderList = orderList;
    }

    public string Customer
    {
        get { return customer; }
        set { customer = value; }
    }

    public string Quadrant
    {
        get { return quadrant; }
        set { quadrant = value; }
    }


    public Vector2 Coordinates
    {
        get { return coordinates; }
        set { coordinates = value; }
    }
    public float Price
    {
        get { return price; }
        set { price = value; }
    }
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public float ExpirationTime
    {
        get { return expirationTime; }
        set { expirationTime = value; }
    }

    public float ColdTime
    {
        get { return coldTime; }
        set { coldTime = value; }
    }

    public List<List<string>> OrderList
    {
        get { return orderList; }
        set { orderList = value; }
    }

    public void PrintOrderList()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            for (int j = 0; j < orderList[i].Count; j++)
            {
                Debug.Log(orderList[i][j]);
            }
        }
    }

    // This method will be called by the OrderManager script when the order is delivered
    public void OrderDelivered()
    {
        Status = "Delivered";
    }

    // This method will be called by the OrderManager script when the order is cancelled
    public void OrderCancelled()
    {
        Status = "Cancelled";
    }

    // This method will be called by the OrderManager script when the order is expired
    public void OrderExpired()
    {
        Status = "Expired";
    }

    // This method will be called by the OrderManager script when the order is ready
    public void OrderReady()
    {
        Status = "Ready";
    }

    // This method will be called by the OrderManager script when the order is being prepared
    public void OrderBeingPrepared()
    {
        Status = "Being Prepared";
    }

    // This method will be called by the OrderManager script when the order is being delivered
    public void OrderBeingDelivered()
    {
        Status = "Being Delivered";
    }
}