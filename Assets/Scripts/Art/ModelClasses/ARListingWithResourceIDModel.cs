using System;
using System.Collections.Generic;
using UnityEngine;

namespace arListingWithResourceID
{
    [Serializable]
    public class Datum
    {
        public int id;
        public string date;
        public int type_id;
        public int views;
        public List<object> tags;
        public Files files;
        public supplement_files supplement_files;
        public Translations translations;
    }

    [Serializable]
    public class En
    {
        public string locale;
        public string preview_url;
        public string media_url;
        public object duration;
        public string type;
        public string name;
        public object description;
        public object text_content;
    }

    [Serializable]
    public class Files
    {
        public En en;
        public Fr fr;
        public Pt pt;
    }

    [Serializable]
    public class supplement_files
    {
        public En en;
        public Fr fr;
        public Pt pt;
    }

    [Serializable]
    public class Fr
    {
        public string locale;
        public string preview_url;
        public string media_url;
        public object duration;
        public string type;
        public string name;
        public object description;
        public object text_content;
    }

    [Serializable]
    public class Message
    {
        public int code;
        public string description;
    }

    [Serializable]
    public class Pt
    {
        public string locale;
        public string preview_url;
        public string media_url;
        public object duration;
        public string type;
        public string name;
        public object description;
        public object text_content;
    }

    [Serializable]
    public class Root
    {
        public List<Datum> data;
        public Status status;
        public Message message;
        public bool flag;
    }

    [Serializable]
    public class Status
    {
        public int code;
        public string description;
    }

    [Serializable]
    public class Translations
    {
        public En en;
        public Fr fr;
        public Pt pt;
    }

}


