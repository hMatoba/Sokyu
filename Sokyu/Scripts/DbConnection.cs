using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.IO;
using System.Collections.Generic;
using System;

public static class DbConnection
{
    public static IMongoDatabase db;

    public static void Connect()
    {
        var client = new MongoClient("mongodb://192.168.11.7");
        db = client.GetDatabase("test");
    }
}
