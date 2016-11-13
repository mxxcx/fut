﻿using System;

namespace EvFutBot.Models
{
    public class Settings
    {
        private readonly byte[] _rpmDelayRange;
        private readonly Random _rand;

        public Settings(byte[] runforHours, byte[] rpmDelay, byte buyPercent, byte sellPercent,
            byte maxAccounts, byte batch, uint maxCredits, byte securityDelay, byte maxCardCost,
            byte lowestBinNr)
        {
            _rand = new Random();

            BinPercent = buyPercent;
            BidPercent = buyPercent;
            SellPercent = sellPercent;
            MaxAccounts = maxAccounts;
            Batch = batch;
            MaxCredits = maxCredits;
            MaxCardCost = maxCardCost;
            LowestBinNr = lowestBinNr;
            SecurityDelay = securityDelay*1000;

            if (rpmDelay[0] < 4)
                throw new ArgumentOutOfRangeException(nameof(rpmDelay), "RPM Delay to small");

            _rpmDelayRange = rpmDelay;
            PreBidDelay = 0; // no delay
            RunforHours = _rand.Next(runforHours[0], runforHours[1]);
            RmpDelayPrices = Convert.ToInt32(60/((decimal) 4900/RunforHours/60)*1000); // 5000 request limit per day
        }

        public int RunforHours { get; }
        public byte BinPercent { get; private set; }
        public byte BidPercent { get; private set; }
        public byte SellPercent { get; private set; }
        public byte MaxAccounts { get; private set; }
        public byte Batch { get; private set; }
        public uint MaxCredits { get; private set; }
        public byte MaxCardCost { get; set; }
        public byte LowestBinNr { get; set; }
        public int SecurityDelay { get; private set; }

        public int RmpDelay => _rand.Next(_rpmDelayRange[0]*1000, _rpmDelayRange[1]*1000);
        public int RmpDelayLow => _rand.Next(3*1000, 9*1000);
        public int RmpDelayPrices { get; private set; }
        public int PreBidDelay { get; private set; }
    }
}