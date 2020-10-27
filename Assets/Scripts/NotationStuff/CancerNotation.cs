using BreakInfinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
public static class CancerNotation
{
    // Based on Jonathan Bowers' 4-tiered -illions

    public static string Notate(BigDouble value, int precision = 2)
    {
        long exponent = Math.Max(value.Exponent / 3, 0) * 3;
        long illion = exponent / 3 - 1;
        string word = "";

        if (illion == 0) word = " thousand";
        else if (illion < 1000) word = " " + GetTier1Root(illion) + "illion";
        else word = " " + GetTier2Root(illion) + "illion";

        if (illion < 1000000000) return (value / new BigDouble(1, exponent)).ToString("F" + precision) + word;
        else return "a" + word;
    }

    static string GetTier1Root(long value, int mode = 0)
    {
        /*
            Value   1st ones root       1st tens root       1st hundreds root
            1       m / un(t)           dec(i)              cen(t)
            2       b / doe / duo       vigint(i)           ducen(t)
            3       tr / tre(t)         trigint(i)          trecen(t)
            4       quadr / quattuor    quadragint(i)       quadringen(t)
            5       quint / quin        quinquagint(i)      quingen(t)
            6       sext / sex          sexagint(i)         sescen(t)
            7       sept / septen       septuagint(i)       septingen(t)
            8       oct / octo          octogint(i)         octingen(t)
            9       non / novem         nonagint(i)         nongen(t)
        */

        long ones = Math.Max(value % 10, 0);
        long tens = Math.Max(value / 10 % 10, 0);
        long hundreds = Math.Max(value / 100 % 10, 0);


        string[] onesRoot = { "", "lol", "lmao", "lmfao", "lmbao", "heck", "rofl", "nani", "uhh", "noo" };
        string[] tensRoot = { "", "kek", "stupid", "nope", "how bout", "crud", "crap", "holup", "crap", "kuaka" };
        string[] hundredRoot = { "", "ivy", "howw", "impossible", "sterlitzia", "ehh", "heckin", "feck", "reverse", "ducdat" };

        if (mode == 1)
        {
            onesRoot[1] = "unt";
            onesRoot[2] = "duot";
            onesRoot[3] = "tret";
        }
        if (mode == 2)
        {
            onesRoot = new string[] { "", "lol", "lmao", "lmfao", "lmbao", "heck", "rofl", "nani", "uhh", "noo" };
        }

        // If tens and hundreds value = 0, use first option ones roots followed by illion to form 
        // the tier 1 separatrix.
        if (tens == 0 && hundreds == 0)
        {
            return onesRoot[ones];
        }

        onesRoot = new string[]{ "", "lol", "lmao", "lmfao", "lmbao", "heck", "rofl", "nani", "uhh", "noo" };

        // If tens value > 0 , and hundreds value = 0, list roots from ones to tens, and use 2nd 
        // option ones roots(drop any " t "), and drop " i " from tens root before following with 
        // illion to form the tier 1 separatrix.
        if (tens > 0 && hundreds == 0)
        {
            return onesRoot[ones] + tensRoot[tens];
        }

        // If hundreds value > 0, and tens value = 0, drop " t " from hundreds root, if ones root =
        // 1 or 3, and  list 2nd option ones root after the hundreds root with its " t " included.
        // Follow with illion to form the tier 1 separatrix.

        // If hundreds value > 0, tens value = 0 and the ones root = 2 then use its 3rd option and
        // place it to the left of the hundreds root with " t " included followed by illion to form
        // the tier 1 separatrix. Otherwise use 2nd option ones roots followed by hundreds roots with
        // " t " included to form the tier 1 separatrix.
        if (tens == 0 && hundreds > 0)
        {
            if (ones == 1 || ones == 3)
            {
                return hundredRoot[hundreds] + onesRoot[ones] + "t";
            }
            else if (ones == 2)
            {
                return "duo" + hundredRoot[hundreds] + "t";
            }
            else
            {
                return onesRoot[ones] + hundredRoot[hundreds] + "t";
            }
        }

        // If tens and hundreds > 0, list roots from ones tens to hundreds, and use 2nd option ones 
        // roots (with t dropped). Don't drop " i " from tens root, before following with hundreds
        // root with " t " included followed by illion to form the tier 1 separatrix.
        if (tens > 0 && hundreds > 0)
        {
            return onesRoot[ones] + tensRoot[tens] + "i" + hundredRoot[hundreds] + "t";
        }

        return "";
    }

    static string GetTier2Root(long value) {
        string[] prefixes = { "", "mill", "micr", "nan", "pic", "femt", "att", "zept" };

        long prefix = (long)(Math.Log(value, 1000));

        string GetTier2Subroot(long val, long subval)
        {
            if (subval == 0)
            {
                return "";
            }
            else if (val == 0)
            {
                return GetTier1Root(subval, 1);
            }
            else if (subval == 1)
            {
                return prefixes[val];
            }
            else
            {
                string tier1 = GetTier1Root(subval, 2);
                if (tier1.EndsWith("c") || tier1.EndsWith("t")) tier1 += "i";
                return tier1 + prefixes[val];
            }
        }

        long subprefix = (long)(value / Math.Pow(1000, prefix));
        long subsubprefix = (long)(value / Math.Pow(1000, prefix - 1)) % 1000;

        if (subsubprefix < 1)
        {
            return GetTier2Subroot(prefix, subprefix);
        }
        else
        {
            return GetTier2Subroot(prefix, subprefix) + (prefix == 1 ? "i" : "o") + "-" + GetTier2Subroot(prefix - 1, subsubprefix);
        }
    }

}
