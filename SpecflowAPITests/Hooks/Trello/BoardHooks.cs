using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
namespace SpecflowAPITests.Hooks.Trello
{
    class BoardHooks
    {
        private Helper _helper;
        public BoardHooks(Helper helper)
        {
            _helper = helper;
        }

        [AfterScenario(Order = 100)]
        [Scope(Tag = "deleteTrelloBorad")]

        public void DeleteBoard()
        {
            foreach (string id in _helper.GetIds())
            {
                var request = new TrelloRequest("boards/" + id);
                RequestManager.Delete(TrelloClient.GetInstance(), request);
            }
        }
    }
}
