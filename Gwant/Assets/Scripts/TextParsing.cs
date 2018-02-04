﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextParsing {

    private const int DEFAULT_INT_VALUE = -1000;
    private const int INACTIVE_INT = -1;
    private static List<int> idList = new List<int>();

    public static List<GameObject> ParseText(string text, out List<string> strings)
    {
        List<GameObject> cards = new List<GameObject>();
        //match:    'faction' + (0+ spaces) + '=' + (0+ spaces) + '{' + 
        //          (everything until the end of file)
        List<string> matches = new List<string>();
        //Match m = Regex.Match(text, @"(faction)\s*(=)\s*({)(?s).*");
        MatchCollection regexMatches = Regex.Matches(text, @"(faction)\s*(=)\s*({)(?s).*");
        foreach (Match m in regexMatches)
        {
        //matches.Add(m.Value);
            int OpenBracketCount = 1;
            int CloseBracketCount = 0;
            for (int i = 0; i < m.Value.Length; i++)
            {
                char ch = m.Value[i];
                if (ch == '{')
                    OpenBracketCount++;
                else if (ch == '}')
                    CloseBracketCount++;

                if (OpenBracketCount == CloseBracketCount)
                {
                    matches.Add(m.Value.Substring(0, i));
                    break;
                }
            }
        }
        if (matches.Count == 0)
            matches.Add(text);
        //strings = matches;


        //string name = "";

        foreach (string s in matches)
        {
            //Unit matches
            MatchCollection matchCollection = Regex.Matches(s, @"(unit)\s*=\s{[^{]+}");
            foreach (Match m in matchCollection)
            {
                //get id
                int id = GetIntProperty("ID", m.Value);
                if (idList.Contains(id))
                    throw new GwantExceptions.DuplicateIDException(id);
                else
                    idList.Add(id);

                //get name
                string name = GetStringProperty("Name", m.Value);

                //get art
                string art = GetStringProperty("Art", m.Value);
                if (art == null) art = name.Replace(" ", string.Empty);
                art = "art/" + art.ToLower() + ".jpg";

                //get strength
                int strength = GetIntProperty("Strength", m.Value);
                if (strength == DEFAULT_INT_VALUE)
                    throw new GwantExceptions.InvalidStrengthException(id);

                //get hero
                bool hero = GetBoolProperty("Hero", m.Value); //invalid result defaults to false


                //get ability
                Enum ab = GetEnumProperty(EnumProperties.Ability, m.Value);
                Card.Abilities ability = (ab != null)? (Card.Abilities)ab : Card.Abilities.None;

                //get section
                UnitCard.Sections section = (UnitCard.Sections)GetEnumProperty(EnumProperties.Section, m.Value);


                //if ability = muster
                //get muster name
                string muster = (ability == Card.Abilities.Muster)? GetStringProperty("Muster", m.Value) : null;
                if (muster == null && ability == Card.Abilities.Muster)
                    muster = name;

                //if ability = scorch
                //get scorch threshold
                int scorch = INACTIVE_INT;
                if (ability == Card.Abilities.Scorch)
                {
                    scorch = GetIntProperty("Scorch", m.Value);
                    scorch = (scorch == DEFAULT_INT_VALUE) ? 10 : scorch;
                }
                
                //if ability = avenger
                //get avenger id
                int avenger = INACTIVE_INT;
                if (ability == Card.Abilities.Avenger)
                {
                    avenger = GetIntProperty("Avenger", m.Value);
                    if (avenger == DEFAULT_INT_VALUE)
                    {
                        throw new GwantExceptions.InvalidAvengerException(id);
                    }
                }

                //get count
                int count = GetIntProperty("Count", m.Value);
                count = (count == DEFAULT_INT_VALUE) ? 1 : count;

                //Create the cards
                for (int i = 0; i < count; i++)
                {
                    GameObject go = new GameObject();
                    UnitCard.AddComponentTo(go, id, name, art, section, strength, hero, ability, avenger, muster, scorch);
                    go.name = name;
                    cards.Add(go);
                }
            }

            //Special matches
            matchCollection = Regex.Matches(s, @"(special)\s*=\s{[^{]+}");
            foreach (Match m in matchCollection)
            {
                //get id
                int id = GetIntProperty("ID", m.Value);
                if (idList.Contains(id))
                    throw new GwantExceptions.DuplicateIDException(id);
                else
                    idList.Add(id);

                //get name
                string name = GetStringProperty("Name", m.Value);

                //get art
                string art = GetStringProperty("Art", m.Value);
                if (art == null) art = name.Replace(" ", string.Empty);
                art = "art/" + art.ToLower() + ".jpg";


                //get ability
                Enum ab = GetEnumProperty(EnumProperties.Ability, m.Value);
                Card.Abilities ability = (ab != null) ? (Card.Abilities)ab : Card.Abilities.None;

                //get weather type
                SpecialCard.WeatherTypes weatherType;
                if (ability == Card.Abilities.Weather)
                {
                    Enum w = GetEnumProperty(EnumProperties.Ability, m.Value);
                    weatherType = (w != null) ? (SpecialCard.WeatherTypes)w :
                        SpecialCard.WeatherTypes.None;
                }
                else weatherType = SpecialCard.WeatherTypes.None;

                
                //get count
                int count = GetIntProperty("Count", m.Value);
                count = (count == DEFAULT_INT_VALUE) ? 1 : count;

                //Create the cards
                for (int i = 0; i < count; i++)
                {
                    GameObject go = new GameObject();
                    SpecialCard.AddComponentTo(go, id, name, art, ability, weatherType);
                    go.name = name;
                    cards.Add(go);
                }
            }
        }

        strings = matches;
        //strings.Add(name);
        return cards;
    }

    private enum StringProperties { Name, Art }
    private static string GetStringProperty(string Property, string Text)
    {
        string pattern = @"(" + Property + @")\s*=\s*""([\p{L}\p{N}\s':]{1,30})""";
        try
        {
            string output = (string)GetProperty(Text, pattern);
            return output;
        }
        catch (InvalidCastException)
        {
            return null;
        }
    }

    private enum EnumProperties { Ability, Section, WeatherType }
    private static Enum GetEnumProperty(EnumProperties Property, string Text)
    {
        string pattern = @"(" + Property.ToString().ToLower() + @")\s*=\s*(";// ([0-9]{1,2})";

        Type enumType;
        if (Property == EnumProperties.Ability)
            enumType = typeof(Card.Abilities);
        else if (Property == EnumProperties.Section)
            enumType = typeof(UnitCard.Sections);
        else// if (Property == EnumProperties.WeatherType)
            enumType = typeof(SpecialCard.WeatherTypes);

        #region Regex Pattern
        if (Property == EnumProperties.Ability)
        {
            //Add Card.Abilities enums to the regex pattern
            for (int i = 0; i < (int)Card.Abilities.COUNT; i++)
            {
                if (i != (int)Card.Abilities.UNIT_END && i != (int)Card.Abilities.SPECIAL_START)
                {
                    pattern += ((Card.Abilities)i).ToString();
                    if (i != (int)Card.Abilities.COUNT - 1)
                        pattern += @"|";
                    else
                        pattern += @")";
                }
            }
        }
        else if (Property == EnumProperties.Section)
        {
            //Add UnitCard.Sections enums to the regex pattern
            for (int i = 0; i < (int)UnitCard.Sections.COUNT; i++)
            {
                pattern += ((UnitCard.Sections)i).ToString();
                if (i != (int)UnitCard.Sections.COUNT - 1)
                    pattern += @"|";
                else
                    pattern += @")";
            }
        }
        else if (Property == EnumProperties.WeatherType)
        {
            //Add EnumProperties.WeatherTypes enums to the regex pattern
            for (int i = 0; i < (int)SpecialCard.WeatherTypes.COUNT; i++)
            {
                pattern += ((SpecialCard.WeatherTypes)i).ToString();
                if (i != (int)SpecialCard.WeatherTypes.COUNT - 1)
                    pattern += @"|";
                else
                    pattern += @")";
            }
        }
        #endregion

        try
        {
            object o = GetProperty(Text, pattern);
            if (o != null)
            {
                string output = (string)o;
                char ch = char.ToUpper(output[0]);
                output = ch + output.Substring(1);
                return (Enum)Enum.Parse(enumType, output);
            }
            return null;
        }
        catch (InvalidCastException)
        {
            return null;
        }
    }

    private enum IntProperties { ID, Strength, Avenger }
    private static int GetIntProperty(string Property, string Text)
    {
        string pattern = @"(" + Property.ToString().ToLower() + @")\s*=\s*([0-9]{1,";// 2})";
        if (Property.ToString().ToLower().Equals("strength"))
            pattern += @"2})";
        else
            pattern += @"3})";
        try
        {
            int output;
            if (int.TryParse((string)GetProperty(Text, pattern), out output))
                return output;
            else
                return DEFAULT_INT_VALUE;
        }
        catch (InvalidCastException)
        {
            return -2;
        }
        catch (NullReferenceException)
        {
            return -3;
        }
    }

    private enum BoolProperties { Hero }
    private static bool GetBoolProperty(string Property, string Text)
    {
        string pattern = @"(" + Property.ToString().ToLower() + @")\s*=\s*(true|false)";
        try
        {
            string o = (string)GetProperty(Text, pattern);
            bool output;
            if (bool.TryParse(o, out output))
                return output;
            else
                return false;
        }
        catch (InvalidCastException)
        {
            //throw new InvalidCastException();
            return false;
        }
        catch (NullReferenceException)
        {
            return false;
        }
    }
    
    private static object GetProperty(string Text, string pattern)
    {

        //object valueToReturn = null;
        Match m = Regex.Match(Text, pattern, RegexOptions.IgnoreCase);
        if (m.Groups[2].Success)
            return m.Groups[2].Value;
        else
            return null;

        //return valueToReturn;
    }

}
