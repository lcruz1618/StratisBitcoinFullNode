﻿using NBitcoin;
using Stratis.Bitcoin;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Features.Api;
using Stratis.Bitcoin.Features.BlockStore;
using Stratis.Bitcoin.Features.MemoryPool;
using Stratis.Bitcoin.Features.Notifications;
using Stratis.Bitcoin.Features.RPC;
using Stratis.Bitcoin.Features.SmartContracts;
using Stratis.Bitcoin.Features.SmartContracts.Wallet;
using Stratis.Bitcoin.IntegrationTests.Common.Runners;

namespace Stratis.Features.FederatedPeg.IntegrationTests.Utils
{
    public class SidechainWithSmartContractsNodeRunner : NodeRunner
    {
        public SidechainWithSmartContractsNodeRunner(string dataDir, string agent, Network network)
            : base(dataDir, agent)
        {
            this.Network = network;
        }

        public override void BuildNode()
        {
            var settings = new NodeSettings(this.Network, args: new string[] { "-conf=poa.conf", "-datadir=" + this.DataFolder });

            this.FullNode = (FullNode)new FullNodeBuilder()
                .UseNodeSettings(settings)
                .UseBlockStore()
                .AddSmartContracts()
                .UseSmartContractWallet()
                .UseReflectionExecutor()
                .AddFederationGateway()
                .UseFederatedPegPoAMining()
                .UseMempool()
                .UseTransactionNotification()
                .UseBlockNotification()
                .UseApi()
                .AddRPC()
                .Build();
        }
    }
}
