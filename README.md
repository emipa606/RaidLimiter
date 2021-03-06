# RaidLimiter

![Image](https://i.imgur.com/WAEzk68.png)

Update of Greeps mod
https://ludeon.com/forums/index.php?topic=42721.0

- The settings are now editable in game. Will only use the xml-based values on first run.

![Image](https://i.imgur.com/7Gzt3Rg.png)


[table]
    [tr]
        [td]https://invite.gg/Mlie]![Image](https://i.imgur.com/zdzzBrc.png)
[/td]
        [td]https://github.com/emipa606/RaidLimiter]![Image](https://i.imgur.com/kTkpTOE.png)
[/td]
    [/tr]
[/table]
	
![Image](https://i.imgur.com/NOW7jU1.png)

Have mods that wildly inflate your wealth?
Don&apos;t like Tynan&apos;s vision of infinitely ramping up raids?
Just a wimp?

Then download raid size limiter now! 

Here&apos;s what the settings do (To change them, open the mod-folder, then defs, and edit settings:

-CapByDifficultySettings: Leave this on &quot;YES&quot; if you want my raid system to pick a value based off your storyteller&apos;s difficulty

-CapByDifficultySettingsMultiplier: Change this number to the base value multiplied by your difficulty.  At 4000, the starting multiplier, this means on rough you will end up with 4000 raid point cap, on medium a cap of 2500 or so.

-RaidCap: If you just want to set it manually, change -1 to a number.  1600 is what v2 uses.

-RaidPointsMultiplier:  This is not a cap, but just a straight multiplier on top of your difficulty.  Maybe you want Rough, but slightly less rough?  In that case you can choose rough with your storyteller and choose 0.9 here for example.  Or maybe you want extreme&apos;s other effects but not it&apos;s raid sizes?  Choose extreme and 0.5 here.

-RaidCapPointsPerColonist: Does what it says.  Change from -1 to a number you want if you want to use this and your raid cap will be based off your number of colonists.  If you have 25 colonists and you set this to 10, your raids will be capped at 2500.

-Colonist Multiplier:  The base game adds points for every colonist you recruit.  Maybe you don&apos;t want that!  Change from 1 to 0 and colonists have no impact on raid points.  Or choose some positive multiplier for it to work the way you want.

-Combat animal Multiplier: Same

-Wealth multiplier: Same.  but be careful of tweaking this too much as most of your points come from wealth.

SoftCaps and adaptation probably shouldn&apos;t be touched unless you like math ;)

Soft caps basically have a soft cap at a certain raid value rather than completely cutting it off
-SoftCapBeginTapering:  Change this from -1 to a raid point amount to start considerably slowing raid points rather than capping them at this value
-SofSoftCapExponent: this is the exponent that will be used if you use this. 
Example: with 2000 and 0.9, if you had 3000 raid points, it would instead give 2000 + 1000^.9 points

Adaptation tags change the in game ramping up.  The game basically punishes you for flawless victories, these can soften (or worsen, but whyyyy?) that effect.  The cap you&apos;re changing is the multiplier on raid points from adaptation.
-AdaptationCap: I default this to being on.  This caps the adaptation so you&apos;re never getting raids 50% higher than you otherwise would have.  Change it to -1 to let the game do what it does normally, change it to 1 to completely get rid of adaptation altogether.
AdaptationSoftcap/exponent.  These work just like the raidsoftcaps only for adaptation.  These really aren&apos;t useful.  I don&apos;t know why I added them lol, since the game already massively slows down adaptation after a point.  If you want to use it knock yourself out.

Debug set to anything but YES will shut up the log warnings but you probably want them.

The Logger should give warning logs for every active setting except some of the per item multipliers using a &quot;before&quot; and &quot;after&quot; so you can see how your tags effect the overall raid points.

![Image](https://i.imgur.com/Rs6T6cr.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using https://steamcommunity.com/workshop/filedetails/?id=818773962]HugsLib and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.



