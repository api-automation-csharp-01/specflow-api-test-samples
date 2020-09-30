using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;

namespace SpecflowAPITests.Hooks.Trello
{
    [Binding]
    public class BoardHooks
    {
        private Helper _helper;
        public BoardHooks(Helper helper)
        {
            _helper = helper;
        }

        [AfterScenario (Order = 80)]
        [Scope(Tag = "deleteTrelloBoard")]

        public void DeleteBoard()
        {
            foreach(string id in _helper.GetIds())
            {
                var request = new TrelloRequest("boards/" + id);
                request.GetRequest().AddJsonBody("{\"colsed\":\"true\"}");
                RequestManager.Put(TrelloClient.GetInstance(), request);
                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }

}
