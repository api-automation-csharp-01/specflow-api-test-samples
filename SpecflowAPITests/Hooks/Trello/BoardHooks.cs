using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

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

        [AfterScenario(Order = 100)]
        [Scope(Tag = "deleteTrelloBoard")]
        public void DeleteBoard()
        {
            foreach(string id in _helper.GetIds())
            {
                var request = new TrelloRequest(resource: "boards/" + id);
                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }
}
