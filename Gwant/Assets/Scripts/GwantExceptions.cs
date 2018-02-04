using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwantExceptions : Exception {

    public GwantExceptions() : base()
    {

    }

    public GwantExceptions(string message) : base(message)
    {

    }

    public class InvalidCardException : GwantExceptions
    {

        public InvalidCardException()
        {

        }

    }

    public class InvalidAvengerException : GwantExceptions
    {
        public InvalidAvengerException(int ID) : base("Invalid avenger ID for card " + ID +
            " Add one in the format: avenger = X")
        {

        }
    }

    public class InvalidWeatherException : GwantExceptions
    {
        public InvalidWeatherException(int ID) : base("Please enter a valid weather type " +
            "for card " + ID + " in the format: weather = X")
        {

        }
    }
}
