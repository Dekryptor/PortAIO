﻿namespace Valvrave_Sharp
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using EloBuddy;
    using Valvrave_Sharp.Core;
    using Valvrave_Sharp.Plugin;
    using EloBuddy.SDK.Menu;
    #endregion

    internal class Program
    {
        #region Constants

        internal const int FlashRange = 425, IgniteRange = 600, SmiteRange = 570;

        #endregion

        #region Static Fields

        internal static SpellSlot Flash, Ignite, Smite;

        internal static Menu _MainMenu;

        internal static AIHeroClient Player;

        internal static Spell Q, Q2, Q3, W, E, E2, R, R2;

        private static readonly Dictionary<string, Func<object>> Plugins = new Dictionary<string, Func<object>>
                                                                               {
                                                                                   //{ "DrMundo", () => new DrMundo() },
                                                                                   //{ "Kennen", () => new Kennen() },
                                                                                   { "LeeSin", () => new LeeSin() },
                                                                                   //{ "Lucian", () => new Lucian() },
                                                                                   { "Yasuo", () => new Yasuo() },
                                                                                   { "Zed", () => new Zed() }
                                                                               };

        #endregion

        #region Methods

        private static void InitMenu(bool isSupport)
        {
            _MainMenu = MainMenu.AddMenu("ValvraveSharp", "Valvrave Sharp");
            if (isSupport)
            {
                Plugins[Player.ChampionName].Invoke();
            }
        }

        private static void InitSummonerSpell()
        {
            var smiteName = Player.Spellbook.Spells.Where(i => (i.Slot == SpellSlot.Summoner1 || i.Slot == SpellSlot.Summoner2) && i.Name.ToLower().Contains("smite")).Select(i => i.Name).FirstOrDefault();
            if (!string.IsNullOrEmpty(smiteName))
            {
                Smite = Player.GetSpellSlot(smiteName);
            }
            Ignite = Player.GetSpellSlot("SummonerDot");
            Flash = Player.GetSpellSlot("SummonerFlash");
        }

        public static void MainA()
        {
            Player = ObjectManager.Player;
            var isSupport = Plugins.ContainsKey(Player.ChampionName);
            InitMenu(isSupport);
            InitSummonerSpell();
        }

        private static void PrintChat(string text)
        {
            Chat.Print("Valvrave Sharp => {0}", text);
        }

        #endregion
    }
}