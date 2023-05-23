
using System;
using System.Collections;
using System.Collections.Generic;
namespace arListing
{
    [Serializable]
    public class Datas
    {

        public int id;
        public int type_id;
        public int business_id;
        public int user_id;
        public object resource_id;
        public object location_id;
        public int views;
        public bool vr_active;
        public bool active;
        public object delete;
        public DateTime created_at;
        public DateTime updated_at;
        public object deleted_at;
        public object latitude;
        public object longitude;
        public object coordinates;
        public object external_url;
        public string date;
        public List<Identifier> identifiers;
        public List<Object> tags;
        public Translations translations = null;
        public bool vr_related;
        public Resource[] resources;
        public int resources_count;
        public List<int> paired_resources;
        public Preview preview;
    }
    [Serializable]
    public class En
    {
        public string locale;
        public string name;
        public string description;
    }
    [Serializable]
    public class Fr
    {
        public string locale;
        public string name;
        public string description;
    }
    [Serializable]
    public class Identifier
    {
        public int id;
        public string name;
        public string preview_url;
        public string media_url;
        public string type_id;
        public string format;
        public string status;
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
        public string name;
        public string description;
    }
    [Serializable]
    public class Resource
    {
        public int id;
        public int type_id;
    }

    [Serializable]
    public class Root
    {
        public List<Datas> data = null;
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

    [Serializable]
    public class Preview
    {
        public string url;
        //public int width { get; set; }
        //public int height { get; set; }
        //public string type { get; set; }
    }
}