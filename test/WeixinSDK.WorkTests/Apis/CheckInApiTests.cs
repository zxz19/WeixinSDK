﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WeixinSDK.Work;
using WeixinSDK.Work.Common;
using WeixinSDK.Work.Enums;
using WeixinSDK.Work.Models.CheckIn;

namespace WeixinSDK.WorkTests.Apis
{
    [TestClass()]
    public class CheckInApiTests
    {
        private WeixinWorkApiClient _client;

        [TestInitialize]
        public void InitTest()
        {
            _client = new WeixinWorkApiClient(
                System.Configuration.ConfigurationManager.AppSettings["WeixinWork_CorpId"],
                System.Configuration.ConfigurationManager.AppSettings["WeixinWork_CorpSecret"]);

            _client.DumpRequest += info => { Console.WriteLine(JsonConvert.SerializeObject(info)); };
        }

        [TestMethod()]
        public void GetCheckInDataTest()
        {
            var result = _client.CheckInApi.GetCheckInData(new GetCheckInDataRequest()
            {
                opencheckindatatype = 3,
                starttime = DateTime.Today.AddDays(-DateTime.Today.Day + 1).ToUnixTime(),
                endtime = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(1).ToUnixTime(),
                useridlist = new List<string>() {"test"}
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.IsTrue(result.checkindata.Count > 0);
        }
    }
}