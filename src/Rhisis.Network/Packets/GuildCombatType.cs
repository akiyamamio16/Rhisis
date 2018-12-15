﻿namespace Rhisis.Network.Packets
{
    public enum GuildCombatType
    {
        GC_WINGUILD = 0x00,
        GC_IN_WINDOW = 0x01,
        GC_IN_APP = 0x02,
        GC_IN_COMPLETE = 0x03,
        GC_REQUEST_STATUS = 0x04,
        GC_SELECTPLAYER = 0x05,
        GC_SELECTWARPOS = 0x06,
        GC_BESTPLAYER = 0x07,
        GC_ISREQUEST = 0x08,
        GC_USERSTATE = 0x10,
        GC_WARPLAYERLIST = 0x11,
        GC_GUILDSTATUS = 0x20,
        GC_GUILDPRECEDENCE = 0x21,
        GC_PLAYERPRECEDENCE = 0x22,
        GC_GCSTATE = 0x30,
        GC_NEXTTIMESTATE = 0x31,
        GC_ENTERTIME = 0x32,
        GC_DIAGMESSAGE = 0x33,
        GC_TELE = 0x34,
        GC_LOG = 0x35,
        GC_LOG_REALTIME = 0x36,
        GC_GETPENYAGUILD = 0x40,
        GC_GETPENYAPLAYER = 0x41,
        GC_PLAYERPOINT = 0x42
    }
}
